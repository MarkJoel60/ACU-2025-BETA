// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRPrimaryContactGraphExt`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Export;
using PX.Api.Models;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GDPR;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CR.Extensions;

/// <exclude />
public abstract class CRPrimaryContactGraphExt<TGraph, TContactDetails, TMaster, FBAccountID, FPrimaryContactID> : 
  PXGraphExtension<
  #nullable disable
  TGraph>
  where TGraph : PXGraph
  where TContactDetails : PXGraphExtension<TGraph>
  where TMaster : BAccount, IBqlTable, new()
  where FBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FBAccountID>
  where FPrimaryContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FPrimaryContactID>
{
  [PXCopyPasteHiddenView]
  [PXViewName("Primary Contact")]
  public FbqlSelect<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  Contact.bAccountID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  FBAccountID, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  Contact.contactType, IBqlString>.IsEqual<
  #nullable disable
  ContactTypesAttribute.person>>>>.And<BqlOperand<
  #nullable enable
  Contact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  FPrimaryContactID, IBqlInt>.FromCurrent>>>, 
  #nullable disable
  Contact>.View PrimaryContactCurrent;
  protected PXView NonDirtyContactsGrid;
  public PXAction<TMaster> AddNewPrimaryContact;
  public PXAction<TMaster> MakeContactPrimary;

  public TContactDetails ContactDetailsExtension { get; private set; }

  protected abstract PXView ContactsView { get; }

  [PXOptimizationBehavior(IgnoreBqlDelegate = true)]
  protected IEnumerable primaryContactCurrent()
  {
    CRPrimaryContactGraphExt<TGraph, TContactDetails, TMaster, FBAccountID, FPrimaryContactID> primaryContactGraphExt = this;
    Contact contact = (Contact) null;
    if (primaryContactGraphExt.Base.Caches[typeof (TMaster)].Current is TMaster current && current.PrimaryContactID.HasValue)
    {
      using (new PXReadDeletedScope(false))
        contact = NonGenericIEnumerableExtensions.FirstOrDefault_(GraphHelper.QuickSelect(((PXSelectBase) primaryContactGraphExt.PrimaryContactCurrent).View)) as Contact;
    }
    if (contact == null && (object) current != null && !current.PrimaryContactID.HasValue)
    {
      using (new ReadOnlyScope(new PXCache[2]
      {
        ((PXSelectBase) primaryContactGraphExt.PrimaryContactCurrent).Cache,
        primaryContactGraphExt.Base.Caches[typeof (TMaster)]
      }))
      {
        contact = new Contact()
        {
          ContactType = "PN",
          Status = "A",
          Phone2Type = "C",
          FullName = current.AcctName,
          LanguageID = current.LocaleName
        };
        contact = ((PXSelectBase<Contact>) primaryContactGraphExt.PrimaryContactCurrent).Insert(contact);
        ((PXSelectBase) primaryContactGraphExt.PrimaryContactCurrent).Cache.SetValueExt<ContactExt.isAddedAsExt>((object) contact, (object) true);
        ((PXSelectBase) primaryContactGraphExt.PrimaryContactCurrent).Cache.SetValue<Contact.defAddressID>((object) contact, (object) current.DefAddressID);
        current.PrimaryContactID = contact.ContactID;
        PXEntryStatus status = primaryContactGraphExt.Base.Caches[typeof (TMaster)].GetStatus((object) current);
        primaryContactGraphExt.Base.Caches[typeof (TMaster)].Update((object) current);
        primaryContactGraphExt.Base.Caches[typeof (TMaster)].SetStatus((object) current, status == null ? (PXEntryStatus) (object) 5 : status);
      }
    }
    if (contact != null)
      primaryContactGraphExt.SetUI(current, contact);
    yield return (object) contact;
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.ContactDetailsExtension = this.Base.GetExtension<TContactDetails>() ?? throw new PXException("The graph does not have defined extension: {0}.", new object[1]
    {
      (object) typeof (TContactDetails).Name
    });
    this.Base.Views[this.ContactsView.Name].WhereAnd<Where<BqlOperand<ContactExt.isMeaningfull, IBqlBool>.IsEqual<True>>>();
    this.NonDirtyContactsGrid = new PXView((PXGraph) this.Base, true, this.Base.Views[this.ContactsView.Name].BqlSelect);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable addNewPrimaryContact(PXAdapter adapter)
  {
    if (this.Base.Caches[typeof (TMaster)].Current is TMaster current && (object) current != null)
    {
      ContactMaint instance = PXGraph.CreateInstance<ContactMaint>();
      Contact contact = ((PXSelectBase<Contact>) instance.Contact).Insert();
      contact.BAccountID = current.BAccountID;
      if (PXResultset<CRContactClass>.op_Implicit(PXSelectBase<CRContactClass, PXSelect<CRContactClass, Where<CRContactClass.classID, Equal<Current<Contact.classID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
      {
        (object) contact
      }, Array.Empty<object>()))?.DefaultOwner == "S")
      {
        contact.WorkgroupID = current.WorkgroupID;
        contact.OwnerID = current.OwnerID;
      }
      ((PXSelectBase<Contact>) instance.Contact).Update(contact);
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Contact");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Set as Primary")]
  [PXButton]
  public virtual void makeContactPrimary()
  {
    if (!(this.Base.Caches[typeof (TMaster)].Current is TMaster current1) || !current1.BAccountID.HasValue || !(this.ContactsView.Cache.Current is Contact current2) || !current2.ContactID.HasValue)
      return;
    Contact contact = ((PXSelectBase<Contact>) this.PrimaryContactCurrent).SelectSingle(Array.Empty<object>());
    if (contact != null && ((PXSelectBase) this.PrimaryContactCurrent).Cache.GetStatus((object) contact) == 2)
      ((PXSelectBase) this.PrimaryContactCurrent).Cache.Delete((object) contact);
    current1.PrimaryContactID = current2.ContactID;
    this.Base.Caches[typeof (TMaster)].Update((object) current1);
  }

  protected virtual void _(Events.RowSelected<TMaster> e)
  {
    TMaster row = e.Row;
    if ((object) row == null)
      return;
    PXUIFieldAttribute.SetVisible<FPrimaryContactID>(this.Base.Caches[typeof (TMaster)], this.Base.Caches[typeof (TMaster)].Current, this.NonDirtyContactsGrid?.SelectSingle(Array.Empty<object>()) != null);
    if (!this.Base.IsContractBasedAPI)
      return;
    PXUIFieldAttribute.SetReadOnly<FPrimaryContactID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<TMaster>>) e).Cache, (object) row);
  }

  protected virtual void _(Events.RowSelected<Contact> e)
  {
    Contact row = e.Row;
    if (row == null)
      return;
    row.IsPrimary = new bool?(false);
    if (!(this.Base.Caches[typeof (TMaster)].Current is TMaster current))
      return;
    int? contactId = row.ContactID;
    int? primaryContactId = current.PrimaryContactID;
    if (contactId.GetValueOrDefault() == primaryContactId.GetValueOrDefault() & contactId.HasValue == primaryContactId.HasValue)
      row.IsPrimary = new bool?(true);
    if (!row.IsPrimary.GetValueOrDefault() || !this.Base.IsContractBasedAPI)
      return;
    PXUIFieldAttribute.SetReadOnly<Contact.bAccountID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<Contact>>) e).Cache, (object) row);
    PXUIFieldAttribute.SetReadOnly<Contact.contactID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<Contact>>) e).Cache, (object) row);
  }

  protected virtual void _(Events.RowDeleted<TMaster> e)
  {
    TMaster row = e.Row;
    if ((object) row == null || EnumerableExtensions.IsIn<PXEntryStatus>(((Events.Event<PXRowDeletedEventArgs, Events.RowDeleted<TMaster>>) e).Cache.GetStatus((object) row), (PXEntryStatus) 2, (PXEntryStatus) 4))
      return;
    Contact contact = ((PXSelectBase<Contact>) this.PrimaryContactCurrent).SelectSingle(Array.Empty<object>());
    if (contact == null || ((PXSelectBase) this.PrimaryContactCurrent).Cache.GetStatus((object) contact) != 2)
      return;
    ((PXSelectBase) this.PrimaryContactCurrent).Cache.Delete((object) contact);
  }

  protected virtual void _(Events.FieldUpdated<FPrimaryContactID> e)
  {
    if (e.Row == null || ((Events.FieldUpdatedBase<Events.FieldUpdated<FPrimaryContactID>, object, object>) e).OldValue == null)
      return;
    TMaster row = e.Row as TMaster;
    if (e.NewValue != null)
    {
      Contact contact = ((PXSelectBase<Contact>) this.PrimaryContactCurrent).SelectSingle(Array.Empty<object>());
      if (contact != null && row.AcctName != null && !row.AcctName.Equals(contact.FullName))
      {
        contact.FullName = row.AcctName;
        ((PXSelectBase<Contact>) this.PrimaryContactCurrent).Update(contact);
      }
    }
    Contact contact1 = PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelect<Contact, Where<Contact.contactID, Equal<Required<Contact.contactID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
    {
      ((Events.FieldUpdatedBase<Events.FieldUpdated<FPrimaryContactID>, object, object>) e).OldValue
    }));
    if (contact1 != null && this.ContactsView.Cache.GetStatus((object) contact1) == 2)
      this.ContactsView.Cache.Delete((object) contact1);
    if (e.NewValue != null)
      return;
    this.Base.SelectTimeStamp();
  }

  protected virtual void _(Events.RowInserting<TMaster> e)
  {
    TMaster row = e.Row;
    if ((object) row == null)
      return;
    int? nullable = ((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<TMaster>>) e).Cache.GetValue<FPrimaryContactID>((object) row) as int?;
    int num = 0;
    if (!(nullable.GetValueOrDefault() < num & nullable.HasValue))
      return;
    ((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<TMaster>>) e).Cache.SetValue<FPrimaryContactID>((object) row, (object) null);
  }

  [PXOverride]
  public virtual void Persist(Action del)
  {
    Contact destData = ((PXSelectBase<Contact>) this.PrimaryContactCurrent).SelectSingle(Array.Empty<object>());
    bool? nullable;
    if (destData != null)
    {
      if (((PXSelectBase) this.PrimaryContactCurrent).Cache.GetStatus((object) destData) == 2)
      {
        nullable = ((PXSelectBase) this.PrimaryContactCurrent).Cache.GetExtension<ContactExt>((object) destData).IsMeaningfull;
        if (!nullable.GetValueOrDefault())
        {
          ((PXSelectBase) this.PrimaryContactCurrent).Cache.Delete((object) destData);
          this.Base.Caches[typeof (TMaster)].SetValue<FPrimaryContactID>(this.Base.Caches[typeof (TMaster)].Current, (object) null);
        }
        else
          UDFHelper.CopyAttributes(this.Base.Caches[typeof (TMaster)], (object) null, ((PXSelectBase) this.PrimaryContactCurrent).Cache, (object) destData, destData?.ClassID);
      }
    }
    else
      this.Base.Caches[typeof (TMaster)].SetValue<FPrimaryContactID>(this.Base.Caches[typeof (TMaster)].Current, (object) null);
    foreach (object obj in ((PXSelectBase) this.PrimaryContactCurrent).Cache.Inserted)
    {
      ContactExt extension = ((PXSelectBase) this.PrimaryContactCurrent).Cache.GetExtension<ContactExt>(obj);
      nullable = extension.IsAddedAsExt;
      if (nullable.GetValueOrDefault())
      {
        nullable = extension.IsMeaningfull;
        if (!nullable.GetValueOrDefault())
          ((PXSelectBase) this.PrimaryContactCurrent).Cache.Delete(obj);
      }
    }
    del();
  }

  protected virtual void SetUI(TMaster account, Contact contact)
  {
    bool flag1 = this.NonDirtyContactsGrid?.SelectSingle(Array.Empty<object>()) != null;
    int num1;
    if ((object) account != null)
    {
      int? primaryContactId = account.PrimaryContactID;
      int num2 = 0;
      if (primaryContactId.GetValueOrDefault() > num2 & primaryContactId.HasValue)
      {
        num1 = contact != null ? (!contact.DeletedDatabaseRecord.GetValueOrDefault() ? 1 : 0) : 1;
        goto label_4;
      }
    }
    num1 = 0;
label_4:
    bool isRealContactSelected = num1 != 0;
    bool flag2 = !flag1 | isRealContactSelected;
    PXUIFieldAttribute.SetVisible<Contact.firstName>(((PXSelectBase) this.PrimaryContactCurrent).Cache, (object) contact, !flag1);
    PXUIFieldAttribute.SetVisible<Contact.lastName>(((PXSelectBase) this.PrimaryContactCurrent).Cache, (object) contact, !flag1);
    PXUIFieldAttribute.SetEnabled<Contact.salutation>(((PXSelectBase) this.PrimaryContactCurrent).Cache, (object) contact, flag2);
    PXUIFieldAttribute.SetEnabled<Contact.eMail>(((PXSelectBase) this.PrimaryContactCurrent).Cache, (object) contact, flag2);
    PXUIFieldAttribute.SetEnabled<Contact.phone1>(((PXSelectBase) this.PrimaryContactCurrent).Cache, (object) contact, flag2);
    PXUIFieldAttribute.SetEnabled<Contact.phone1Type>(((PXSelectBase) this.PrimaryContactCurrent).Cache, (object) contact, flag2);
    PXUIFieldAttribute.SetEnabled<Contact.phone2>(((PXSelectBase) this.PrimaryContactCurrent).Cache, (object) contact, flag2);
    PXUIFieldAttribute.SetEnabled<Contact.phone2Type>(((PXSelectBase) this.PrimaryContactCurrent).Cache, (object) contact, flag2);
    PXUIFieldAttribute.SetEnabled<Contact.consentAgreement>(((PXSelectBase) this.PrimaryContactCurrent).Cache, (object) contact, flag2);
    PXUIFieldAttribute.SetEnabled<Contact.consentDate>(((PXSelectBase) this.PrimaryContactCurrent).Cache, (object) contact, flag2);
    PXUIFieldAttribute.SetEnabled<Contact.consentExpirationDate>(((PXSelectBase) this.PrimaryContactCurrent).Cache, (object) contact, flag2);
    EnumerableExtensions.ForEach<PXConsentAgreementFieldAttribute>(((PXSelectBase) this.PrimaryContactCurrent).Cache.GetAttributesOfType<PXConsentAgreementFieldAttribute>((object) contact, typeof (Contact.consentAgreement).Name), (Action<PXConsentAgreementFieldAttribute>) (a => a.SuppressWarning = !isRealContactSelected));
  }

  [PXOverride]
  public virtual void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
    int index = script.FindIndex((Predicate<Command>) (_ => _.FieldName == "PrimaryContactID"));
    if (index == -1)
      return;
    Command command = script[index];
    Container container = containers[index];
    script.Remove(command);
    containers.Remove(container);
  }
}
