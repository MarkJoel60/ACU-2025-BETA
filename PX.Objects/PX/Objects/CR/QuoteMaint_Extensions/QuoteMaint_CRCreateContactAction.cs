// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.QuoteMaint_Extensions.QuoteMaint_CRCreateContactAction
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
public class QuoteMaint_CRCreateContactAction : CRCreateContactAction<QuoteMaint, CRQuote>
{
  protected override string TargetType => "PX.Objects.CR.CRQuote";

  public override void Initialize()
  {
    base.Initialize();
    this.Addresses = new PXSelectExtension<DocumentAddress>((PXSelectBase) this.Base.Quote_Address);
    this.Contacts = new PXSelectExtension<DocumentContact>((PXSelectBase) this.Base.Quote_Contact);
    this.ContactMethod = new PXSelectExtension<DocumentContactMethod>((PXSelectBase) this.Base.Quote_Contact);
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

  protected override CRCreateContactAction<QuoteMaint, CRQuote>.DocumentContactMethodMapping GetDocumentContactMethodMapping()
  {
    return new CRCreateContactAction<QuoteMaint, CRQuote>.DocumentContactMethodMapping(typeof (CRContact));
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
    Events.FieldDefaulting<ContactFilter, ContactFilter.contactClass> e)
  {
    Contact contact = ((PXSelectBase<Contact>) this.ExistingContact).SelectSingle(Array.Empty<object>());
    if (contact != null)
    {
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<ContactFilter, ContactFilter.contactClass>, ContactFilter, object>) e).NewValue = (object) contact.ClassID;
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<ContactFilter, ContactFilter.contactClass>>) e).Cancel = true;
    }
    else
    {
      if (((PXSelectBase<CRQuote>) this.Base.Quote).Current == null)
        return;
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<ContactFilter, ContactFilter.contactClass>, ContactFilter, object>) e).NewValue = (object) ((PXSelectBase<CRSetup>) this.Base.Setup).Current?.DefaultContactClassID;
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<ContactFilter, ContactFilter.contactClass>>) e).Cancel = true;
    }
  }

  public override void _(Events.RowSelected<ContactFilter> e)
  {
    base._(e);
    QuoteMaint quoteMaint = this.Base;
    int num;
    if (quoteMaint == null)
    {
      num = 0;
    }
    else
    {
      PXSelect<CRQuote, Where<CRQuote.opportunityID, Equal<Optional<CRQuote.opportunityID>>, And<CRQuote.quoteType, Equal<CRQuoteTypeAttribute.distribution>>>> quote = quoteMaint.Quote;
      if (quote == null)
      {
        num = 0;
      }
      else
      {
        CRQuote current = ((PXSelectBase<CRQuote>) quote).Current;
        num = current != null ? (current.BAccountID.HasValue ? 1 : 0) : 0;
      }
    }
    bool flag = num != 0;
    PXUIFieldAttribute.SetReadOnly<ContactFilter.fullName>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ContactFilter>>) e).Cache, (object) e.Row, flag);
  }

  protected override void MapContactMethod(DocumentContactMethod source, Contact target)
  {
  }

  protected override object GetDefaultFieldValueFromCache<TExistingField, TField>()
  {
    if (!(typeof (TExistingField) == typeof (Contact.fullName)))
    {
      CRQuote current1 = ((PXSelectBase<CRQuote>) this.Base.Quote).Current;
      if ((current1 != null ? (!current1.BAccountID.HasValue ? 1 : 0) : 1) == 0)
      {
        CRQuote current2 = ((PXSelectBase<CRQuote>) this.Base.Quote).Current;
        if ((current2 != null ? (current2.AllowOverrideContactAddress.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          return (object) null;
      }
    }
    return base.GetDefaultFieldValueFromCache<TExistingField, TField>();
  }
}
