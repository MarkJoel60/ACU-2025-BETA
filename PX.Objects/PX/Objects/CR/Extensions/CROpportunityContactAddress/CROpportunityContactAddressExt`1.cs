// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CROpportunityContactAddress.CROpportunityContactAddressExt`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.Extensions.CROpportunityContactAddress;

[PXInternalUseOnly]
public abstract class CROpportunityContactAddressExt<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  public PXSelectExtension<Document> Documents;
  public PXSelectExtension<DocumentContact> Contacts;
  public PXSelectExtension<DocumentAddress> Addresses;

  protected abstract CROpportunityContactAddressExt<TGraph>.DocumentMapping GetDocumentMapping();

  protected abstract CROpportunityContactAddressExt<TGraph>.DocumentContactMapping GetDocumentContactMapping();

  protected abstract CROpportunityContactAddressExt<TGraph>.DocumentAddressMapping GetDocumentAddressMapping();

  [PXOverride]
  public virtual void Persist(Action del)
  {
    this.PreventSameItemsDeletion();
    del();
  }

  protected virtual void PreventSameItemsDeletion()
  {
    HashSet<int> intSet = new HashSet<int>();
    foreach (PXCache pxCache in ((IEnumerable<System.Type>) new System.Type[3]
    {
      typeof (CRContact),
      typeof (CRBillingContact),
      typeof (CRShippingContact)
    }).Select<System.Type, PXCache>((Func<System.Type, PXCache>) (type => this.Base.Caches[type])))
    {
      foreach (CRContact crContact in pxCache.Deleted.OfType<CRContact>())
      {
        int? contactId = crContact.ContactID;
        if (contactId.HasValue)
        {
          int valueOrDefault = contactId.GetValueOrDefault();
          if (valueOrDefault > 0 && !intSet.Add(valueOrDefault))
            pxCache.Remove((object) crContact);
        }
      }
    }
    intSet.Clear();
    foreach (PXCache pxCache in ((IEnumerable<System.Type>) new System.Type[3]
    {
      typeof (CRAddress),
      typeof (CRBillingAddress),
      typeof (CRShippingAddress)
    }).Select<System.Type, PXCache>((Func<System.Type, PXCache>) (type => this.Base.Caches[type])))
    {
      foreach (CRAddress crAddress in pxCache.Deleted.OfType<CRAddress>())
      {
        int? addressId = crAddress.AddressID;
        if (addressId.HasValue)
        {
          int valueOrDefault = addressId.GetValueOrDefault();
          if (valueOrDefault > 0 && !intSet.Add(valueOrDefault))
            pxCache.Remove((object) crAddress);
        }
      }
    }
  }

  protected virtual void _(
    Events.FieldUpdated<Document, Document.contactID> e)
  {
    Document row1 = e.Row;
    if (row1 == null || object.Equals(((Events.FieldUpdatedBase<Events.FieldUpdated<Document, Document.contactID>, Document, object>) e).OldValue, e.NewValue))
      return;
    Contact oldContact = (Contact) null;
    Address oldAddress = (Address) null;
    if (((Events.FieldUpdatedBase<Events.FieldUpdated<Document, Document.contactID>, Document, object>) e).OldValue != null)
    {
      oldContact = PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelect<Contact, Where<Contact.contactID, Equal<Required<Document.contactID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) (int?) ((Events.FieldUpdatedBase<Events.FieldUpdated<Document, Document.contactID>, Document, object>) e).OldValue
      }));
      oldAddress = PXResultset<Address>.op_Implicit(PXSelectBase<Address, PXSelectJoin<Address, LeftJoin<Contact, On<Contact.defAddressID, Equal<Address.addressID>>>, Where<Contact.contactID, Equal<Required<Document.contactID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) (int?) ((Events.FieldUpdatedBase<Events.FieldUpdated<Document, Document.contactID>, Document, object>) e).OldValue
      }));
    }
    else if (row1.LocationID.HasValue)
    {
      oldContact = PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelectJoin<Contact, LeftJoin<Location, On<Location.locationID, Equal<Current<Document.locationID>>>>, Where<Contact.contactID, Equal<Location.defContactID>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
      oldAddress = PXResultset<Address>.op_Implicit(PXSelectBase<Address, PXSelectJoin<Address, LeftJoin<Contact, On<Contact.defAddressID, Equal<Address.addressID>>, LeftJoin<Location, On<Location.locationID, Equal<Current<Document.locationID>>>>>, Where<Address.addressID, Equal<Location.defAddressID>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    }
    Document row2 = row1;
    CROpportunityContactAddressExt<TGraph>.ChangedData changedForContactInfo = CROpportunityContactAddressExt<TGraph>.ChangedData.ShouldReplace(oldContact, oldAddress);
    CROpportunityContactAddressExt<TGraph>.ChangedData shouldNotReplace1 = CROpportunityContactAddressExt<TGraph>.ChangedData.ShouldNotReplace;
    CROpportunityContactAddressExt<TGraph>.ChangedData shouldNotReplace2 = CROpportunityContactAddressExt<TGraph>.ChangedData.ShouldNotReplace;
    CROpportunityContactAddressExt<TGraph>.ChangedData changedForShippingInfo = shouldNotReplace1;
    this.DefaultRecords(row2, changedForContactInfo, shouldNotReplace2, changedForShippingInfo);
  }

  protected virtual void _(Events.RowInserted<Document> e)
  {
    Document row1 = e.Row;
    if (row1 == null)
      return;
    using (new ReadOnlyScope(((IEnumerable<PXCache>) new PXCache[6]
    {
      this.GetContactCache(),
      this.GetAddressCache(),
      this.GetBillingContactCache(),
      this.GetBillingAddressCache(),
      this.GetShippingContactCache(),
      this.GetShippingAddressCache()
    }).Where<PXCache>((Func<PXCache, bool>) (cache => cache != null)).ToArray<PXCache>()))
    {
      Contact oldContact = (Contact) null;
      Address oldAddress = (Address) null;
      int? nullable = row1.ContactID;
      if (nullable.HasValue)
      {
        oldContact = PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelect<Contact, Where<Contact.contactID, Equal<Current<Document.contactID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
        oldAddress = PXResultset<Address>.op_Implicit(PXSelectBase<Address, PXSelectJoin<Address, LeftJoin<Contact, On<Contact.defAddressID, Equal<Address.addressID>>>, Where<Contact.contactID, Equal<Current<Document.contactID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
      }
      else
      {
        nullable = row1.LocationID;
        if (nullable.HasValue)
        {
          oldContact = PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelectJoin<Contact, LeftJoin<Location, On<Location.locationID, Equal<Current<Document.locationID>>>>, Where<Contact.contactID, Equal<Location.defContactID>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
          oldAddress = PXResultset<Address>.op_Implicit(PXSelectBase<Address, PXSelectJoin<Address, LeftJoin<Contact, On<Contact.defAddressID, Equal<Address.addressID>>, LeftJoin<Location, On<Location.locationID, Equal<Current<Document.locationID>>>>>, Where<Address.addressID, Equal<Location.defAddressID>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
        }
      }
      CROpportunityContactAddressExt<TGraph>.ChangedData changedData1 = CROpportunityContactAddressExt<TGraph>.ChangedData.ShouldReplace(oldContact, oldAddress);
      CROpportunityContactAddressExt<TGraph>.ChangedData changedData2 = CROpportunityContactAddressExt<TGraph>.ChangedData.ShouldReplace(oldContact, oldAddress);
      Document row2 = row1;
      CROpportunityContactAddressExt<TGraph>.ChangedData changedForContactInfo = changedData1;
      CROpportunityContactAddressExt<TGraph>.ChangedData changedData3 = changedData2;
      CROpportunityContactAddressExt<TGraph>.ChangedData shouldNotReplace = CROpportunityContactAddressExt<TGraph>.ChangedData.ShouldNotReplace;
      CROpportunityContactAddressExt<TGraph>.ChangedData changedForShippingInfo = changedData3;
      this.DefaultRecords(row2, changedForContactInfo, shouldNotReplace, changedForShippingInfo);
    }
  }

  protected virtual void _(
    Events.FieldUpdated<Document, Document.locationID> e)
  {
    Document row = e.Row;
    if (row == null || object.Equals(((Events.FieldUpdatedBase<Events.FieldUpdated<Document, Document.locationID>, Document, object>) e).OldValue, e.NewValue))
      return;
    Contact oldContact = (Contact) null;
    Address oldAddress = (Address) null;
    int? oldValue = (int?) ((Events.FieldUpdatedBase<Events.FieldUpdated<Document, Document.locationID>, Document, object>) e).OldValue;
    if (oldValue.HasValue)
    {
      oldContact = PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelectJoin<Contact, LeftJoin<Location, On<Location.locationID, Equal<Required<Document.locationID>>>>, Where<Contact.contactID, Equal<Location.defContactID>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) oldValue
      }));
      oldAddress = PXResultset<Address>.op_Implicit(PXSelectBase<Address, PXSelectJoin<Address, LeftJoin<Contact, On<Contact.defAddressID, Equal<Address.addressID>>, LeftJoin<Location, On<Location.locationID, Equal<Required<Document.locationID>>>>>, Where<Address.addressID, Equal<Location.defAddressID>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) oldValue
      }));
    }
    Location location = PXResultset<Location>.op_Implicit(PXSelectBase<Location, PXSelect<Location, Where<Location.locationID, Equal<Required<Location.locationID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) oldValue
    }));
    CROpportunityContactAddressExt<TGraph>.ChangedData changedForShippingInfo = CROpportunityContactAddressExt<TGraph>.ChangedData.ShouldReplace(oldContact, oldAddress);
    int? baccountId1 = row.BAccountID;
    int? baccountId2 = (int?) location?.BAccountID;
    CROpportunityContactAddressExt<TGraph>.ChangedData changedData = !(baccountId1.GetValueOrDefault() == baccountId2.GetValueOrDefault() & baccountId1.HasValue == baccountId2.HasValue) ? changedForShippingInfo : CROpportunityContactAddressExt<TGraph>.ChangedData.ShouldNotReplace;
    if (!row.LocationID.HasValue)
      return;
    this.DefaultRecords(row, changedData, changedData, changedForShippingInfo);
  }

  protected virtual void _(
    Events.FieldUpdated<Document, Document.bAccountID> e)
  {
    Document row = e.Row;
    if (row == null || object.Equals(((Events.FieldUpdatedBase<Events.FieldUpdated<Document, Document.bAccountID>, Document, object>) e).OldValue, e.NewValue))
      return;
    Contact oldContact = (Contact) null;
    Address oldAddress = (Address) null;
    if (row.BAccountID.HasValue)
    {
      int? oldValue = (int?) ((Events.FieldUpdatedBase<Events.FieldUpdated<Document, Document.bAccountID>, Document, object>) e).OldValue;
      int? nullable = new int?();
      BAccount baccount = PXResultset<BAccount>.op_Implicit(PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) oldValue
      }));
      if (baccount != null)
      {
        Location location = PXResultset<Location>.op_Implicit(PXSelectBase<Location, PXSelect<Location, Where<Location.locationID, Equal<Required<Location.locationID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) baccount.DefLocationID
        }));
        if (location != null)
          nullable = location.LocationID;
      }
      if (row.ContactID.HasValue)
      {
        oldContact = PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelect<Contact, Where<Contact.contactID, Equal<Current<Document.contactID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
        oldAddress = PXResultset<Address>.op_Implicit(PXSelectBase<Address, PXSelectJoin<Address, LeftJoin<Contact, On<Contact.defAddressID, Equal<Address.addressID>>>, Where<Contact.contactID, Equal<Current<Document.contactID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
      }
      if (nullable.HasValue)
      {
        oldContact = PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelectJoin<Contact, LeftJoin<Location, On<Location.locationID, Equal<Required<Document.locationID>>>>, Where<Contact.contactID, Equal<Location.defContactID>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) nullable
        }));
        oldAddress = PXResultset<Address>.op_Implicit(PXSelectBase<Address, PXSelectJoin<Address, LeftJoin<Contact, On<Contact.defAddressID, Equal<Address.addressID>>, LeftJoin<Location, On<Location.locationID, Equal<Required<Document.locationID>>>>>, Where<Address.addressID, Equal<Location.defAddressID>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) nullable
        }));
      }
    }
    CROpportunityContactAddressExt<TGraph>.ChangedData changedData = CROpportunityContactAddressExt<TGraph>.ChangedData.ShouldReplace(oldContact, oldAddress);
    this.DefaultRecords(row, changedData, changedData, changedData);
  }

  protected virtual void _(
    Events.FieldDefaulting<Document, Document.allowOverrideContactAddress> e)
  {
    Document row = e.Row;
    if (row == null)
      return;
    if (((PXSelectBase<DocumentContact>) this.Contacts).Current != null)
      ((PXSelectBase) this.Contacts).Cache.SetValue<DocumentContact.overrideContact>((object) ((PXSelectBase<DocumentContact>) this.Contacts).Current, (object) row.AllowOverrideContactAddress);
    if (((PXSelectBase<DocumentAddress>) this.Addresses).Current == null)
      return;
    ((PXSelectBase) this.Addresses).Cache.SetValue<DocumentAddress.overrideAddress>((object) ((PXSelectBase<DocumentAddress>) this.Addresses).Current, (object) row.AllowOverrideContactAddress);
  }

  protected virtual void _(
    Events.FieldUpdated<Document, Document.allowOverrideContactAddress> e)
  {
    Document row = e.Row;
    if (row == null)
      return;
    DocumentAddress documentAddress = ((PXSelectBase<DocumentAddress>) this.Addresses).SelectSingle(Array.Empty<object>());
    DocumentContact documentContact = ((PXSelectBase<DocumentContact>) this.Contacts).SelectSingle(Array.Empty<object>());
    bool? overrideContactAddress;
    if (documentContact != null)
    {
      ((PXSelectBase) this.Contacts).Cache.SetValue<DocumentContact.overrideContact>((object) documentContact, (object) row.AllowOverrideContactAddress);
      PXCache contactCache = this.GetContactCache();
      if (contactCache != null)
      {
        IPersonalContact currentContact = this.GetCurrentContact();
        if (currentContact != null)
        {
          PXCache pxCache = contactCache;
          IPersonalContact personalContact = currentContact;
          overrideContactAddress = row.AllowOverrideContactAddress;
          // ISSUE: variable of a boxed type
          __Boxed<bool> local = (ValueType) !overrideContactAddress.GetValueOrDefault();
          pxCache.SetValue<CRContact.isDefaultContact>((object) personalContact, (object) local);
        }
      }
    }
    if (documentAddress != null)
    {
      ((PXSelectBase) this.Addresses).Cache.SetValue<DocumentAddress.overrideAddress>((object) documentAddress, (object) row.AllowOverrideContactAddress);
      PXCache addressCache = this.GetAddressCache();
      if (addressCache != null)
      {
        IAddress currentAddress = this.GetCurrentAddress();
        if (currentAddress != null)
        {
          PXCache pxCache = addressCache;
          IAddress address = currentAddress;
          overrideContactAddress = row.AllowOverrideContactAddress;
          // ISSUE: variable of a boxed type
          __Boxed<bool> local = (ValueType) !overrideContactAddress.GetValueOrDefault();
          pxCache.SetValue<CRAddress.isDefaultAddress>((object) address, (object) local);
        }
      }
    }
    ((PXSelectBase) this.Addresses).Cache.Update((object) documentAddress);
    ((PXSelectBase) this.Contacts).Cache.Update((object) documentContact);
  }

  protected virtual void _(
    Events.FieldSelecting<Document, Document.allowOverrideContactAddress> e)
  {
    Document row = e.Row;
    if (row == null || row.BAccountID.HasValue || row.LocationID.HasValue || row.ContactID.HasValue)
      return;
    ((Events.FieldSelectingBase<Events.FieldSelecting<Document, Document.allowOverrideContactAddress>>) e).ReturnValue = (object) false;
  }

  protected virtual void _(
    Events.FieldUpdated<Document, Document.allowOverrideShippingContactAddress> e)
  {
    bool? shippingContactAddress = (bool?) e.Row?.AllowOverrideShippingContactAddress;
    if (!shippingContactAddress.HasValue)
      return;
    bool valueOrDefault = shippingContactAddress.GetValueOrDefault();
    this.AllowOverrides_Updated(this.GetShippingContactCache(), this.GetShippingAddressCache(), valueOrDefault);
  }

  protected virtual void _(
    Events.FieldUpdated<Document, Document.allowOverrideBillingContactAddress> e)
  {
    bool? billingContactAddress = (bool?) e.Row?.AllowOverrideBillingContactAddress;
    if (!billingContactAddress.HasValue)
      return;
    bool valueOrDefault = billingContactAddress.GetValueOrDefault();
    this.AllowOverrides_Updated(this.GetBillingContactCache(), this.GetBillingAddressCache(), valueOrDefault);
  }

  private void AllowOverrides_Updated(
    PXCache contactCache,
    PXCache addressCache,
    bool allowOverrideValue)
  {
    this.RefreshCurrents();
    if (contactCache?.Current is IPersonalContact current1)
      contactCache.SetValueExt<CRContact.overrideContact>((object) current1, (object) allowOverrideValue);
    if (!(addressCache?.Current is IAddress current2))
      return;
    addressCache.SetValueExt<CRAddress.overrideAddress>((object) current2, (object) allowOverrideValue);
  }

  protected virtual void _(Events.RowUpdated<DocumentAddress> e)
  {
    DocumentAddress row = e.Row;
    if (row == null || !(((PXSelectBase) this.Documents).Cache.Current is Document current) || current.BAccountID.HasValue || current.ContactID.HasValue)
      return;
    row.IsValidated = new bool?(false);
  }

  private bool LocationOrContactIsNotNull(Document row)
  {
    return row.LocationID.HasValue || row.ContactID.HasValue;
  }

  protected virtual void DefaultRecords(
    Document row,
    CROpportunityContactAddressExt<TGraph>.ChangedData changedForContactInfo,
    CROpportunityContactAddressExt<TGraph>.ChangedData changedForBillingInfo,
    CROpportunityContactAddressExt<TGraph>.ChangedData changedForShippingInfo)
  {
    PXCache cache = ((PXSelectBase) this.Documents).Cache;
    this.RefreshCurrents();
    bool needAskFromContactAddress = this.AskForConfirmationForContactAddress(row, changedForContactInfo);
    bool needAskFromBillingContact = this.AskForConfirmationForBillingContact(row, changedForBillingInfo);
    bool needAskFromBillingAddress = this.AskForConfirmationForBillingAddress(row, changedForBillingInfo);
    bool needAskFromShippingContact = this.AskForConfirmationForShippingContact(row, changedForShippingInfo);
    bool needAskFromShippingAddress = this.AskForConfirmationForShippingAddress(row, changedForShippingInfo);
    if (this.LocationOrContactIsNotNull(row))
    {
      string messageGeneral = this.GetMessageGeneral(needAskFromContactAddress, needAskFromBillingContact, needAskFromBillingAddress, needAskFromShippingContact, needAskFromShippingAddress);
      if (!string.IsNullOrEmpty(messageGeneral))
      {
        WebDialogResult webDialogResult = ((PXSelectBase) this.Documents).View.Ask_YesNoCancel_WithCallback((object) null, "Warning", messageGeneral, (MessageIcon) 3);
        if (webDialogResult == 2)
        {
          this.UpdateBAccountIDAndContactID(cache, row);
        }
        else
        {
          this.RefreshCurrents();
          bool flag1 = webDialogResult == 6;
          bool? nullable = row.AllowOverrideContactAddress;
          bool flag2 = false;
          if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue || flag1 & needAskFromContactAddress)
            this.ResetContactAndAddress(cache, row);
          if (!(flag1 & needAskFromShippingContact))
          {
            nullable = (bool?) this.GetCurrentShippingContact()?.IsDefaultContact;
            if (!nullable.HasValue || !nullable.GetValueOrDefault())
              goto label_9;
          }
          this.ResetShippingContact(cache, row);
label_9:
          if (!(flag1 & needAskFromShippingAddress))
          {
            nullable = (bool?) this.GetCurrentShippingAddress()?.IsDefaultAddress;
            if (!nullable.HasValue || !nullable.GetValueOrDefault())
              goto label_12;
          }
          this.ResetShippingAddress(cache, row);
label_12:
          if (!(flag1 & needAskFromBillingContact))
          {
            nullable = (bool?) this.GetCurrentBillingContact()?.IsDefaultContact;
            if (!nullable.HasValue || !nullable.GetValueOrDefault())
              goto label_15;
          }
          this.ResetBillingContact(cache, row);
label_15:
          if (!(flag1 & needAskFromBillingAddress))
          {
            nullable = (bool?) this.GetCurrentBillingAddress()?.IsDefaultAddress;
            if (!nullable.HasValue || !nullable.GetValueOrDefault())
              goto label_37;
          }
          this.ResetBillingAddress(cache, row);
        }
      }
      else
      {
        if (changedForContactInfo.CanBeReplaced)
          this.ResetContactAndAddress(cache, row);
        if (changedForBillingInfo.CanBeReplaced)
        {
          this.ResetBillingContact(cache, row);
          this.ResetBillingAddress(cache, row);
        }
        if (changedForShippingInfo.CanBeReplaced)
        {
          this.ResetShippingContact(cache, row);
          this.ResetShippingAddress(cache, row);
        }
      }
    }
    else
    {
      int? nullable1 = row.LocationID;
      if (!nullable1.HasValue)
      {
        nullable1 = row.ContactID;
        if (!nullable1.HasValue)
        {
          nullable1 = row.BAccountID;
          if (!nullable1.HasValue)
          {
            this.RefreshCurrents();
            bool? nullable2 = row.AllowOverrideContactAddress;
            bool flag = false;
            if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
              this.ResetContactAndAddress(cache, row);
            nullable2 = (bool?) this.GetCurrentBillingContact()?.IsDefaultContact;
            if (nullable2.HasValue && nullable2.GetValueOrDefault())
            {
              this.ResetBillingContact(cache, row);
              IPersonalContact currentBillingContact = this.GetCurrentBillingContact(true);
              this.GetBillingContactCache().RaiseRowUpdated((object) currentBillingContact, (object) currentBillingContact);
            }
            nullable2 = (bool?) this.GetCurrentBillingAddress()?.IsDefaultAddress;
            if (nullable2.HasValue && nullable2.GetValueOrDefault())
            {
              this.ResetBillingAddress(cache, row);
              IAddress currentBillingAddress = this.GetCurrentBillingAddress(true);
              this.GetBillingAddressCache().RaiseRowUpdated((object) currentBillingAddress, (object) currentBillingAddress);
            }
            nullable2 = (bool?) this.GetCurrentShippingContact()?.IsDefaultContact;
            if (nullable2.HasValue && nullable2.GetValueOrDefault())
            {
              this.ResetShippingContact(cache, row);
              IPersonalContact currentShippingContact = this.GetCurrentShippingContact();
              this.GetShippingContactCache().RaiseRowUpdated((object) currentShippingContact, (object) currentShippingContact);
            }
            nullable2 = (bool?) this.GetCurrentShippingAddress()?.IsDefaultAddress;
            if (nullable2.HasValue && nullable2.GetValueOrDefault())
            {
              this.ResetShippingAddress(cache, row);
              IAddress currentShippingAddress = this.GetCurrentShippingAddress();
              this.GetShippingAddressCache().RaiseRowUpdated((object) currentShippingAddress, (object) currentShippingAddress);
            }
          }
        }
      }
    }
