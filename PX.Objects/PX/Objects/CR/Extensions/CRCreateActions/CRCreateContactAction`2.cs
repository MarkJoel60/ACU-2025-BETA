// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.CRCreateContactAction`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR.ContactMaint_Extensions;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CR.Extensions.CRCreateActions;

/// <exclude />
public abstract class CRCreateContactAction<TGraph, TMain> : CRCreateContactActionBase<
#nullable disable
TGraph, TMain>
  where TGraph : PXGraph, new()
  where TMain : class, IBqlTable, new()
{
  public PXSelectExtension<DocumentContactMethod> ContactMethod;
  public FbqlSelect<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  Contact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  Document.refContactID, IBqlInt>.FromCurrent.NoDefault>>, 
  #nullable disable
  Contact>.View ExistingContact;

  protected abstract CRCreateContactAction<TGraph, TMain>.DocumentContactMethodMapping GetDocumentContactMethodMapping();

  protected override IEnumerable<CSAnswers> GetAttributesForMasterEntity()
  {
    Contact contact = ((PXSelectBase<Contact>) this.ExistingContact).SelectSingle(Array.Empty<object>());
    if (contact == null)
      return base.GetAttributesForMasterEntity();
    return PXSelectBase<CSAnswers, PXSelect<CSAnswers, Where<CSAnswers.refNoteID, Equal<Required<Contact.noteID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) contact.NoteID
    }).FirstTableItems;
  }

  protected override object GetMasterEntity()
  {
    return (object) ((PXSelectBase<Contact>) this.ExistingContact).SelectSingle(Array.Empty<object>());
  }

  protected virtual object GetDefaultFieldValueFromCache<TExistingField, TField>()
  {
    Contact contact = ((PXSelectBase<Contact>) this.ExistingContact).SelectSingle(Array.Empty<object>());
    if (contact != null)
    {
      int? contactId = contact.ContactID;
      int num = 0;
      if (contactId.GetValueOrDefault() > num & contactId.HasValue)
        return ((PXSelectBase) this.ExistingContact).Cache.GetValue((object) contact, typeof (TExistingField).Name);
    }
    return ((PXSelectBase) this.Contacts).Cache.GetValue((object) ((PXSelectBase<DocumentContact>) this.Contacts).SelectSingle(Array.Empty<object>()), typeof (TField).Name);
  }

  public virtual void _(
    Events.FieldDefaulting<ContactFilter, ContactFilter.firstName> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<ContactFilter, ContactFilter.firstName>, ContactFilter, object>) e).NewValue = this.GetDefaultFieldValueFromCache<Contact.firstName, DocumentContact.firstName>();
  }

  public virtual void _(
    Events.FieldDefaulting<ContactFilter, ContactFilter.lastName> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<ContactFilter, ContactFilter.lastName>, ContactFilter, object>) e).NewValue = this.GetDefaultFieldValueFromCache<Contact.lastName, DocumentContact.lastName>();
  }

  public virtual void _(
    Events.FieldDefaulting<ContactFilter, ContactFilter.fullName> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<ContactFilter, ContactFilter.fullName>, ContactFilter, object>) e).NewValue = this.GetDefaultFieldValueFromCache<Contact.fullName, DocumentContact.fullName>();
  }

  public virtual void _(
    Events.FieldDefaulting<ContactFilter, ContactFilter.salutation> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<ContactFilter, ContactFilter.salutation>, ContactFilter, object>) e).NewValue = this.GetDefaultFieldValueFromCache<Contact.salutation, DocumentContact.salutation>();
  }

  public virtual void _(
    Events.FieldDefaulting<ContactFilter, ContactFilter.phone1> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<ContactFilter, ContactFilter.phone1>, ContactFilter, object>) e).NewValue = this.GetDefaultFieldValueFromCache<Contact.phone1, DocumentContact.phone1>();
  }

  public virtual void _(
    Events.FieldDefaulting<ContactFilter, ContactFilter.phone1Type> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<ContactFilter, ContactFilter.phone1Type>, ContactFilter, object>) e).NewValue = this.GetDefaultFieldValueFromCache<Contact.phone1Type, DocumentContact.phone1Type>();
  }

  public virtual void _(
    Events.FieldDefaulting<ContactFilter, ContactFilter.phone2> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<ContactFilter, ContactFilter.phone2>, ContactFilter, object>) e).NewValue = this.GetDefaultFieldValueFromCache<Contact.phone2, DocumentContact.phone2>();
  }

  public virtual void _(
    Events.FieldDefaulting<ContactFilter, ContactFilter.phone2Type> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<ContactFilter, ContactFilter.phone2Type>, ContactFilter, object>) e).NewValue = this.GetDefaultFieldValueFromCache<Contact.phone2Type, DocumentContact.phone2Type>();
  }

  public virtual void _(
    Events.FieldDefaulting<ContactFilter, ContactFilter.email> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<ContactFilter, ContactFilter.email>, ContactFilter, object>) e).NewValue = this.GetDefaultFieldValueFromCache<Contact.eMail, DocumentContact.email>();
  }

  public virtual void _(Events.RowSelected<ContactFilter> e)
  {
    if (e.Row != null)
      e.Row.NeedToUse = new bool?(this.NeedToUse);
    Contact existing = ((PXSelectBase<Contact>) this.ExistingContact).SelectSingle(Array.Empty<object>());
    PXCacheEx.AdjustUI(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ContactFilter>>) e).Cache, (object) e.Row).ForAllFields((Action<PXUIFieldAttribute>) (_ => _.Enabled = existing == null));
  }

  public virtual void _(Events.RowSelected<Document> e)
  {
    Contact contact = ((PXSelectBase<Contact>) this.ExistingContact).SelectSingle(Array.Empty<object>());
    ((PXAction) this.CreateContact).SetEnabled(contact == null);
    if (contact == null)
      return;
    ((PXSelectBase) this.ContactInfoAttributes).AllowUpdate = ((PXSelectBase) this.ContactInfoUDF).AllowUpdate = false;
  }

  public override ConversionResult<Contact> Convert(ContactConversionOptions options = null)
  {
    Contact contact = ((PXSelectBase<Contact>) this.ExistingContact).SelectSingle(Array.Empty<object>());
    if (contact != null)
    {
      ConversionResult<Contact> conversionResult = new ConversionResult<Contact>();
      conversionResult.Entity = contact;
      conversionResult.Converted = false;
      return conversionResult;
    }
    ConversionResult<Contact> conversionResult1 = base.Convert(options);
    this.FillDependedRelation(conversionResult1.Entity, options);
    return conversionResult1;
  }

  protected override Contact CreateMaster(ContactMaint graph, ContactConversionOptions _)
  {
    Contact master = base.CreateMaster(graph, _);
    this.MapContactMethod(((PXSelectBase<DocumentContactMethod>) this.ContactMethod).Current ?? ((PXSelectBase<DocumentContactMethod>) this.ContactMethod).SelectSingle(Array.Empty<object>()), master);
    this.TransferActivities(graph, master);
    return ((PXSelectBase<Contact>) graph.Contact).Update(master);
  }

  /// <summary>
  /// Updates CRRelation view of related Graph with new ContactID if needed
  /// </summary>
  /// <param name="contact"></param>
  /// <param name="options"></param>
  protected virtual void FillDependedRelation(Contact contact, ContactConversionOptions options)
  {
    if (options?.GraphWithRelation == null)
      return;
    Document current = ((PXSelectBase<Document>) this.Documents).Current;
    IEnumerable cached = ((PXCache) GraphHelper.Caches<CRRelation>(options.GraphWithRelation))?.Cached;
    if (cached == null)
      return;
    foreach (CRRelation crRelation in cached)
    {
      Guid? targetNoteId = crRelation.TargetNoteID;
      Guid? noteId = current.NoteID;
      if ((targetNoteId.HasValue == noteId.HasValue ? (targetNoteId.HasValue ? (targetNoteId.GetValueOrDefault() == noteId.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && !crRelation.ContactID.HasValue)
      {
        crRelation.ContactID = contact.ContactID;
        ((PXCache) GraphHelper.Caches<CRRelation>(options.GraphWithRelation)).RaiseFieldUpdated<CRRelation.contactID>((object) crRelation, (object) null);
        GraphHelper.MarkUpdated((PXCache) GraphHelper.Caches<CRRelation>(options.GraphWithRelation), (object) crRelation);
        options.GraphWithRelation.Actions.PressSave();
      }
    }
  }

  protected override void ReverseDocumentUpdate(ContactMaint graph, Contact entity)
  {
    Document current = ((PXSelectBase<Document>) this.Documents).Current;
    ((PXSelectBase) this.Documents).Cache.SetValue<Document.refContactID>((object) current, (object) entity.ContactID);
    GraphHelper.Caches<TMain>((PXGraph) graph).Update(this.GetMain(current));
    DocumentContact documentContact = ((PXSelectBase<DocumentContact>) this.Contacts).SelectSingle(Array.Empty<object>());
    ((PXSelectBase) this.Contacts).Cache.SetValue<DocumentContact.firstName>((object) documentContact, (object) entity.FirstName);
    ((PXSelectBase) this.Contacts).Cache.SetValue<DocumentContact.lastName>((object) documentContact, (object) entity.LastName);
    ((PXSelectBase) this.Contacts).Cache.SetValue<DocumentContact.fullName>((object) documentContact, (object) entity.FullName);
    ((PXSelectBase) this.Contacts).Cache.SetValue<DocumentContact.salutation>((object) documentContact, (object) entity.Salutation);
    ((PXSelectBase) this.Contacts).Cache.SetValue<DocumentContact.phone1>((object) documentContact, (object) entity.Phone1);
    ((PXSelectBase) this.Contacts).Cache.SetValue<DocumentContact.phone1Type>((object) documentContact, (object) entity.Phone1Type);
    ((PXSelectBase) this.Contacts).Cache.SetValue<DocumentContact.phone2>((object) documentContact, (object) entity.Phone2);
    ((PXSelectBase) this.Contacts).Cache.SetValue<DocumentContact.phone2Type>((object) documentContact, (object) entity.Phone2Type);
    ((PXSelectBase) this.Contacts).Cache.SetValue<DocumentContact.email>((object) documentContact, (object) entity.EMail);
    object main = ((PXSelectBase) this.Contacts).Cache.GetMain<DocumentContact>(documentContact);
    ((PXGraph) graph).Caches[main.GetType()].Update(main);
  }

  protected virtual void TransferActivities(ContactMaint graph, Contact contact)
  {
    foreach (PXResult<CRPMTimeActivity> pxResult in this.Activities.Select(Array.Empty<object>()))
    {
      CRPMTimeActivity crpmTimeActivity = PXResult<CRPMTimeActivity>.op_Implicit(pxResult);
      crpmTimeActivity.ContactID = contact.ContactID;
      ((PXSelectBase<CRPMTimeActivity>) ((PXGraph) graph).GetExtension<ContactMaint_ActivityDetailsExt>().Activities).Update(crpmTimeActivity);
    }
  }

  protected class DocumentContactMethodMapping : IBqlMapping
  {
    protected System.Type _table;
    public System.Type Method = typeof (DocumentContactMethod.method);
    public System.Type NoFax = typeof (DocumentContactMethod.noFax);
    public System.Type NoMail = typeof (DocumentContactMethod.noMail);
    public System.Type NoMarketing = typeof (DocumentContactMethod.noMarketing);
    public System.Type NoCall = typeof (DocumentContactMethod.noCall);
    public System.Type NoEMail = typeof (DocumentContactMethod.noEMail);
    public System.Type NoMassMail = typeof (DocumentContactMethod.noMassMail);

    public System.Type Extension => typeof (DocumentContactMethod);

    public System.Type Table => this._table;

    public DocumentContactMethodMapping(System.Type table) => this._table = table;
  }
}
