// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.DefContactAddressExt`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.CS;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CR.Extensions;

/// <summary>
/// Extension that is used for selecting and defaulting the Default Address and Default Contact of the Business Account and it's inheritors.
/// No Inserting of Contact and Address is implemented, as the Inserting is handled inside the <see cref="T:PX.Objects.CR.Extensions.Relational.SharedChildOverrideGraphExt`2" /> graph extension.
/// </summary>
public abstract class DefContactAddressExt<TGraph, TMaster, FAcctName> : 
  PXGraphExtension<TGraph>,
  IAddressValidationHelper
  where TGraph : PXGraph
  where TMaster : PX.Objects.CR.BAccount, IBqlTable, new()
  where FAcctName : class, IBqlField
{
  [PXViewName("Account Address")]
  public PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.bAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>, And<PX.Objects.CR.Address.addressID, Equal<Current<PX.Objects.CR.BAccount.defAddressID>>>>> DefAddress;
  [PXViewName("Additional Account Info")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (PX.Objects.CR.Contact.duplicateStatus), typeof (PX.Objects.CR.Contact.duplicateFound)})]
  public PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.bAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>, And<PX.Objects.CR.Contact.contactID, Equal<Current<PX.Objects.CR.BAccount.defContactID>>>>> DefContact;
  public PXAction<TMaster> ViewMainOnMap;
  public PXAction<TMaster> ValidateAddresses;

  protected virtual bool PersistentAddressValidation => false;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Contact.fullName>(((PXSelectBase) this.DefContact).Cache, (object) null);
    PXUIFieldAttribute.SetVisible<PX.Objects.CR.Contact.languageID>(((PXSelectBase) this.DefContact).Cache, (object) null, PXDBLocalizableStringAttribute.HasMultipleLocales);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable viewMainOnMap(PXAdapter adapter)
  {
    PX.Objects.CR.Address aAddr = ((PXSelectBase<PX.Objects.CR.Address>) this.DefAddress).SelectSingle(Array.Empty<object>());
    if (aAddr != null)
      BAccountUtility.ViewOnMap(aAddr);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable validateAddresses(PXAdapter adapter)
  {
    if ((object) (this.Base.Caches[typeof (TMaster)].Current as TMaster) == null)
      return adapter.Get();
    if (this.PersistentAddressValidation && this.Base.IsDirty)
      this.Base.Actions.PressSave();
    this.Base.FindAllImplementations<IAddressValidationHelper>().ValidateAddresses();
    if (this.PersistentAddressValidation)
      this.Base.Actions.PressSave();
    return adapter.Get();
  }

  public virtual bool CurrentAddressRequiresValidation => true;

  public virtual void ValidateAddress()
  {
    PX.Objects.CR.Address aAddress = ((PXSelectBase<PX.Objects.CR.Address>) this.DefAddress).SelectSingle(Array.Empty<object>());
    if (aAddress == null)
      return;
    bool? isValidated = aAddress.IsValidated;
    bool flag = false;
    if (!(isValidated.GetValueOrDefault() == flag & isValidated.HasValue))
      return;
    PXAddressValidator.Validate<PX.Objects.CR.Address>((PXGraph) this.Base, aAddress, true, true);
  }

  [PXDefault("AP")]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.contactType> e)
  {
  }

  [PXDefault("B2")]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.phone2Type> e)
  {
  }

  [PXUIField]
  [PXDBDefault(typeof (PX.Objects.CR.BAccount.bAccountID))]
  [PXSelector(typeof (PX.Objects.CR.BAccount.bAccountID), SubstituteKey = typeof (PX.Objects.CR.BAccount.acctCD), DirtyRead = true)]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.bAccountID> e)
  {
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<TMaster, FAcctName> e)
  {
    PX.Objects.CR.BAccount row = (PX.Objects.CR.BAccount) e.Row;
    PX.Objects.CR.Contact contact = ((PXSelectBase<PX.Objects.CR.Contact>) this.DefContact).SelectSingle(Array.Empty<object>());
    if (contact == null)
      return;
    contact.FullName = row.AcctName;
    ((PXSelectBase<PX.Objects.CR.Contact>) this.DefContact).Update(contact);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.CR.Address, PX.Objects.CR.Address.countryID> e)
  {
    PX.Objects.CR.Address row = e.Row;
    if (this.Base.IsContractBasedAPI || !((string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.CR.Address, PX.Objects.CR.Address.countryID>, PX.Objects.CR.Address, object>) e).OldValue != row.CountryID))
      return;
    row.State = (string) null;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<TMaster, PX.Objects.CR.BAccount.status> e)
  {
    PX.Objects.CR.Contact contact = ((PXSelectBase<PX.Objects.CR.Contact>) this.DefContact).SelectSingle(Array.Empty<object>());
    if (contact == null)
      return;
    contact.IsActive = !((string) e.NewValue == "I") ? new bool?(true) : new bool?(false);
    ((PXSelectBase<PX.Objects.CR.Contact>) this.DefContact).Update(contact);
  }

  protected virtual void _(PX.Data.Events.RowSelected<TMaster> e)
  {
    PX.Objects.CR.BAccount row = (PX.Objects.CR.BAccount) e.Row;
    if (row == null)
      return;
    ((PXAction) this.ValidateAddresses).SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<TMaster>>) e).Cache.GetStatus((object) row) != 2);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.CR.Contact> e)
  {
    PX.Objects.CR.Contact row = e.Row;
    if (row == null || e.Operation != 1 || (string) ((PXSelectBase) this.DefContact).Cache.GetValueOriginal<PX.Objects.CR.Contact.languageID>((object) row) == row.LanguageID)
      return;
    TMaster current = this.Base.Caches[typeof (TMaster)].Current as TMaster;
    switch (current?.Type)
    {
      case "CU":
        PXDatabase.Update<Customer>(new PXDataFieldParam[2]
        {
          (PXDataFieldParam) new PXDataFieldAssign<Customer.localeName>((object) row.LanguageID),
          (PXDataFieldParam) new PXDataFieldRestrict<Customer.bAccountID>((object) (int?) current?.BAccountID)
        });
        break;
      case "VE":
        PXDatabase.Update<PX.Objects.AP.Vendor>(new PXDataFieldParam[2]
        {
          (PXDataFieldParam) new PXDataFieldAssign<PX.Objects.AP.Vendor.localeName>((object) row.LanguageID),
          (PXDataFieldParam) new PXDataFieldRestrict<PX.Objects.AP.Vendor.bAccountID>((object) (int?) current?.BAccountID)
        });
        break;
      case "VC":
        PXDatabase.Update<Customer>(new PXDataFieldParam[2]
        {
          (PXDataFieldParam) new PXDataFieldAssign<Customer.localeName>((object) row.LanguageID),
          (PXDataFieldParam) new PXDataFieldRestrict<Customer.bAccountID>((object) (int?) current?.BAccountID)
        });
        PXDatabase.Update<PX.Objects.AP.Vendor>(new PXDataFieldParam[2]
        {
          (PXDataFieldParam) new PXDataFieldAssign<PX.Objects.AP.Vendor.localeName>((object) row.LanguageID),
          (PXDataFieldParam) new PXDataFieldRestrict<PX.Objects.AP.Vendor.bAccountID>((object) (int?) current?.BAccountID)
        });
        break;
    }
  }

  public abstract class WithPersistentAddressValidation : 
    DefContactAddressExt<TGraph, TMaster, FAcctName>
  {
    protected override bool PersistentAddressValidation => true;
  }

  public abstract class WithCombinedTypeValidation : DefContactAddressExt<TGraph, TMaster, FAcctName>
  {
    protected virtual void _(PX.Data.Events.RowDeleting<TMaster> e)
    {
      PX.Objects.CR.BAccount row = (PX.Objects.CR.BAccount) e.Row;
      if (row == null || row == null || !(row.Type == "VC") && !row.IsBranch.GetValueOrDefault())
        return;
      PXParentAttribute.SetLeaveChildren<PX.Objects.CR.Contact.bAccountID>(((PXSelectBase) this.DefContact).Cache, (object) null, true);
      PXParentAttribute.SetLeaveChildren<PX.Objects.CR.Address.bAccountID>(((PXSelectBase) this.DefAddress).Cache, (object) null, true);
    }
  }
}
