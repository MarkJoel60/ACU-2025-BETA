// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRContactAccountDataSync.CRContactAccountDataSync`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR.Extensions.CRContactAccountDataSync;

/// <exclude />
[Obsolete("Not used anymore. Support of this class is abandoned.")]
public abstract class CRContactAccountDataSync<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  public PXSelectExtension<Document> Documents;
  public PXSelectExtension<DocumentContact> Contacts;
  public PXSelectExtension<DocumentAddress> Addresses;
  public PXSelect<BAccount, Where<BAccount.bAccountID, Equal<Current<Document.bAccountID>>>> ExistingAccount;

  protected abstract PX.Objects.CR.Extensions.CRContactAccountDataSync.CRContactAccountDataSync<TGraph>.DocumentMapping GetDocumentMapping();

  protected abstract PX.Objects.CR.Extensions.CRContactAccountDataSync.CRContactAccountDataSync<TGraph>.DocumentContactMapping GetDocumentContactMapping();

  protected abstract PX.Objects.CR.Extensions.CRContactAccountDataSync.CRContactAccountDataSync<TGraph>.DocumentAddressMapping GetDocumentAddressMapping();

  protected virtual void _(Events.RowSelected<Document> e)
  {
    Document row = e.Row;
    if (row == null)
      return;
    PXCache cache = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<Document>>) e).Cache;
    Document document = row;
    int? nullable = row.RefContactID;
    int num;
    if (!nullable.HasValue)
    {
      nullable = row.BAccountID;
      num = nullable.HasValue ? 1 : 0;
    }
    else
      num = 1;
    PXUIFieldAttribute.SetEnabled<Document.overrideRefContact>(cache, (object) document, num != 0);
  }

  protected virtual void _(
    Events.FieldUpdated<Document, Document.overrideRefContact> e)
  {
    if (e.Row == null)
      return;
    bool? overrideRefContact = e.Row.OverrideRefContact;
    bool flag = false;
    if (!(overrideRefContact.GetValueOrDefault() == flag & overrideRefContact.HasValue) || !((Events.FieldUpdatedBase<Events.FieldUpdated<Document, Document.overrideRefContact>>) e).ExternalCall)
      return;
    if (e.Row.RefContactID.HasValue)
    {
      this.FillFromContact();
    }
    else
    {
      if (!e.Row.BAccountID.HasValue)
        return;
      this.FillFromAccount();
    }
  }

  protected virtual void _(
    Events.FieldUpdated<Document, Document.refContactID> e)
  {
    if (e.Row == null)
      return;
    int? refContactId = e.Row.RefContactID;
    int? nullable = ((Events.FieldUpdatedBase<Events.FieldUpdated<Document, Document.refContactID>, Document, object>) e).OldValue as int?;
    if (refContactId.GetValueOrDefault() == nullable.GetValueOrDefault() & refContactId.HasValue == nullable.HasValue || this.Base.IsImport && ((PXSelectBase) this.Documents).View.Answer == null || this.Base.IsContractBasedAPI || this.Base.UnattendedMode)
      return;
    nullable = e.Row.RefContactID;
    if (!nullable.HasValue)
    {
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<Document, Document.refContactID>>) e).Cache.RaiseFieldUpdated<Document.bAccountID>((object) e.Row, (object) e.Row.BAccountID);
    }
    else
    {
      ((PXSelectBase) this.Documents).View.Answer = this.MapDocumentDialogAnswers();
      WebDialogResult webDialogResult = ((PXSelectBase) this.Documents).View.Ask((object) null, "Warning", "Would you like the contact information to be replaced with the information from the selected contact?", (MessageButtons) 2, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
      {
        {
          (WebDialogResult) 3,
          "Yes"
        },
        {
          (WebDialogResult) 4,
          "No"
        },
        {
          (WebDialogResult) 5,
          "Cancel"
        }
      }, (MessageIcon) 3);
      if (webDialogResult == 3)
      {
        this.FillFromContact();
        ((PXSelectBase) this.Documents).Cache.SetValue<Document.overrideRefContact>((object) ((PXSelectBase<Document>) this.Documents).Current, (object) false);
      }
      else if (webDialogResult == 4)
      {
        ((PXSelectBase) this.Documents).Cache.SetValue<Document.overrideRefContact>((object) ((PXSelectBase<Document>) this.Documents).Current, (object) true);
      }
      else
      {
        if (webDialogResult != 5)
          return;
        ((PXSelectBase) this.Documents).Cache.SetValue<Document.refContactID>((object) ((PXSelectBase<Document>) this.Documents).Current, ((Events.FieldUpdatedBase<Events.FieldUpdated<Document, Document.refContactID>, Document, object>) e).OldValue);
      }
    }
  }

  protected virtual void _(
    Events.FieldUpdated<Document, Document.bAccountID> e)
  {
    if (e.Row == null || this.Base.IsImport || this.Base.IsContractBasedAPI || this.Base.UnattendedMode)
      return;
    int? nullable = e.Row.BAccountID;
    if (!nullable.HasValue)
    {
      nullable = e.Row.RefContactID;
      if (!nullable.HasValue)
      {
        ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<Document, Document.bAccountID>>) e).Cache.SetValueExt<Document.overrideRefContact>((object) e.Row, (object) false);
        return;
      }
    }
    WebDialogResult webDialogResult = ((PXSelectBase) this.Documents).View.Ask((object) null, "Warning", "Would you like the contact information to be replaced with the information from the selected business account?", (MessageButtons) 2, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
    {
      {
        (WebDialogResult) 3,
        "Yes"
      },
      {
        (WebDialogResult) 4,
        "No"
      },
      {
        (WebDialogResult) 5,
        "Cancel"
      }
    }, (MessageIcon) 3);
    if (webDialogResult == 3)
    {
      this.FillFromAccount();
      ((PXSelectBase) this.Documents).Cache.SetValue<Document.overrideRefContact>((object) ((PXSelectBase<Document>) this.Documents).Current, (object) false);
    }
    else if (webDialogResult == 4)
    {
      ((PXSelectBase) this.Documents).Cache.SetValue<Document.overrideRefContact>((object) ((PXSelectBase<Document>) this.Documents).Current, (object) true);
    }
    else
    {
      if (webDialogResult != 5)
        return;
      ((PXSelectBase) this.Documents).Cache.SetValue<Document.bAccountID>((object) ((PXSelectBase<Document>) this.Documents).Current, ((Events.FieldUpdatedBase<Events.FieldUpdated<Document, Document.bAccountID>, Document, object>) e).OldValue);
    }
  }

  private void FillFromContact()
  {
    Contact contact = PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelect<Contact, Where<Contact.contactID, Equal<Current<Document.refContactID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new Document[1]
    {
      ((PXSelectBase<Document>) this.Documents).Current
    }, Array.Empty<object>()));
    Address address = PXResultset<Address>.op_Implicit(PXSelectBase<Address, PXSelect<Address, Where<Address.addressID, Equal<Current<Contact.defAddressID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new Contact[1]
    {
      contact
    }, Array.Empty<object>()));
    DocumentContact current = ((PXSelectBase) this.Contacts).Cache.Current as DocumentContact;
    DocumentAddress docAddress = ((PXSelectBase<DocumentAddress>) this.Addresses).SelectSingle(new object[1]
    {
      (object) current.DefAddressID
    });
    PX.Objects.CR.Extensions.CRContactAccountDataSync.CRContactAccountDataSync<TGraph>.FillToDocumentContact(((PXSelectBase) this.Contacts).Cache, current, contact);
    PX.Objects.CR.Extensions.CRContactAccountDataSync.CRContactAccountDataSync<TGraph>.FillToDocumentAddress(((PXSelectBase) this.Addresses).Cache, docAddress, address);
  }

  private void FillFromAccount()
  {
    Contact contact = PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelectJoin<Contact, InnerJoin<BAccount, On<BAccount.defContactID, Equal<Contact.contactID>>>, Where<BAccount.bAccountID, Equal<Current<Document.bAccountID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new Document[1]
    {
      ((PXSelectBase<Document>) this.Documents).Current
    }, Array.Empty<object>()));
    Address address = PXResultset<Address>.op_Implicit(PXSelectBase<Address, PXSelectJoin<Address, InnerJoin<BAccount, On<BAccount.defAddressID, Equal<Address.addressID>>>, Where<BAccount.bAccountID, Equal<Current<Document.bAccountID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new Contact[1]
    {
      contact
    }, Array.Empty<object>()));
    DocumentContact current = ((PXSelectBase) this.Contacts).Cache.Current as DocumentContact;
    DocumentAddress docAddress = ((PXSelectBase<DocumentAddress>) this.Addresses).SelectSingle(new object[1]
    {
      (object) current.DefAddressID
    });
    PX.Objects.CR.Extensions.CRContactAccountDataSync.CRContactAccountDataSync<TGraph>.FillToDocumentContact(((PXSelectBase) this.Contacts).Cache, current, contact);
    PX.Objects.CR.Extensions.CRContactAccountDataSync.CRContactAccountDataSync<TGraph>.FillToDocumentAddress(((PXSelectBase) this.Addresses).Cache, docAddress, address);
  }

  private static void FillToDocumentContact(
    PXCache docContactCache,
    DocumentContact docContact,
    Contact contact)
  {
    if (contact == null || docContact == null)
      return;
    docContactCache.SetValue<DocumentContact.fullName>((object) docContact, (object) contact.FullName);
    docContactCache.SetValue<DocumentContact.title>((object) docContact, (object) contact.Title);
    docContactCache.SetValue<DocumentContact.firstName>((object) docContact, (object) contact.FirstName);
    docContactCache.SetValue<DocumentContact.lastName>((object) docContact, (object) contact.LastName);
    docContactCache.SetValue<DocumentContact.salutation>((object) docContact, (object) contact.Salutation);
    docContactCache.SetValue<DocumentContact.attention>((object) docContact, (object) contact.Attention);
    docContactCache.SetValue<DocumentContact.email>((object) docContact, (object) contact.EMail);
    docContactCache.SetValue<DocumentContact.webSite>((object) docContact, (object) contact.WebSite);
    docContactCache.SetValue<DocumentContact.phone1>((object) docContact, (object) contact.Phone1);
    docContactCache.SetValue<DocumentContact.phone1Type>((object) docContact, (object) contact.Phone1Type);
    docContactCache.SetValue<DocumentContact.phone2>((object) docContact, (object) contact.Phone2);
    docContactCache.SetValue<DocumentContact.phone2Type>((object) docContact, (object) contact.Phone2Type);
    docContactCache.SetValue<DocumentContact.phone3>((object) docContact, (object) contact.Phone3);
    docContactCache.SetValue<DocumentContact.phone3Type>((object) docContact, (object) contact.Phone3Type);
    docContactCache.SetValue<DocumentContact.fax>((object) docContact, (object) contact.Fax);
    docContactCache.SetValue<DocumentContact.faxType>((object) docContact, (object) contact.FaxType);
    docContactCache.SetValue<DocumentContact.consentAgreement>((object) docContact, (object) contact.ConsentAgreement);
    docContactCache.SetValue<DocumentContact.consentDate>((object) docContact, (object) contact.ConsentDate);
    docContactCache.SetValue<DocumentContact.consentExpirationDate>((object) docContact, (object) contact.ConsentExpirationDate);
  }

  private static void FillToDocumentAddress(
    PXCache docAddressCache,
    DocumentAddress docAddress,
    Address address)
  {
    if (address == null || docAddress == null)
      return;
    docAddressCache.SetValue<DocumentAddress.addressLine1>((object) docAddress, (object) address.AddressLine1);
    docAddressCache.SetValue<DocumentAddress.addressLine2>((object) docAddress, (object) address.AddressLine2);
    docAddressCache.SetValue<DocumentAddress.city>((object) docAddress, (object) address.City);
    docAddressCache.SetValue<DocumentAddress.countryID>((object) docAddress, (object) address.CountryID);
    docAddressCache.SetValue<DocumentAddress.state>((object) docAddress, (object) address.State);
    docAddressCache.SetValue<DocumentAddress.postalCode>((object) docAddress, (object) address.PostalCode);
    docAddressCache.Update((object) docAddress);
  }

  protected virtual WebDialogResult MapDocumentDialogAnswers()
  {
    switch (((PXSelectBase) this.Documents).View.Answer - 2)
    {
      case 0:
      case 3:
        return (WebDialogResult) 5;
      case 1:
      case 4:
        return (WebDialogResult) 3;
      case 2:
      case 5:
        return (WebDialogResult) 4;
      default:
        return ((PXSelectBase) this.Documents).View.Answer;
    }
  }

  protected class DocumentMapping : IBqlMapping
  {
    protected System.Type _table;
    public System.Type OverrideRefContact = typeof (Document.overrideRefContact);
    public System.Type RefContactID = typeof (Document.refContactID);
    public System.Type BAccountID = typeof (Document.bAccountID);

    public System.Type Extension => typeof (Document);

    public System.Type Table => this._table;

    public DocumentMapping(System.Type table) => this._table = table;
  }

  protected class DocumentContactMapping : IBqlMapping
  {
    protected System.Type _table;
    public System.Type FullName = typeof (DocumentContact.fullName);
    public System.Type Title = typeof (DocumentContact.title);
    public System.Type FirstName = typeof (DocumentContact.firstName);
    public System.Type LastName = typeof (DocumentContact.lastName);
    public System.Type Salutation = typeof (DocumentContact.salutation);
    public System.Type Attention = typeof (DocumentContact.attention);
    public System.Type Email = typeof (DocumentContact.email);
    public System.Type Phone1 = typeof (DocumentContact.phone1);
    public System.Type Phone1Type = typeof (DocumentContact.phone1Type);
    public System.Type Phone2 = typeof (DocumentContact.phone2);
    public System.Type Phone2Type = typeof (DocumentContact.phone2Type);
    public System.Type Phone3 = typeof (DocumentContact.phone3);
    public System.Type Phone3Type = typeof (DocumentContact.phone3Type);
    public System.Type Fax = typeof (DocumentContact.fax);
    public System.Type FaxType = typeof (DocumentContact.faxType);
    public System.Type OverrideContact = typeof (DocumentContact.overrideContact);
    public System.Type ConsentAgreement = typeof (DocumentContact.consentAgreement);
    public System.Type ConsentDate = typeof (DocumentContact.consentDate);
    public System.Type ConsentExpirationDate = typeof (DocumentContact.consentExpirationDate);
    public System.Type DefAddressID = typeof (DocumentContact.defAddressID);

    public System.Type Extension => typeof (DocumentContact);

    public System.Type Table => this._table;

    public DocumentContactMapping(System.Type table) => this._table = table;
  }

  protected class DocumentAddressMapping : IBqlMapping
  {
    protected System.Type _table;
    public System.Type OverrideAddress = typeof (DocumentAddress.overrideAddress);
    public System.Type AddressLine1 = typeof (DocumentAddress.addressLine1);
    public System.Type AddressLine2 = typeof (DocumentAddress.addressLine2);
    public System.Type AddressLine3 = typeof (DocumentAddress.addressLine3);
    public System.Type City = typeof (DocumentAddress.city);
    public System.Type CountryID = typeof (DocumentAddress.countryID);
    public System.Type State = typeof (DocumentAddress.state);
    public System.Type PostalCode = typeof (DocumentAddress.postalCode);
    public System.Type IsValidated = typeof (DocumentAddress.isValidated);

    public System.Type Extension => typeof (DocumentAddress);

    public System.Type Table => this._table;

    public DocumentAddressMapping(System.Type table) => this._table = table;
  }
}
