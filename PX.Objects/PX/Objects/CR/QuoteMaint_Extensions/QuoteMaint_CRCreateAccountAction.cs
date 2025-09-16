// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.QuoteMaint_Extensions.QuoteMaint_CRCreateAccountAction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions.CRCreateActions;
using System;

#nullable disable
namespace PX.Objects.CR.QuoteMaint_Extensions;

/// <exclude />
public class QuoteMaint_CRCreateAccountAction : CRCreateAccountAction<QuoteMaint, CRQuote>
{
  protected override string TargetType => "PX.Objects.CR.CRQuote";

  public override void Initialize()
  {
    base.Initialize();
    this.Addresses = new PXSelectExtension<DocumentAddress>((PXSelectBase) this.Base.Quote_Address);
    this.Contacts = new PXSelectExtension<DocumentContact>((PXSelectBase) this.Base.Quote_Contact);
  }

  protected override CRCreateActionBaseInit<QuoteMaint, CRQuote>.DocumentMapping GetDocumentMapping()
  {
    return new CRCreateActionBaseInit<QuoteMaint, CRQuote>.DocumentMapping(typeof (CRQuote))
    {
      RefContactID = typeof (CRQuote.contactID)
    };
  }

  protected override CRCreateActionBaseInit<QuoteMaint, CRQuote>.DocumentContactMapping GetDocumentContactMapping()
  {
    return new CRCreateActionBaseInit<QuoteMaint, CRQuote>.DocumentContactMapping(typeof (CRContact));
  }

  protected override CRCreateActionBaseInit<QuoteMaint, CRQuote>.DocumentAddressMapping GetDocumentAddressMapping()
  {
    return new CRCreateActionBaseInit<QuoteMaint, CRQuote>.DocumentAddressMapping(typeof (CRAddress));
  }

  protected override PXSelectBase<CRPMTimeActivity> Activities
  {
    get
    {
      return (PXSelectBase<CRPMTimeActivity>) ((PXGraph) this.Base).GetExtension<QuoteMaint_ActivityDetailsExt>().Activities;
    }
  }

  protected virtual void _(
    Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass> e)
  {
    BAccount baccount = ((PXSelectBase<BAccount>) this.ExistingAccount).SelectSingle(Array.Empty<object>());
    if (baccount != null)
    {
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass>, AccountsFilter, object>) e).NewValue = (object) baccount.ClassID;
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass>>) e).Cancel = true;
    }
    else
    {
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass>, AccountsFilter, object>) e).NewValue = (object) ((PXSelectBase<CRSetup>) this.Base.Setup).Current?.DefaultCustomerClassID;
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass>>) e).Cancel = true;
    }
  }

  protected override void _(Events.RowSelected<AccountsFilter> e)
  {
    base._(e);
    AccountsFilter row = e.Row;
    if (row == null)
      return;
    CRQuote current = ((PXSelectBase<CRQuote>) this.Base.Quote).Current;
    int? nullable;
    int num;
    if (current == null)
    {
      num = 0;
    }
    else
    {
      nullable = current.ContactID;
      num = nullable.HasValue ? 1 : 0;
    }
    if (num == 0)
      return;
    PXUIFieldAttribute.SetVisible<AccountsFilter.linkContactToAccount>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<AccountsFilter>>) e).Cache, (object) row, true);
    Contact contact = ((PXSelectBase<Contact>) this.Base.CurrentContact).Current ?? ((PXSelectBase<Contact>) this.Base.CurrentContact).SelectSingle(Array.Empty<object>());
    if (contact == null)
    {
      PXUIFieldAttribute.SetEnabled<AccountsFilter.linkContactToAccount>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<AccountsFilter>>) e).Cache, (object) row, false);
    }
    else
    {
      nullable = contact.BAccountID;
      if (nullable.HasValue)
        PXUIFieldAttribute.SetWarning<AccountsFilter.linkContactToAccount>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<AccountsFilter>>) e).Cache, (object) row, "Contact linked to another Business Account");
      else
        PXUIFieldAttribute.SetEnabled<AccountsFilter.linkContactToAccount>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<AccountsFilter>>) e).Cache, (object) row, true);
    }
  }

  protected virtual void _(
    Events.FieldDefaulting<AccountsFilter, AccountsFilter.linkContactToAccount> e)
  {
    if (e.Row == null)
      return;
    CRQuote current = ((PXSelectBase<CRQuote>) this.Base.Quote).Current;
    int? nullable;
    int num;
    if (current == null)
    {
      num = 0;
    }
    else
    {
      nullable = current.ContactID;
      num = nullable.HasValue ? 1 : 0;
    }
    if (num != 0)
    {
      Contact contact = ((PXSelectBase<Contact>) this.Base.CurrentContact).Current ?? ((PXSelectBase<Contact>) this.Base.CurrentContact).SelectSingle(Array.Empty<object>());
      if (contact == null)
      {
        ((Events.FieldDefaultingBase<Events.FieldDefaulting<AccountsFilter, AccountsFilter.linkContactToAccount>, AccountsFilter, object>) e).NewValue = (object) false;
      }
      else
      {
        nullable = contact.BAccountID;
        if (nullable.HasValue)
          ((Events.FieldDefaultingBase<Events.FieldDefaulting<AccountsFilter, AccountsFilter.linkContactToAccount>, AccountsFilter, object>) e).NewValue = (object) false;
        else
          ((Events.FieldDefaultingBase<Events.FieldDefaulting<AccountsFilter, AccountsFilter.linkContactToAccount>, AccountsFilter, object>) e).NewValue = (object) true;
      }
    }
    else
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<AccountsFilter, AccountsFilter.linkContactToAccount>, AccountsFilter, object>) e).NewValue = (object) false;
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<AccountsFilter, AccountsFilter.linkContactToAccount>>) e).Cancel = true;
  }
}
