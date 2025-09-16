// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.ContactAddress.ContactAddressGraph`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Extensions.ContactAddress;

public abstract class ContactAddressGraph<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  public PXSelectExtension<Document> Documents;
  public PXSelectExtension<DocumentContact> Contacts;
  public PXSelectExtension<DocumentAddress> Addresses;

  protected abstract ContactAddressGraph<TGraph>.DocumentMapping GetDocumentMapping();

  protected abstract ContactAddressGraph<TGraph>.DocumentContactMapping GetDocumentContactMapping();

  protected abstract ContactAddressGraph<TGraph>.DocumentAddressMapping GetDocumentAddressMapping();

  protected virtual void _(
    PX.Data.Events.FieldUpdated<Document, Document.contactID> e)
  {
    Document row = e.Row;
    if (row == null || e.OldValue == e.NewValue)
      return;
    PX.Objects.CR.Contact contact = (PX.Objects.CR.Contact) null;
    Address address = (Address) null;
    ContactAddressGraph<TGraph>.ChangedData changedForContactInfo = new ContactAddressGraph<TGraph>.ChangedData();
    ContactAddressGraph<TGraph>.ChangedData changedForShippingInfo = new ContactAddressGraph<TGraph>.ChangedData(false);
    if (e.OldValue != null)
    {
      contact = (PX.Objects.CR.Contact) PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<Document.contactID>>>>.Config>.Select((PXGraph) this.Base, (object) (int?) e.OldValue);
      address = (Address) PXSelectBase<Address, PXSelectJoin<Address, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.defAddressID, Equal<Address.addressID>>>, Where<PX.Objects.CR.Contact.contactID, Equal<Required<Document.contactID>>>>.Config>.Select((PXGraph) this.Base, (object) (int?) e.OldValue);
    }
    else if (row.LocationID.HasValue)
    {
      contact = (PX.Objects.CR.Contact) PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, LeftJoin<Location, On<Location.locationID, Equal<Current<Document.locationID>>>>, Where<PX.Objects.CR.Contact.contactID, Equal<Location.defContactID>>>.Config>.Select((PXGraph) this.Base);
      address = (Address) PXSelectBase<Address, PXSelectJoin<Address, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.defAddressID, Equal<Address.addressID>>, LeftJoin<Location, On<Location.locationID, Equal<Current<Document.locationID>>>>>, Where<Address.addressID, Equal<Location.defAddressID>>>.Config>.Select((PXGraph) this.Base);
    }
    changedForContactInfo.OldContact = contact;
    changedForContactInfo.OldAddress = address;
    this.DefaultRecords(row, changedForContactInfo, changedForShippingInfo);
  }

  protected virtual void _(PX.Data.Events.RowInserted<Document> e)
  {
    Document row = e.Row;
    if (row == null)
      return;
    using (new ReadOnlyScope(((IEnumerable<PXCache>) new PXCache[4]
    {
      this.GetContactCache(),
      this.GetAddressCache(),
      this.GetShippingContactCache(),
      this.GetShippingAddressCache()
    }).Where<PXCache>((Func<PXCache, bool>) (cache => cache != null)).ToArray<PXCache>()))
    {
      PX.Objects.CR.Contact contact = (PX.Objects.CR.Contact) null;
      Address address = (Address) null;
      int? nullable = row.ContactID;
      if (nullable.HasValue)
      {
        contact = (PX.Objects.CR.Contact) PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Current<Document.contactID>>>>.Config>.Select((PXGraph) this.Base);
        address = (Address) PXSelectBase<Address, PXSelectJoin<Address, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.defAddressID, Equal<Address.addressID>>>, Where<PX.Objects.CR.Contact.contactID, Equal<Current<Document.contactID>>>>.Config>.Select((PXGraph) this.Base);
      }
      else
      {
        nullable = row.LocationID;
        if (nullable.HasValue)
        {
          contact = (PX.Objects.CR.Contact) PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, LeftJoin<Location, On<Location.locationID, Equal<Current<Document.locationID>>>>, Where<PX.Objects.CR.Contact.contactID, Equal<Location.defContactID>>>.Config>.Select((PXGraph) this.Base);
          address = (Address) PXSelectBase<Address, PXSelectJoin<Address, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.defAddressID, Equal<Address.addressID>>, LeftJoin<Location, On<Location.locationID, Equal<Current<Document.locationID>>>>>, Where<Address.addressID, Equal<Location.defAddressID>>>.Config>.Select((PXGraph) this.Base);
        }
      }
      this.DefaultRecords(row, new ContactAddressGraph<TGraph>.ChangedData()
      {
        OldContact = contact,
        OldAddress = address
      }, new ContactAddressGraph<TGraph>.ChangedData()
      {
        OldContact = contact,
        OldAddress = address
      });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<Document, Document.locationID> e)
  {
    Document row = e.Row;
    if (row == null || e.OldValue == e.NewValue)
      return;
    ContactAddressGraph<TGraph>.ChangedData changedForShippingInfo = new ContactAddressGraph<TGraph>.ChangedData();
    ContactAddressGraph<TGraph>.ChangedData changedForContactInfo = new ContactAddressGraph<TGraph>.ChangedData(false);
    int? oldValue = (int?) e.OldValue;
    if (oldValue.HasValue)
    {
      changedForShippingInfo.OldContact = (PX.Objects.CR.Contact) PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, LeftJoin<Location, On<Location.locationID, Equal<Required<Document.locationID>>>>, Where<PX.Objects.CR.Contact.contactID, Equal<Location.defContactID>>>.Config>.Select((PXGraph) this.Base, (object) oldValue);
      changedForShippingInfo.OldAddress = (Address) PXSelectBase<Address, PXSelectJoin<Address, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.defAddressID, Equal<Address.addressID>>, LeftJoin<Location, On<Location.locationID, Equal<Required<Document.locationID>>>>>, Where<Address.addressID, Equal<Location.defAddressID>>>.Config>.Select((PXGraph) this.Base, (object) oldValue);
    }
    Location location = (Location) PXSelectBase<Location, PXSelect<Location, Where<Location.locationID, Equal<Required<Location.locationID>>>>.Config>.Select((PXGraph) this.Base, (object) oldValue);
    int? baccountId1 = row.BAccountID;
    int? baccountId2 = (int?) location?.BAccountID;
    if (!(baccountId1.GetValueOrDefault() == baccountId2.GetValueOrDefault() & baccountId1.HasValue == baccountId2.HasValue))
      changedForContactInfo = changedForShippingInfo;
    if (!row.LocationID.HasValue)
      return;
    this.DefaultRecords(row, changedForContactInfo, changedForShippingInfo);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<Document, Document.bAccountID> e)
  {
    Document row = e.Row;
    if (row == null || e.OldValue == e.NewValue)
      return;
    ContactAddressGraph<TGraph>.ChangedData changedForContactInfo = new ContactAddressGraph<TGraph>.ChangedData();
    ContactAddressGraph<TGraph>.ChangedData changedForShippingInfo = new ContactAddressGraph<TGraph>.ChangedData();
    int? nullable1 = row.BAccountID;
    if (nullable1.HasValue)
    {
      int? oldValue = (int?) e.OldValue;
      int? nullable2 = new int?();
      PX.Objects.CR.BAccount baccount = (PX.Objects.CR.BAccount) PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>>>.Config>.Select((PXGraph) this.Base, (object) oldValue);
      if (baccount != null)
      {
        Location location = (Location) PXSelectBase<Location, PXSelect<Location, Where<Location.locationID, Equal<Required<Location.locationID>>>>.Config>.Select((PXGraph) this.Base, (object) baccount.DefLocationID);
        if (location != null)
          nullable2 = location.LocationID;
      }
      nullable1 = row.ContactID;
      if (nullable1.HasValue)
      {
        changedForContactInfo.OldContact = (PX.Objects.CR.Contact) PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Current<Document.contactID>>>>.Config>.Select((PXGraph) this.Base);
        changedForContactInfo.OldAddress = (Address) PXSelectBase<Address, PXSelectJoin<Address, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.defAddressID, Equal<Address.addressID>>>, Where<PX.Objects.CR.Contact.contactID, Equal<Current<Document.contactID>>>>.Config>.Select((PXGraph) this.Base);
      }
      if (nullable2.HasValue)
      {
        changedForShippingInfo.OldContact = (PX.Objects.CR.Contact) PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, LeftJoin<Location, On<Location.locationID, Equal<Required<Document.locationID>>>>, Where<PX.Objects.CR.Contact.contactID, Equal<Location.defContactID>>>.Config>.Select((PXGraph) this.Base, (object) nullable2);
        changedForShippingInfo.OldAddress = (Address) PXSelectBase<Address, PXSelectJoin<Address, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.defAddressID, Equal<Address.addressID>>, LeftJoin<Location, On<Location.locationID, Equal<Required<Document.locationID>>>>>, Where<Address.addressID, Equal<Location.defAddressID>>>.Config>.Select((PXGraph) this.Base, (object) nullable2);
        nullable1 = row.ContactID;
        if (!nullable1.HasValue)
          changedForContactInfo = changedForShippingInfo;
      }
    }
    this.DefaultRecords(row, changedForContactInfo, changedForShippingInfo);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<Document, Document.allowOverrideContactAddress> e)
  {
    Document row = e.Row;
    if (row == null)
      return;
    if (this.Contacts.Current != null)
      this.Contacts.Cache.SetValue<DocumentContact.overrideContact>((object) this.Contacts.Current, (object) row.AllowOverrideContactAddress);
    if (this.Addresses.Current == null)
      return;
    this.Addresses.Cache.SetValue<DocumentAddress.overrideAddress>((object) this.Addresses.Current, (object) row.AllowOverrideContactAddress);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<Document, Document.allowOverrideContactAddress> e)
  {
    Document row = e.Row;
    if (row == null)
      return;
    DocumentAddress data1 = this.Addresses.SelectSingle();
    DocumentContact data2 = this.Contacts.SelectSingle();
    bool? overrideContactAddress;
    if (data2 != null)
    {
      this.Contacts.Cache.SetValue<DocumentContact.overrideContact>((object) data2, (object) row.AllowOverrideContactAddress);
      PXCache contactCache = this.GetContactCache();
      if (contactCache != null)
      {
        IPersonalContact currentContact = this.GetCurrentContact();
        if (currentContact != null)
        {
          PXCache pxCache = contactCache;
          IPersonalContact data3 = currentContact;
          overrideContactAddress = row.AllowOverrideContactAddress;
          // ISSUE: variable of a boxed type
          __Boxed<bool> local = (ValueType) !overrideContactAddress.GetValueOrDefault();
          pxCache.SetValue<CRContact.isDefaultContact>((object) data3, (object) local);
        }
      }
    }
    if (data1 != null)
    {
      this.Addresses.Cache.SetValue<DocumentAddress.overrideAddress>((object) data1, (object) row.AllowOverrideContactAddress);
      PXCache addressCache = this.GetAddressCache();
      if (addressCache != null)
      {
        IAddress currentAddress = this.GetCurrentAddress();
        if (currentAddress != null)
        {
          PXCache pxCache = addressCache;
          IAddress data4 = currentAddress;
          overrideContactAddress = row.AllowOverrideContactAddress;
          // ISSUE: variable of a boxed type
          __Boxed<bool> local = (ValueType) !overrideContactAddress.GetValueOrDefault();
          pxCache.SetValue<CRAddress.isDefaultAddress>((object) data4, (object) local);
        }
      }
    }
    this.Addresses.Cache.Update((object) data1);
    this.Contacts.Cache.Update((object) data2);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<Document, Document.allowOverrideShippingContactAddress> e)
  {
    Document row = e.Row;
    if (row == null)
      return;
    PXCache shippingContactCache = this.GetShippingContactCache();
    if (shippingContactCache != null)
    {
      PXCache contactCache = this.GetContactCache();
      if (contactCache.Current == null)
        contactCache.Current = (object) this.GetCurrentContact();
      if (shippingContactCache.Current == null)
        shippingContactCache.Current = (object) this.GetCurrentShippingContact();
      if (shippingContactCache.Current is IPersonalContact current)
      {
        shippingContactCache.SetValueExt<CRShippingContact.overrideContact>((object) current, (object) row.AllowOverrideShippingContactAddress);
        if (shippingContactCache.Current == null)
        {
          shippingContactCache.Clear();
          shippingContactCache.Current = (object) this.GetCurrentShippingContact();
        }
      }
    }
    PXCache shippingAddressCache = this.GetShippingAddressCache();
    if (shippingAddressCache == null)
      return;
    PXCache addressCache = this.GetAddressCache();
    if (addressCache.Current == null)
      addressCache.Current = (object) this.GetCurrentAddress();
    IAddress currentShippingAddress = this.GetCurrentShippingAddress();
    if (currentShippingAddress == null)
      return;
    shippingAddressCache.SetValueExt<CRShippingAddress.overrideAddress>((object) currentShippingAddress, (object) row.AllowOverrideShippingContactAddress);
    if (shippingAddressCache.Current != null)
      return;
    shippingAddressCache.Clear();
    shippingAddressCache.Current = (object) this.GetCurrentShippingAddress();
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<Document, Document.allowOverrideContactAddress> e)
  {
    Document row = e.Row;
    if (row == null || row.BAccountID.HasValue || row.LocationID.HasValue || row.ContactID.HasValue)
      return;
    e.ReturnValue = (object) false;
  }

  protected virtual void _(PX.Data.Events.RowUpdated<DocumentAddress> e)
  {
    DocumentAddress row = e.Row;
    if (row == null || !(this.Documents.Cache.Current is Document current) || current.BAccountID.HasValue || current.ContactID.HasValue)
      return;
    row.IsValidated = new bool?(false);
  }

  protected virtual bool IsThereSomeContactAddressSourceValue(PXCache cache, Document row)
  {
    return row.LocationID.HasValue || row.ContactID.HasValue;
  }

  protected virtual void DefaultRecords(
    Document row,
    ContactAddressGraph<TGraph>.ChangedData changedForContactInfo,
    ContactAddressGraph<TGraph>.ChangedData changedForShippingInfo)
  {
    PXCache cache = this.Documents.Cache;
    bool flag1 = this.AskForConfirmationForAddress(row, changedForContactInfo);
    bool flag2 = this.AskForConfirmationForShippingAddress(row, changedForShippingInfo);
    int? nullable1;
    bool? nullable2;
    if (flag1 | flag2)
    {
      nullable1 = row.LocationID;
      if (!nullable1.HasValue)
      {
        nullable1 = row.ContactID;
        if (!nullable1.HasValue)
          goto label_13;
      }
      switch (this.Documents.View.Ask((object) null, "Warning", flag1 & flag2 ? "Would you like the overridden settings on the Contact and Shipping tabs to be replaced with the settings from the newly selected entity?" : (flag1 ? "Would you like the overridden settings on the Contact tab to be replaced with the settings from the newly selected entity?" : "Would you like the overridden settings on the Shipping tab to be replaced with the settings from the newly selected entity?"), MessageButtons.YesNoCancel, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
      {
        {
          WebDialogResult.Yes,
          "Yes"
        },
        {
          WebDialogResult.No,
          "No"
        },
        {
          WebDialogResult.Cancel,
          "Cancel"
        }
      }, MessageIcon.Warning))
      {
        case WebDialogResult.Cancel:
          cache.SetValue<Document.bAccountID>((object) row, cache.GetValueOriginal<Document.bAccountID>(cache.GetMain<Document>(row)));
          cache.SetValue<Document.contactID>((object) row, cache.GetValueOriginal<Document.contactID>(cache.GetMain<Document>(row)));
          goto label_18;
        case WebDialogResult.Yes:
          if (flag1)
          {
            SharedRecordAttribute.DefaultRecord<Document.documentAddressID>(cache, cache.GetMain<Document>(row));
            SharedRecordAttribute.DefaultRecord<Document.documentContactID>(cache, cache.GetMain<Document>(row));
            cache.SetValue<Document.allowOverrideContactAddress>((object) row, (object) false);
          }
          if (flag2)
          {
            SharedRecordAttribute.DefaultRecord<Document.shipContactID>(cache, cache.GetMain<Document>(row));
            SharedRecordAttribute.DefaultRecord<Document.shipAddressID>(cache, cache.GetMain<Document>(row));
            cache.SetValue<Document.allowOverrideShippingContactAddress>((object) row, (object) false);
            goto label_18;
          }
          goto label_18;
        case WebDialogResult.No:
          nullable2 = row.AllowOverrideContactAddress;
          bool flag3 = false;
          if (nullable2.GetValueOrDefault() == flag3 & nullable2.HasValue)
          {
            SharedRecordAttribute.DefaultRecord<Document.documentAddressID>(cache, cache.GetMain<Document>(row));
            SharedRecordAttribute.DefaultRecord<Document.documentContactID>(cache, cache.GetMain<Document>(row));
            cache.SetValue<Document.allowOverrideContactAddress>((object) row, (object) false);
            goto label_18;
          }
          nullable2 = row.AllowOverrideShippingContactAddress;
          bool flag4 = false;
          if (nullable2.GetValueOrDefault() == flag4 & nullable2.HasValue)
          {
            SharedRecordAttribute.DefaultRecord<Document.shipContactID>(cache, cache.GetMain<Document>(row));
            SharedRecordAttribute.DefaultRecord<Document.shipAddressID>(cache, cache.GetMain<Document>(row));
            cache.SetValue<Document.allowOverrideShippingContactAddress>((object) row, (object) false);
            goto label_18;
          }
          goto label_18;
        default:
          goto label_18;
      }
    }
label_13:
    if (this.IsThereSomeContactAddressSourceValue(cache, row))
    {
      if (changedForContactInfo.CanBeReplace)
      {
        SharedRecordAttribute.DefaultRecord<Document.documentAddressID>(cache, cache.GetMain<Document>(row));
        SharedRecordAttribute.DefaultRecord<Document.documentContactID>(cache, cache.GetMain<Document>(row));
        cache.SetValue<Document.allowOverrideContactAddress>((object) row, (object) false);
      }
      if (changedForShippingInfo.CanBeReplace)
      {
        SharedRecordAttribute.DefaultRecord<Document.shipContactID>(cache, cache.GetMain<Document>(row));
        SharedRecordAttribute.DefaultRecord<Document.shipAddressID>(cache, cache.GetMain<Document>(row));
        cache.SetValue<Document.allowOverrideShippingContactAddress>((object) row, (object) false);
      }
    }
label_18:
    nullable1 = row.LocationID;
    if (!nullable1.HasValue)
    {
      nullable1 = row.ContactID;
      if (!nullable1.HasValue)
      {
        nullable1 = row.BAccountID;
        if (!nullable1.HasValue)
        {
          nullable2 = row.AllowOverrideContactAddress;
          bool flag5 = false;
          if (nullable2.GetValueOrDefault() == flag5 & nullable2.HasValue)
          {
            SharedRecordAttribute.DefaultRecord<Document.documentAddressID>(cache, cache.GetMain<Document>(row));
            SharedRecordAttribute.DefaultRecord<Document.documentContactID>(cache, cache.GetMain<Document>(row));
            cache.SetValue<Document.allowOverrideContactAddress>((object) row, (object) false);
          }
          nullable2 = row.AllowOverrideShippingContactAddress;
          bool flag6 = false;
          if (nullable2.GetValueOrDefault() == flag6 & nullable2.HasValue)
          {
            SharedRecordAttribute.DefaultRecord<Document.shipContactID>(cache, cache.GetMain<Document>(row));
            SharedRecordAttribute.DefaultRecord<Document.shipAddressID>(cache, cache.GetMain<Document>(row));
            cache.SetValue<Document.allowOverrideShippingContactAddress>((object) row, (object) false);
            PXCache contactCache = this.GetContactCache();
            IPersonalContact currentContact = this.GetCurrentContact();
            IPersonalContact newItem1 = currentContact;
            IPersonalContact oldItem1 = currentContact;
            contactCache.RaiseRowUpdated((object) newItem1, (object) oldItem1);
            PXCache addressCache = this.GetAddressCache();
            IAddress currentAddress = this.GetCurrentAddress();
            IAddress newItem2 = currentAddress;
            IAddress oldItem2 = currentAddress;
            addressCache.RaiseRowUpdated((object) newItem2, (object) oldItem2);
          }
        }
      }
    }
    if (!this.IsDefaultContactAdress())
      return;
    cache.SetValue<Document.allowOverrideContactAddress>((object) row, (object) true);
  }

  protected virtual bool AskForConfirmationForAddress(
    Document row,
    ContactAddressGraph<TGraph>.ChangedData data)
  {
    return row.AllowOverrideContactAddress.GetValueOrDefault() && !this.IsDefaultContactAdress() && this.IsThereSomeContactAddressSourceValue(this.Documents.Cache, row) && !this.IsContactAddressNoChanged(data);
  }

  protected virtual bool AskForConfirmationForShippingAddress(
    Document row,
    ContactAddressGraph<TGraph>.ChangedData data)
  {
    return row.AllowOverrideShippingContactAddress.GetValueOrDefault() && !this.IsDefaultShippingContactAdress() && !this.IsShippingContactAddressNoChanged(data);
  }

  protected abstract IPersonalContact GetCurrentContact();

  protected abstract IPersonalContact GetCurrentShippingContact();

  protected abstract IPersonalContact GetEtalonContact();

  protected abstract IPersonalContact GetEtalonShippingContact();

  protected abstract IAddress GetCurrentAddress();

  protected abstract IAddress GetCurrentShippingAddress();

  protected abstract IAddress GetEtalonAddress();

  protected abstract IAddress GetEtalonShippingAddress();

  protected abstract PXCache GetContactCache();

  protected abstract PXCache GetAddressCache();

  protected abstract PXCache GetShippingContactCache();

  protected abstract PXCache GetShippingAddressCache();

  protected bool IsDefaultContactAdress()
  {
    IPersonalContact currentContact = this.GetCurrentContact();
    IAddress currentAddress = this.GetCurrentAddress();
    if (currentContact != null && currentAddress != null)
    {
      IAddress etalonAddress = this.GetEtalonAddress();
      IPersonalContact etalonContact = this.GetEtalonContact();
      if (currentContact.FullName != etalonContact.FullName || currentContact.Title != etalonContact.Title || currentContact.FirstName != etalonContact.FirstName || currentContact.LastName != etalonContact.LastName || currentContact.Salutation != etalonContact.Salutation || currentContact.Attention != etalonContact.Attention || currentContact.Email != etalonContact.Email || currentContact.Phone1 != etalonContact.Phone1 || currentContact.Phone1Type != etalonContact.Phone1Type || currentContact.Phone2 != etalonContact.Phone2 || currentContact.Phone2Type != etalonContact.Phone2Type || currentContact.Phone3 != etalonContact.Phone3 || currentContact.Phone3Type != etalonContact.Phone3Type || currentContact.Fax != etalonContact.Fax || currentContact.FaxType != etalonContact.FaxType || ((IAddressBase) currentAddress).AddressLine1 != ((IAddressBase) etalonAddress).AddressLine1 || ((IAddressBase) currentAddress).AddressLine2 != ((IAddressBase) etalonAddress).AddressLine2 || ((IAddressBase) currentAddress).City != ((IAddressBase) etalonAddress).City || ((IAddressBase) currentAddress).State != ((IAddressBase) etalonAddress).State || ((IAddressBase) currentAddress).CountryID != ((IAddressBase) etalonAddress).CountryID || ((IAddressBase) currentAddress).PostalCode != ((IAddressBase) etalonAddress).PostalCode)
        return false;
    }
    return true;
  }

  protected bool IsContactAddressNoChanged(
    ContactAddressGraph<TGraph>.ChangedData replaceData)
  {
    if (replaceData == null || !replaceData.CanBeReplace)
      return true;
    PX.Objects.CR.Contact oldContact = replaceData.OldContact;
    Address oldAddress = replaceData.OldAddress;
    if (oldContact == null || oldAddress == null)
      return false;
    IPersonalContact currentContact = this.GetCurrentContact();
    IAddress currentAddress = this.GetCurrentAddress();
    return currentContact != null && currentAddress != null && !(currentContact.FullName != oldContact.FullName) && !(currentContact.Title != oldContact.Title) && !(currentContact.LastName != oldContact.LastName) && !(currentContact.FirstName != oldContact.FirstName) && !(currentContact.Salutation != oldContact.Salutation) && !(currentContact.Attention != oldContact.Attention) && !(currentContact.Email != oldContact.EMail) && !(currentContact.Phone1 != oldContact.Phone1) && !(currentContact.Phone1Type != oldContact.Phone1Type) && !(currentContact.Phone2 != oldContact.Phone2) && !(currentContact.Phone2Type != oldContact.Phone2Type) && !(currentContact.Phone3 != oldContact.Phone3) && !(currentContact.Phone3Type != oldContact.Phone3Type) && !(currentContact.Fax != oldContact.Fax) && !(currentContact.FaxType != oldContact.FaxType) && !(((IAddressBase) currentAddress).AddressLine1 != oldAddress.AddressLine1) && !(((IAddressBase) currentAddress).AddressLine2 != oldAddress.AddressLine2) && !(((IAddressBase) currentAddress).City != oldAddress.City) && !(((IAddressBase) currentAddress).State != oldAddress.State) && !(((IAddressBase) currentAddress).CountryID != oldAddress.CountryID) && !(((IAddressBase) currentAddress).PostalCode != oldAddress.PostalCode);
  }

  protected bool IsDefaultShippingContactAdress()
  {
    IPersonalContact currentShippingContact = this.GetCurrentShippingContact();
    IAddress currentShippingAddress = this.GetCurrentShippingAddress();
    if (currentShippingContact != null && currentShippingAddress != null)
    {
      IAddress etalonShippingAddress = this.GetEtalonShippingAddress();
      IPersonalContact etalonShippingContact = this.GetEtalonShippingContact();
      if (currentShippingContact.FullName != etalonShippingContact.FullName || currentShippingContact.Attention != etalonShippingContact.Attention || currentShippingContact.Email != etalonShippingContact.Email || currentShippingContact.Phone1 != etalonShippingContact.Phone1 || currentShippingContact.Phone1Type != etalonShippingContact.Phone1Type || currentShippingContact.Phone2 != etalonShippingContact.Phone2 || currentShippingContact.Phone2Type != etalonShippingContact.Phone2Type || ((IAddressBase) currentShippingAddress).AddressLine1 != ((IAddressBase) etalonShippingAddress).AddressLine1 || ((IAddressBase) currentShippingAddress).AddressLine2 != ((IAddressBase) etalonShippingAddress).AddressLine2 || ((IAddressBase) currentShippingAddress).City != ((IAddressBase) etalonShippingAddress).City || ((IAddressBase) currentShippingAddress).State != ((IAddressBase) etalonShippingAddress).State || ((IAddressBase) currentShippingAddress).CountryID != ((IAddressBase) etalonShippingAddress).CountryID || ((IAddressBase) currentShippingAddress).PostalCode != ((IAddressBase) etalonShippingAddress).PostalCode)
        return false;
    }
    return true;
  }

  protected bool IsShippingContactAddressNoChanged(
    ContactAddressGraph<TGraph>.ChangedData replaceData)
  {
    if (replaceData == null || !replaceData.CanBeReplace)
      return true;
    PX.Objects.CR.Contact oldContact = replaceData.OldContact;
    Address oldAddress = replaceData.OldAddress;
    if (oldContact == null || oldAddress == null)
      return false;
    IPersonalContact currentShippingContact = this.GetCurrentShippingContact();
    IAddress currentShippingAddress = this.GetCurrentShippingAddress();
    return currentShippingContact != null && currentShippingAddress != null && !(currentShippingContact.FullName != oldContact.FullName) && !(currentShippingContact.Attention != oldContact.Attention) && !(currentShippingContact.Email != oldContact.EMail) && !(currentShippingContact.Phone1 != oldContact.Phone1) && !(currentShippingContact.Phone1Type != oldContact.Phone1Type) && !(currentShippingContact.Phone2 != oldContact.Phone2) && !(currentShippingContact.Phone2Type != oldContact.Phone2Type) && !(((IAddressBase) currentShippingAddress).AddressLine1 != oldAddress.AddressLine1) && !(((IAddressBase) currentShippingAddress).AddressLine2 != oldAddress.AddressLine2) && !(((IAddressBase) currentShippingAddress).City != oldAddress.City) && !(((IAddressBase) currentShippingAddress).State != oldAddress.State) && !(((IAddressBase) currentShippingAddress).CountryID != oldAddress.CountryID) && !(((IAddressBase) currentShippingAddress).PostalCode != oldAddress.PostalCode);
  }

  protected class DocumentMapping : IBqlMapping
  {
    protected System.Type _table;
    public System.Type ContactID = typeof (Document.contactID);
    public System.Type DocumentContactID = typeof (Document.documentContactID);
    public System.Type DocumentAddressID = typeof (Document.documentAddressID);
    public System.Type ShipContactID = typeof (Document.shipContactID);
    public System.Type ShipAddressID = typeof (Document.shipAddressID);
    public System.Type LocationID = typeof (Document.locationID);
    public System.Type BAccountID = typeof (Document.bAccountID);
    public System.Type AllowOverrideContactAddress = typeof (Document.allowOverrideContactAddress);
    public System.Type AllowOverrideShippingContactAddress = typeof (Document.allowOverrideShippingContactAddress);

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
    public System.Type EMail = typeof (DocumentContact.email);
    public System.Type Phone1 = typeof (DocumentContact.phone1);
    public System.Type Phone1Type = typeof (DocumentContact.phone1Type);
    public System.Type Phone2 = typeof (DocumentContact.phone2);
    public System.Type Phone2Type = typeof (DocumentContact.phone2Type);
    public System.Type Phone3 = typeof (DocumentContact.phone3);
    public System.Type Phone3Type = typeof (DocumentContact.phone3Type);
    public System.Type Fax = typeof (DocumentContact.fax);
    public System.Type FaxType = typeof (DocumentContact.faxType);
    public System.Type IsDefaultContact = typeof (DocumentContact.isDefaultContact);
    public System.Type OverrideContact = typeof (DocumentContact.overrideContact);

    public System.Type Extension => typeof (DocumentContact);

    public System.Type Table => this._table;

    public DocumentContactMapping(System.Type table) => this._table = table;
  }

  protected class DocumentAddressMapping : IBqlMapping
  {
    protected System.Type _table;
    public System.Type IsDefaultAddress = typeof (DocumentAddress.isDefaultAddress);
    public System.Type OverrideAddress = typeof (DocumentAddress.overrideAddress);
    public System.Type AddressLine1 = typeof (DocumentAddress.addressLine1);
    public System.Type AddressLine2 = typeof (DocumentAddress.addressLine2);
    public System.Type AddressLine3 = typeof (DocumentAddress.addressLine3);
    public System.Type City = typeof (DocumentAddress.city);
    public System.Type CountryID = typeof (DocumentAddress.countryID);
    public System.Type State = typeof (DocumentAddress.state);
    public System.Type PostalCode = typeof (DocumentAddress.postalCode);
    public System.Type IsValidated = typeof (DocumentAddress.isValidated);
    public System.Type Department = typeof (DocumentAddress.department);
    public System.Type SubDepartment = typeof (DocumentAddress.subDepartment);
    public System.Type StreetName = typeof (DocumentAddress.streetName);
    public System.Type BuildingNumber = typeof (DocumentAddress.buildingNumber);
    public System.Type BuildingName = typeof (DocumentAddress.buildingName);
    public System.Type Floor = typeof (DocumentAddress.floor);
    public System.Type UnitNumber = typeof (DocumentAddress.unitNumber);
    public System.Type PostBox = typeof (DocumentAddress.postBox);
    public System.Type Room = typeof (DocumentAddress.room);
    public System.Type TownLocationName = typeof (DocumentAddress.townLocationName);
    public System.Type DistrictName = typeof (DocumentAddress.districtName);

    public System.Type Extension => typeof (DocumentAddress);

    public System.Type Table => this._table;

    public DocumentAddressMapping(System.Type table) => this._table = table;
  }

  protected class ChangedData
  {
    public ChangedData(bool canBeReplace = true) => this.CanBeReplace = canBeReplace;

    public bool CanBeReplace { get; set; }

    public PX.Objects.CR.Contact OldContact { get; set; }

    public Address OldAddress { get; set; }

    [Obsolete("This field is obsolete and will be removed in 2025R1")]
    public bool? OverrideAddress { get; set; }

    [Obsolete("This field is obsolete and will be removed in 2025R1")]
    public bool? OverrideContact { get; set; }
  }
}
