// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.CRCreateContactActionBase`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.GDPR;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR.Extensions.CRCreateActions;

/// <exclude />
public abstract class CRCreateContactActionBase<TGraph, TMain> : 
  CRCreateActionBase<TGraph, TMain, ContactMaint, Contact, ContactFilter, ContactConversionOptions>
  where TGraph : PXGraph, new()
  where TMain : class, IBqlTable, new()
{
  [PXHidden]
  [PXCopyPasteHiddenView]
  public CRValidationFilter<ContactFilter> ContactInfo;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public CRValidationFilter<PopupAttributes> ContactInfoAttributes;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public CRValidationFilter<PopupUDFAttributes> ContactInfoUDF;
  public PXAction<TMain> CreateContact;
  public PXAction<TMain> CreateContactCancel;
  public PXAction<TMain> CreateContactFinish;
  public PXAction<TMain> CreateContactFinishRedirect;
  public PXAction<TMain> CreateContactToolBar;
  public PXAction<TMain> CreateContactRedirect;

  protected override string TargetType => "PX.Objects.CR.Contact";

  protected override ICRValidationFilter[] AdditionalFilters
  {
    get
    {
      return new ICRValidationFilter[2]
      {
        (ICRValidationFilter) this.ContactInfoAttributes,
        (ICRValidationFilter) this.ContactInfoUDF
      };
    }
  }

  protected override CRValidationFilter<ContactFilter> FilterInfo => this.ContactInfo;

  protected virtual IEnumerable contactInfoAttributes() => (IEnumerable) this.GetFilledAttributes();

  protected virtual IEnumerable<PopupUDFAttributes> contactInfoUDF() => this.GetRequiredUDFFields();

  public virtual void _(
    Events.FieldUpdated<ContactFilter, ContactFilter.contactClass> e)
  {
    ((PXCache) GraphHelper.Caches<PopupAttributes>((PXGraph) this.Base)).Clear();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable createContact(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CRCreateContactActionBase<TGraph, TMain>.\u003C\u003Ec__DisplayClass13_0 cDisplayClass130 = new CRCreateContactActionBase<TGraph, TMain>.\u003C\u003Ec__DisplayClass13_0();
    // ISSUE: reference to a compiler-generated field
    if (this.AskExtConvert(false, out cDisplayClass130.redirect))
    {
      if (this.Base.IsDirty)
        this.Base.Actions.PressSave();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass130.processingGraph = this.Base.CloneGraphState<TGraph>();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass130, __methodptr(\u003CcreateContact\u003Eb__0)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable createContactCancel(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable createContactFinish(PXAdapter adapter)
  {
    if (!this.Base.IsImport && !this.Base.IsContractBasedAPI && !this.PopupValidator.TryValidate())
      throw new PXActionInterruptException("Validation failed.");
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable createContactFinishRedirect(PXAdapter adapter)
  {
    return this.createContactFinish(adapter);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false, ImageSet = "main", ImageKey = "AddNew", PopupCommand = "RefreshContact")]
  public virtual IEnumerable createContactToolBar(PXAdapter adapter) => this.createContact(adapter);

  [PXUIField]
  [PXButton]
  public virtual IEnumerable createContactRedirect(PXAdapter adapter)
  {
    ContactMaint targetGraph = this.CreateTargetGraph();
    Contact master = this.CreateMaster(targetGraph, (ContactConversionOptions) null);
    ConversionResult<Contact> result = new ConversionResult<Contact>();
    result.Graph = (PXGraph) targetGraph;
    result.Entity = master;
    result.Converted = false;
    this.Redirect(result);
    return adapter.Get();
  }

  protected override Contact CreateMaster(ContactMaint graph, ContactConversionOptions _)
  {
    Document current1 = ((PXSelectBase<Document>) this.Documents).Current;
    ContactFilter current2 = ((PXSelectBase<ContactFilter>) this.ContactInfo).Current;
    DocumentContact source1 = ((PXSelectBase<DocumentContact>) this.Contacts).SelectSingle(Array.Empty<object>());
    DocumentAddress source2 = ((PXSelectBase<DocumentAddress>) this.Addresses).SelectSingle(Array.Empty<object>());
    Contact target1 = new Contact()
    {
      ContactType = "PN",
      ParentBAccountID = current1.ParentBAccountID
    };
    this.MapContact(source1, (IPersonalContact) target1);
    this.MapConsentable(source1, (IConsentable) target1);
    this.MapFromFilter(current2, target1);
    this.MapFromDocument(current1, target1);
    Contact contact1 = ((PXSelectBase<Contact>) graph.Contact).Insert(target1);
    if (PXResultset<CRContactClass>.op_Implicit(PXSelectBase<CRContactClass, PXSelect<CRContactClass, Where<CRContactClass.classID, Equal<Required<CRContactClass.classID>>>>.Config>.SelectSingleBound((PXGraph) graph, (object[]) null, new object[1]
    {
      (object) contact1.ClassID
    }))?.DefaultOwner == "S")
    {
      contact1.WorkgroupID = current1.WorkgroupID;
      contact1.OwnerID = current1.OwnerID;
    }
    Address target2 = ((PXSelectBase<Address>) graph.AddressCurrent).SelectSingle(Array.Empty<object>()) ?? throw new InvalidOperationException("Cannot get Address for Business Account.");
    this.MapAddress(source2, (IAddressBase) target2);
    Address address = (Address) ((PXSelectBase) graph.AddressCurrent).Cache.Update((object) target2);
    contact1.DefAddressID = address.AddressID;
    Contact contact2 = ((PXSelectBase<Contact>) graph.Contact).Update(contact1);
    this.ReverseDocumentUpdate(graph, contact2);
    this.FillRelations((PXGraph) graph, contact2);
    this.FillAttributes(graph.Answers, contact2);
    this.FillUDF(((PXSelectBase) this.ContactInfoUDF).Cache, ((PXSelectBase) this.Documents).Cache.GetMain<Document>(current1), ((PXSelectBase) graph.Contact).Cache, contact2, contact2.ClassID);
    this.FillNotesAndAttachments((PXGraph) graph, ((PXSelectBase) this.Documents).Cache.GetMain<Document>(current1), ((PXSelectBase) graph.Contact).Cache, contact2);
    return ((PXSelectBase<Contact>) graph.Contact).Update(contact2);
  }

  protected override Contact MapFromDocument(Document source, Contact target)
  {
    target = base.MapFromDocument(source, target);
    target.ContactType = "PN";
    target.ParentBAccountID = source.ParentBAccountID;
    target.BAccountID = source.BAccountID;
    target.OverrideAddress = new bool?(true);
    target.Source = source.Source;
    target.LanguageID = source.LocaleName;
    target.OverrideSalesTerritory = source.OverrideSalesTerritory;
    bool? overrideSalesTerritory = target.OverrideSalesTerritory;
    if (overrideSalesTerritory.HasValue && overrideSalesTerritory.GetValueOrDefault())
      target.SalesTerritoryID = source.SalesTerritoryID;
    return target;
  }

  protected override IPersonalContact MapContact(
    DocumentContact docContact,
    IPersonalContact target)
  {
    base.MapContact(docContact, target);
    target.Title = (string) null;
    return target;
  }

  protected virtual void MapContactMethod(DocumentContactMethod source, Contact target)
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    if (target == null)
      throw new ArgumentNullException(nameof (target));
    target.Method = source.Method;
    target.NoFax = source.NoFax;
    target.NoMail = source.NoMail;
    target.NoMarketing = source.NoMarketing;
    target.NoCall = source.NoCall;
    target.NoEMail = source.NoEMail;
    target.NoMassMail = source.NoMassMail;
  }

  protected virtual void MapFromFilter(ContactFilter source, Contact target)
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    if (target == null)
      throw new ArgumentNullException(nameof (target));
    target.FirstName = source.FirstName;
    target.LastName = source.LastName;
    target.FullName = source.FullName;
    target.Salutation = source.Salutation;
    target.Phone1 = source.Phone1;
    target.Phone1Type = source.Phone1Type;
    target.Phone2 = source.Phone2;
    target.Phone2Type = source.Phone2Type;
    target.EMail = source.Email;
    target.ClassID = source.ContactClass;
  }

  protected override void OnBeforePersist(ContactMaint graph, Contact entity)
  {
    ContactMaint.CRDuplicateEntitiesForContactGraphExt implementation = ((PXGraph) graph).FindImplementation<ContactMaint.CRDuplicateEntitiesForContactGraphExt>();
    if (implementation == null)
      return;
    implementation.HardBlockOnly = true;
  }
}