label_37:
    if (!this.IsDefaultContactAddress())
      return;
    cache.SetValue<Document.allowOverrideContactAddress>((object) row, (object) true);
  }

  private string GetMessageGeneral(
    bool needAskFromContactAddress,
    bool needAskFromBillingContact,
    bool needAskFromBillingAddress,
    bool needAskFromShippingContact,
    bool needAskFromShippingAddress)
  {
    if (!(needAskFromContactAddress | needAskFromBillingContact | needAskFromBillingAddress | needAskFromShippingContact | needAskFromShippingAddress))
      return (string) null;
    if (PXContext.IsModernUiScreen())
    {
      bool needAskFromModernAddressTab = needAskFromBillingContact | needAskFromBillingAddress | needAskFromShippingContact | needAskFromShippingAddress;
      return this.GetMessageForDefRecModernUI(needAskFromContactAddress, needAskFromModernAddressTab);
    }
    bool needAskFromBillingContactAddress = needAskFromBillingContact | needAskFromBillingAddress;
    bool needAskFromShippingContactAddress = needAskFromShippingContact | needAskFromShippingAddress;
    return this.GetMessageForDefaultRecords(needAskFromContactAddress, needAskFromBillingContactAddress, needAskFromShippingContactAddress);
  }

  private string GetMessageForDefRecModernUI(
    bool needAskFromContactAddress,
    bool needAskFromModernAddressTab)
  {
    if (needAskFromContactAddress & needAskFromModernAddressTab)
      return "Override settings on the Contact and Addresses tabs with settings from the new entity?";
    return needAskFromModernAddressTab ? "Override settings on the Addresses tab with settings from the new entity?" : "Would you like the overridden settings on the Contact tab to be replaced with the settings from the newly selected entity?";
  }

  protected virtual string GetMessageForDefaultRecords(
    bool needAskFromContactAddress,
    bool needAskFromBillingContactAddress,
    bool needAskFromShippingContactAddress)
  {
    if (needAskFromContactAddress)
    {
      if (!needAskFromShippingContactAddress)
        return "Would you like the overridden settings on the Contact tab to be replaced with the settings from the newly selected entity?";
      return needAskFromBillingContactAddress ? "Would you like the overridden settings on the Contact, Financial, and Shipping tabs to be replaced with the settings from the newly selected entity?" : "Would you like the overridden settings on the Contact and Shipping tabs to be replaced with the settings from the newly selected entity?";
    }
    if (!needAskFromShippingContactAddress)
      return "Would you like the overridden settings on the Financial tab to be replaced with the settings from the newly selected entity?";
    return needAskFromBillingContactAddress ? "Would you like the overridden settings on the Financial and Shipping tabs to be replaced with the settings from the newly selected entity?" : "Would you like the overridden settings on the Shipping tab to be replaced with the settings from the newly selected entity?";
  }

  private void UpdateBAccountIDAndContactID(PXCache cache, Document row)
  {
    cache.SetValue<Document.bAccountID>((object) row, cache.GetValueOriginal<Document.bAccountID>(cache.GetMain<Document>(row)));
    cache.SetValue<Document.contactID>((object) row, cache.GetValueOriginal<Document.contactID>(cache.GetMain<Document>(row)));
  }

  private void ResetContactAndAddress(PXCache cache, Document row)
  {
    SharedRecordAttribute.DefaultRecord<Document.documentAddressID>(cache, cache.GetMain<Document>(row));
    SharedRecordAttribute.DefaultRecord<Document.documentContactID>(cache, cache.GetMain<Document>(row));
    cache.SetValue<Document.allowOverrideContactAddress>((object) row, (object) false);
  }

  private void ResetShippingContact(PXCache cache, Document row)
  {
    PXCache shippingContactCache = this.GetShippingContactCache();
    if (shippingContactCache == null)
      return;
    SharedRecordAttribute.DefaultRecord<Document.shipContactID>(cache, cache.GetMain<Document>(row));
    IPersonalContact currentShippingContact = this.GetCurrentShippingContact();
    shippingContactCache.SetValue((object) currentShippingContact, "IsDefaultContact", (object) true);
  }

  private void ResetShippingAddress(PXCache cache, Document row)
  {
    PXCache shippingAddressCache = this.GetShippingAddressCache();
    if (shippingAddressCache == null)
      return;
    SharedRecordAttribute.DefaultRecord<Document.shipAddressID>(cache, cache.GetMain<Document>(row));
    IAddress currentShippingAddress = this.GetCurrentShippingAddress();
    shippingAddressCache.SetValue((object) currentShippingAddress, "IsDefaultAddress", (object) true);
  }

  private void ResetBillingContact(PXCache cache, Document row)
  {
    PXCache billingContactCache = this.GetBillingContactCache();
    if (billingContactCache == null)
      return;
    SharedRecordAttribute.DefaultRecord<Document.billContactID>(cache, cache.GetMain<Document>(row));
    IPersonalContact currentBillingContact = this.GetCurrentBillingContact();
    billingContactCache.SetValue((object) currentBillingContact, "IsDefaultContact", (object) true);
  }

  private void ResetBillingAddress(PXCache cache, Document row)
  {
    PXCache shippingAddressCache = this.GetShippingAddressCache();
    if (shippingAddressCache == null)
      return;
    SharedRecordAttribute.DefaultRecord<Document.billAddressID>(cache, cache.GetMain<Document>(row));
    IAddress currentBillingAddress = this.GetCurrentBillingAddress();
    shippingAddressCache.SetValue((object) currentBillingAddress, "IsDefaultAddress", (object) true);
  }

  protected virtual bool AskForConfirmationForContactAddress(
    Document row,
    CROpportunityContactAddressExt<TGraph>.ChangedData data)
  {
    IPersonalContact currentContact = this.GetCurrentContact();
    IAddress currentAddress = this.GetCurrentAddress();
    return row.AllowOverrideContactAddress.GetValueOrDefault() && this.LocationOrContactIsNotNull(row) && (data.WasFullContactChanged(currentContact) || data.WasAddressChanged(currentAddress)) && !this.IsDefaultContactAddress();
  }

  protected virtual bool AskForConfirmationForBillingContact(
    Document row,
    CROpportunityContactAddressExt<TGraph>.ChangedData data)
  {
    IPersonalContact currentBillingContact = this.GetCurrentBillingContact();
    if (currentBillingContact != null)
    {
      bool? isDefaultContact = currentBillingContact.IsDefaultContact;
      bool flag = false;
      if (isDefaultContact.GetValueOrDefault() == flag & isDefaultContact.HasValue && !data.SkipAskContact())
        return !this.IsDefaultBillingContact();
    }
    return false;
  }

  protected virtual bool AskForConfirmationForBillingAddress(
    Document row,
    CROpportunityContactAddressExt<TGraph>.ChangedData data)
  {
    IAddress currentBillingAddress = this.GetCurrentBillingAddress();
    if (currentBillingAddress != null)
    {
      bool? isDefaultAddress = currentBillingAddress.IsDefaultAddress;
      bool flag = false;
      if (isDefaultAddress.GetValueOrDefault() == flag & isDefaultAddress.HasValue && !data.SkipAskAddress())
        return !this.IsDefaultBillingAddress();
    }
    return false;
  }

  protected virtual bool AskForConfirmationForShippingContact(
    Document row,
    CROpportunityContactAddressExt<TGraph>.ChangedData data)
  {
    IPersonalContact currentShippingContact = this.GetCurrentShippingContact();
    if (currentShippingContact != null)
    {
      bool? isDefaultContact = currentShippingContact.IsDefaultContact;
      bool flag = false;
      if (isDefaultContact.GetValueOrDefault() == flag & isDefaultContact.HasValue && !data.SkipAskContact())
        return !this.IsDefaultShippingContact();
    }
    return false;
  }

  protected virtual bool AskForConfirmationForShippingAddress(
    Document row,
    CROpportunityContactAddressExt<TGraph>.ChangedData data)
  {
    IAddress currentShippingAddress = this.GetCurrentShippingAddress();
    if (currentShippingAddress != null)
    {
      bool? isDefaultAddress = currentShippingAddress.IsDefaultAddress;
      bool flag = false;
      if (isDefaultAddress.GetValueOrDefault() == flag & isDefaultAddress.HasValue && !data.SkipAskAddress())
        return !this.IsDefaultShippingAddress();
    }
    return false;
  }

  private void RefreshCurrents()
  {
    this.GetCurrentContact(true);
    this.GetCurrentShippingContact(true);
    this.GetCurrentBillingContact(true);
    this.GetCurrentAddress(true);
    this.GetCurrentShippingAddress(true);
    this.GetCurrentBillingAddress(true);
  }

  protected virtual IPersonalContact GetCurrentContact(bool forceSelect = false)
  {
    PXCache contactCache = this.GetContactCache();
    return !forceSelect && contactCache.Current is IPersonalContact current ? current : (contactCache.Current = (object) this.SelectContact()) as IPersonalContact;
  }

  protected virtual IPersonalContact GetCurrentShippingContact(bool forceSelect = false)
  {
    PXCache shippingContactCache = this.GetShippingContactCache();
    if (shippingContactCache == null)
      return (IPersonalContact) null;
    return !forceSelect && shippingContactCache.Current is IPersonalContact current ? current : (shippingContactCache.Current = (object) this.SelectShippingContact()) as IPersonalContact;
  }

  protected virtual IPersonalContact GetCurrentBillingContact(bool forceSelect = false)
  {
    PXCache billingContactCache = this.GetBillingContactCache();
    if (billingContactCache == null)
      return (IPersonalContact) null;
    return !forceSelect && billingContactCache.Current is IPersonalContact current ? current : (billingContactCache.Current = (object) this.SelectBillingContact()) as IPersonalContact;
  }

  protected virtual IAddress GetCurrentAddress(bool forceSelect = false)
  {
    PXCache addressCache = this.GetAddressCache();
    return !forceSelect && addressCache.Current is IAddress current ? current : (addressCache.Current = (object) this.SelectAddress()) as IAddress;
  }

  protected virtual IAddress GetCurrentShippingAddress(bool forceSelect = false)
  {
    PXCache shippingAddressCache = this.GetShippingAddressCache();
    if (shippingAddressCache == null)
      return (IAddress) null;
    return !forceSelect && shippingAddressCache.Current is IAddress current ? current : (shippingAddressCache.Current = (object) this.SelectShippingAddress()) as IAddress;
  }

  protected virtual IAddress GetCurrentBillingAddress(bool forceSelect = false)
  {
    PXCache billingAddressCache = this.GetBillingAddressCache();
    if (billingAddressCache == null)
      return (IAddress) null;
    return !forceSelect && billingAddressCache.Current is IAddress current ? current : (billingAddressCache.Current = (object) this.SelectBillingAddress()) as IAddress;
  }

  protected abstract IPersonalContact SelectContact();

  protected virtual IPersonalContact SelectBillingContact() => (IPersonalContact) null;

  protected virtual IPersonalContact SelectShippingContact() => (IPersonalContact) null;

  protected abstract IPersonalContact GetEtalonContact();

  protected virtual IPersonalContact GetEtalonBillingContact() => (IPersonalContact) null;

  protected virtual IPersonalContact GetEtalonShippingContact() => (IPersonalContact) null;

  protected abstract IAddress SelectAddress();

  protected virtual IAddress SelectBillingAddress() => (IAddress) null;

  protected virtual IAddress SelectShippingAddress() => (IAddress) null;

  protected abstract IAddress GetEtalonAddress();

  protected virtual IAddress GetEtalonBillingAddress() => (IAddress) null;

  protected virtual IAddress GetEtalonShippingAddress() => (IAddress) null;

  protected abstract PXCache GetContactCache();

  protected abstract PXCache GetAddressCache();

  protected virtual PXCache GetBillingContactCache() => (PXCache) null;

  protected virtual PXCache GetBillingAddressCache() => (PXCache) null;

  protected virtual PXCache GetShippingContactCache() => (PXCache) null;

  protected virtual PXCache GetShippingAddressCache() => (PXCache) null;

  protected virtual bool IsDefaultContactAddress()
  {
    IPersonalContact currentContact = this.GetCurrentContact(true);
    IAddress currentAddress = this.GetCurrentAddress(true);
    if (currentContact == null || currentAddress == null)
      return true;
    return CROpportunityContactAddressExt<TGraph>.AreFullContactsEquivalent(currentContact, this.GetEtalonContact()) && CROpportunityContactAddressExt<TGraph>.AreAddressesEquivalent((IAddressBase) currentAddress, (IAddressBase) this.GetEtalonAddress());
  }

  protected virtual bool IsDefaultBillingContact()
  {
    return CROpportunityContactAddressExt<TGraph>.AreShortContactsEquivalent((IContact) this.GetCurrentBillingContact(), (IContact) this.GetEtalonBillingContact());
  }

  protected virtual bool IsDefaultBillingAddress()
  {
    return CROpportunityContactAddressExt<TGraph>.AreAddressesEquivalent((IAddressBase) this.GetCurrentBillingAddress(), (IAddressBase) this.GetEtalonBillingAddress());
  }

  protected virtual bool IsDefaultShippingContact()
  {
    return CROpportunityContactAddressExt<TGraph>.AreShortContactsEquivalent((IContact) this.GetCurrentShippingContact(), (IContact) this.GetEtalonShippingContact());
  }

  protected virtual bool IsDefaultShippingAddress()
  {
    return CROpportunityContactAddressExt<TGraph>.AreAddressesEquivalent((IAddressBase) this.GetCurrentShippingAddress(), (IAddressBase) this.GetEtalonShippingAddress());
  }

  protected object SafeGetEtalon(PXCache cache)
  {
    using (new ReplaceCurrentScope((IEnumerable<KeyValuePair<PXCache, object>>) new KeyValuePair<PXCache, object>[1]
    {
      new KeyValuePair<PXCache, object>(cache, (object) null)
    }))
    {
      using (new ReadOnlyScope(new PXCache[1]{ cache }))
      {
        object etalon = cache.Insert();
        cache.SetStatus(etalon, (PXEntryStatus) 5);
        return etalon;
      }
    }
  }

  private static bool AreAddressesEquivalent(
    IAddressBase currentAddress,
    IAddressBase etalonAddress)
  {
    if (currentAddress == null || etalonAddress == null)
      return false;
    bool flag1 = false;
    if (currentAddress is IAddressISO20022 iaddressIsO20022_1 && etalonAddress is IAddressISO20022 iaddressIsO20022_2)
      flag1 = iaddressIsO20022_1.Department != iaddressIsO20022_2.Department || iaddressIsO20022_1.SubDepartment != iaddressIsO20022_2.SubDepartment || iaddressIsO20022_1.StreetName != iaddressIsO20022_2.StreetName || iaddressIsO20022_1.BuildingNumber != iaddressIsO20022_2.BuildingNumber || iaddressIsO20022_1.BuildingName != iaddressIsO20022_2.BuildingName || iaddressIsO20022_1.Floor != iaddressIsO20022_2.Floor || iaddressIsO20022_1.UnitNumber != iaddressIsO20022_2.UnitNumber || iaddressIsO20022_1.PostBox != iaddressIsO20022_2.PostBox || iaddressIsO20022_1.Room != iaddressIsO20022_2.Room || iaddressIsO20022_1.TownLocationName != iaddressIsO20022_2.TownLocationName || iaddressIsO20022_1.DistrictName != iaddressIsO20022_2.DistrictName;
    bool flag2 = false;
    if (currentAddress is IAddressLocation iaddressLocation1 && etalonAddress is IAddressLocation iaddressLocation2)
    {
      Decimal? longitude1 = iaddressLocation1.Longitude;
      Decimal? longitude2 = iaddressLocation2.Longitude;
      int num;
      if (longitude1.GetValueOrDefault() == longitude2.GetValueOrDefault() & longitude1.HasValue == longitude2.HasValue)
      {
        Decimal? latitude1 = iaddressLocation1.Latitude;
        Decimal? latitude2 = iaddressLocation2.Latitude;
        num = !(latitude1.GetValueOrDefault() == latitude2.GetValueOrDefault() & latitude1.HasValue == latitude2.HasValue) ? 1 : 0;
      }
      else
        num = 1;
      flag2 = num != 0;
    }
    return ((currentAddress.AddressLine1 != etalonAddress.AddressLine1 || currentAddress.AddressLine2 != etalonAddress.AddressLine2 || currentAddress.City != etalonAddress.City || currentAddress.State != etalonAddress.State || currentAddress.CountryID != etalonAddress.CountryID ? 1 : (currentAddress.PostalCode != etalonAddress.PostalCode ? 1 : 0)) | (flag1 ? 1 : 0) | (flag2 ? 1 : 0)) == 0;
  }

  private static bool AreFullContactsEquivalent(
    IPersonalContact currentContact,
    IPersonalContact etalonContact)
  {
    return currentContact != null && etalonContact != null && !(currentContact.FullName != etalonContact.FullName) && !(currentContact.Title != etalonContact.Title) && !(currentContact.FirstName != etalonContact.FirstName) && !(currentContact.LastName != etalonContact.LastName) && !(currentContact.Salutation != etalonContact.Salutation) && !(currentContact.Attention != etalonContact.Attention) && !(currentContact.Email != etalonContact.Email) && !(currentContact.Phone1 != etalonContact.Phone1) && !(currentContact.Phone1Type != etalonContact.Phone1Type) && !(currentContact.Phone2 != etalonContact.Phone2) && !(currentContact.Phone2Type != etalonContact.Phone2Type) && !(currentContact.Phone3 != etalonContact.Phone3) && !(currentContact.Phone3Type != etalonContact.Phone3Type) && !(currentContact.Fax != etalonContact.Fax) && !(currentContact.FaxType != etalonContact.FaxType);
  }

  private static bool AreShortContactsEquivalent(IContact currentContact, IContact etalonContact)
  {
    return currentContact != null && etalonContact != null && !(currentContact.FullName != etalonContact.FullName) && !(currentContact.Attention != etalonContact.Attention) && !(currentContact.Email != etalonContact.Email) && !(currentContact.Phone1 != etalonContact.Phone1) && !(currentContact.Phone1Type != etalonContact.Phone1Type) && !(currentContact.Phone2 != etalonContact.Phone2) && !(currentContact.Phone2Type != etalonContact.Phone2Type);
  }

  protected class DocumentMapping : IBqlMapping
  {
    protected System.Type _table;
    public System.Type ContactID = typeof (Document.contactID);
    public System.Type DocumentContactID = typeof (Document.documentContactID);
    public System.Type DocumentAddressID = typeof (Document.documentAddressID);
    public System.Type ShipContactID = typeof (Document.shipContactID);
    public System.Type ShipAddressID = typeof (Document.shipAddressID);
    public System.Type BillContactID = typeof (Document.billContactID);
    public System.Type BillAddressID = typeof (Document.billAddressID);
    public System.Type LocationID = typeof (Document.locationID);
    public System.Type BAccountID = typeof (Document.bAccountID);
    public System.Type AllowOverrideContactAddress = typeof (Document.allowOverrideContactAddress);
    public System.Type AllowOverrideShippingContactAddress = typeof (Document.allowOverrideShippingContactAddress);
    public System.Type AllowOverrideBillingContactAddress = typeof (Document.allowOverrideBillingContactAddress);

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
    public System.Type IsValidated = typeof (DocumentAddress.isValidated);

    public System.Type Extension => typeof (DocumentAddress);

    public System.Type Table => this._table;

    public DocumentAddressMapping(System.Type table) => this._table = table;
  }

  protected struct ChangedData
  {
    public static readonly CROpportunityContactAddressExt<TGraph>.ChangedData ShouldNotReplace = new CROpportunityContactAddressExt<TGraph>.ChangedData(false, (Contact) null, (Address) null);

    public static CROpportunityContactAddressExt<TGraph>.ChangedData ShouldReplace(
      Contact oldContact,
      Address oldAddress)
    {
      return new CROpportunityContactAddressExt<TGraph>.ChangedData(true, oldContact, oldAddress);
    }

    private ChangedData(bool canBeReplaced, Contact oldContact, Address oldAddress)
    {
      this.CanBeReplaced = canBeReplaced;
      this.OldContact = oldContact;
      this.OldAddress = oldAddress;
    }

    public bool CanBeReplaced { get; }

    public Contact OldContact { get; }

    public Address OldAddress { get; }

    public bool SkipAskContact() => !this.CanBeReplaced || this.OldContact == null;

    public bool WasContactChanged(IContact contact)
    {
      if (!this.CanBeReplaced)
        return false;
      return this.OldContact == null || !CROpportunityContactAddressExt<TGraph>.AreShortContactsEquivalent(contact, (IContact) this.OldContact);
    }

    public bool WasFullContactChanged(IPersonalContact contact)
    {
      if (!this.CanBeReplaced)
        return false;
      return this.OldContact == null || !CROpportunityContactAddressExt<TGraph>.AreFullContactsEquivalent(contact, (IPersonalContact) this.OldContact);
    }

    public bool SkipAskAddress() => !this.CanBeReplaced || this.OldAddress == null;

    public bool WasAddressChanged(IAddress address)
    {
      if (!this.CanBeReplaced)
        return false;
      return this.OldAddress == null || !CROpportunityContactAddressExt<TGraph>.AreAddressesEquivalent((IAddressBase) address, (IAddressBase) this.OldAddress);
    }
  }
}
