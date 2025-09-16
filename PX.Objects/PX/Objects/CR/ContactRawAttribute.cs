// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ContactRawAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.CR;

[PXDBInt]
[PXInt]
[PXUIField]
[PXRestrictor(typeof (Where<Contact.isActive, Equal<True>>), "Contact '{0}' is inactive or closed.", new System.Type[] {typeof (Contact.displayName)})]
public class ContactRawAttribute : 
  PXEntityAttribute,
  IPXFieldDefaultingSubscriber,
  IPXFieldVerifyingSubscriber
{
  protected System.Type[] ContactTypes = new System.Type[1]
  {
    typeof (ContactTypesAttribute.person)
  };
  protected System.Type BAccountIDField;
  public bool WithContactDefaultingByBAccount;
  protected PXView AvailableContacts;

  public virtual PXSelectorMode SelectorMode { get; set; } = (PXSelectorMode) 16 /*0x10*/;

  public ContactRawAttribute()
    : this((System.Type) null)
  {
  }

  public ContactRawAttribute(System.Type bAccountIDField)
    : this(bAccountIDField, (System.Type[]) null, (System.Type) null, (System.Type) null, (System.Type[]) null, (string[]) null)
  {
  }

  public ContactRawAttribute(
    System.Type bAccountIDField = null,
    System.Type[] contactTypes = null,
    System.Type customSearchField = null,
    System.Type customSearchQuery = null,
    System.Type[] fieldList = null,
    string[] headerList = null)
  {
    this.BAccountIDField = bAccountIDField;
    this.ContactTypes = contactTypes ?? this.ContactTypes;
    System.Type type = customSearchQuery;
    if ((object) type == null)
      type = this.CreateSelect(bAccountIDField, customSearchField);
    System.Type[] typeArray = fieldList;
    if (typeArray == null)
      typeArray = new System.Type[7]
      {
        typeof (Contact.displayName),
        typeof (Contact.salutation),
        typeof (Contact.fullName),
        typeof (BAccount.acctCD),
        typeof (Contact.eMail),
        typeof (Contact.phone1),
        typeof (Contact.contactType)
      };
    PXSelectorAttribute selectorAttribute1 = new PXSelectorAttribute(type, typeArray);
    PXSelectorAttribute selectorAttribute2 = selectorAttribute1;
    string[] strArray;
    if (headerList == null && fieldList == null)
      strArray = new string[7]
      {
        "Contact",
        "Job Title",
        "Account Name",
        "Business Account",
        "Email",
        "Phone 1",
        "Type"
      };
    else
      strArray = headerList;
    selectorAttribute2.Headers = strArray;
    selectorAttribute1.DescriptionField = typeof (Contact.displayName);
    selectorAttribute1.SelectorMode = this.SelectorMode;
    selectorAttribute1.Filterable = true;
    selectorAttribute1.DirtyRead = true;
    PXSelectorAttribute selectorAttribute3 = selectorAttribute1;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) selectorAttribute3);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    if (!(bAccountIDField != (System.Type) null))
      return;
    PXAggregateAttribute.AggregatedAttributesCollection attributes = ((PXAggregateAttribute) this)._Attributes;
    PXContactAccountDiffersRestrictorAttribute restrictorAttribute = new PXContactAccountDiffersRestrictorAttribute(BqlTemplate.OfCondition<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<BqlPlaceholder.A>, IsNull>>>>.Or<BqlOperand<Contact.bAccountID, IBqlInt>.IsEqual<BqlField<BqlPlaceholder.A, BqlPlaceholder.IBqlAny>.FromCurrent>>>>.Replace<BqlPlaceholder.A>(bAccountIDField).ToType(), new System.Type[2]
    {
      selectorAttribute3.DescriptionField,
      BqlCommand.Compose(new System.Type[3]
      {
        typeof (Selector<,>),
        bAccountIDField,
        typeof (BAccount.acctName)
      })
    });
    restrictorAttribute.ShowWarning = true;
    attributes.Add((PXEventSubscriberAttribute) restrictorAttribute);
  }

  protected virtual System.Type GetContactTypeWhere()
  {
    System.Type contactTypeWhere = (System.Type) null;
    switch (this.ContactTypes.Length)
    {
      case 1:
        contactTypeWhere = BqlCommand.Compose(new System.Type[4]
        {
          typeof (Where<,>),
          typeof (Contact.contactType),
          typeof (Equal<>),
          this.ContactTypes[0]
        });
        break;
      case 2:
        contactTypeWhere = BqlCommand.Compose(new System.Type[5]
        {
          typeof (Where<,>),
          typeof (Contact.contactType),
          typeof (In3<,>),
          this.ContactTypes[0],
          this.ContactTypes[1]
        });
        break;
      case 3:
        contactTypeWhere = BqlCommand.Compose(new System.Type[6]
        {
          typeof (Where<,>),
          typeof (Contact.contactType),
          typeof (In3<,,>),
          this.ContactTypes[0],
          this.ContactTypes[1],
          this.ContactTypes[2]
        });
        break;
      case 4:
        contactTypeWhere = BqlCommand.Compose(new System.Type[7]
        {
          typeof (Where<,>),
          typeof (Contact.contactType),
          typeof (In3<,,,>),
          this.ContactTypes[0],
          this.ContactTypes[1],
          this.ContactTypes[2],
          this.ContactTypes[3]
        });
        break;
      case 5:
        contactTypeWhere = BqlCommand.Compose(new System.Type[8]
        {
          typeof (Where<,>),
          typeof (Contact.contactType),
          typeof (In3<,,,,>),
          this.ContactTypes[0],
          this.ContactTypes[1],
          this.ContactTypes[2],
          this.ContactTypes[3],
          this.ContactTypes[4]
        });
        break;
    }
    return contactTypeWhere;
  }

  protected virtual System.Type CreateSelect(System.Type bAccountIDField, System.Type customSearchField)
  {
    System.Type contactTypeWhere = this.GetContactTypeWhere();
    System.Type type1 = ((IEnumerable<System.Type>) this.ContactTypes).Contains<System.Type>(typeof (ContactTypesAttribute.employee)) ? typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.type, In3<BAccountType.branchType, BAccountType.organizationType>>>>>.And<BqlOperand<Contact.contactType, IBqlString>.IsEqual<ContactTypesAttribute.employee>>) : typeof (BqlOperand<True, IBqlBool>.IsEqual<True>);
    IBqlCommandTemplate ibqlCommandTemplate = BqlTemplate.OfCommand<FbqlSelect<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<BAccount>.On<BqlOperand<BAccount.bAccountID, IBqlInt>.IsEqual<Contact.bAccountID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.bAccountID, IsNull>>>>.Or<Match<BAccount, Current<AccessInfo.userName>>>>>, And<Brackets<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlPlaceholder.C>>, Or<BqlOperand<BAccount.type, IBqlString>.IsNull>>>.Or<BqlOperand<BAccount.type, IBqlString>.IsIn<BAccountType.prospectType, BAccountType.customerType, BAccountType.combinedType, BAccountType.vendorType>>>>>>.And<BqlPlaceholder.A>>, Contact>.SearchFor<BqlPlaceholder.B>>.Replace<BqlPlaceholder.A>(contactTypeWhere);
    System.Type type2 = customSearchField;
    if ((object) type2 == null)
      type2 = typeof (Contact.contactID);
    return ((IBqlTemplate) ibqlCommandTemplate.Replace<BqlPlaceholder.B>(type2).Replace<BqlPlaceholder.C>(type1)).ToType();
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    if (this.BAccountIDField != (System.Type) null)
    {
      BqlCommand command = BqlTemplate.OfCommand<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<BAccount>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.bAccountID, Equal<Contact.bAccountID>>>>>.And<BqlOperand<BAccount.defContactID, IBqlInt>.IsNotEqual<Contact.contactID>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact.bAccountID, Equal<P.AsInt>>>>, And<BqlOperand<Contact.isActive, IBqlBool>.IsEqual<True>>>>.And<BqlPlaceholder.A>>>.Replace<BqlPlaceholder.A>(this.GetContactTypeWhere()).ToCommand();
      this.AvailableContacts = new PXView(sender.Graph, true, command);
      PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
      System.Type itemType = sender.GetItemType();
      string name = this.BAccountIDField.Name;
      ContactRawAttribute contactRawAttribute = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) contactRawAttribute, __vmethodptr(contactRawAttribute, BAccountID_FieldUpdated));
      fieldUpdated.AddHandler(itemType, name, pxFieldUpdated);
    }
    sender.Graph.OnBeforeCommit += (Action<PXGraph>) (graph =>
    {
      foreach (object obj in NonGenericIEnumerableExtensions.Concat_(sender.Inserted, sender.Updated))
      {
        int? nullable = (int?) sender.GetValue(obj, ((PXEventSubscriberAttribute) this)._FieldName);
        int num = 0;
        if (nullable.GetValueOrDefault() < num & nullable.HasValue)
          throw new PXException("The document cannot be saved because the {0} field in the database record that corresponds to this document is corrupted. Please try to save the document again. In case the issue remains, contact your Acumatica support provider for the assistance.", new object[1]
          {
            (object) PXUIFieldAttribute.GetDisplayName(sender, ((PXEventSubscriberAttribute) this)._FieldName)
          });
      }
    });
  }

  protected virtual void BAccountID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null || this.BAccountIDField == (System.Type) null || !this.WithContactDefaultingByBAccount || sender.Graph.UnattendedMode)
      return;
    object obj = sender.GetValue(e.Row, this.BAccountIDField.Name);
    if (obj == null || object.Equals(obj, e.OldValue))
      return;
    int? contactID = sender.GetValue(e.Row, this.FieldName) as int?;
    if (this.GetAvailableContacts(obj).Any<(int?, bool)>((Func<(int?, bool), bool>) (contact =>
    {
      int? contactId = contact.ContactID;
      int? nullable = contactID;
      return contactId.GetValueOrDefault() == nullable.GetValueOrDefault() & contactId.HasValue == nullable.HasValue;
    })))
      return;
    sender.SetDefaultExt(e.Row, this.FieldName, (object) null);
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null || this.BAccountIDField == (System.Type) null || !this.WithContactDefaultingByBAccount || sender.Graph.UnattendedMode)
      return;
    object bAccountID = sender.GetValue(e.Row, this.BAccountIDField.Name);
    if (bAccountID == null)
      return;
    List<(int? ContactID, bool IsPrimary)> availableContacts = this.GetAvailableContacts(bAccountID);
    e.NewValue = (object) (availableContacts.Count == 1 ? availableContacts[0].ContactID : availableContacts.FirstOrDefault<(int?, bool)>((Func<(int?, bool), bool>) (item =>
    {
      if (item.IsPrimary)
        return true;
      int? contactId = item.ContactID;
      int? newValue = e.NewValue as int?;
      return contactId.GetValueOrDefault() == newValue.GetValueOrDefault() & contactId.HasValue == newValue.HasValue;
    })).Item1);
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null || this.BAccountIDField == (System.Type) null || !sender.Graph.IsCopyPasteContext)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual List<(int? ContactID, bool IsPrimary)> GetAvailableContacts(object bAccountID)
  {
    PXView availableContacts = this.AvailableContacts;
    if (availableContacts == null)
      return (List<(int?, bool)>) null;
    return availableContacts.SelectMulti(new object[1]
    {
      bAccountID
    }).Cast<PXResult<Contact, BAccount>>().Select<PXResult<Contact, BAccount>, (int?, bool)>((Func<PXResult<Contact, BAccount>, (int?, bool)>) (item =>
    {
      Contact contact1;
      BAccount baccount1;
      item.Deconstruct(ref contact1, ref baccount1);
      Contact contact2 = contact1;
      BAccount baccount2 = baccount1;
      int? contactId = contact2.ContactID;
      int? primaryContactId = (int?) baccount2?.PrimaryContactID;
      int? nullable = contactId;
      return (contactId, primaryContactId.GetValueOrDefault() == nullable.GetValueOrDefault() & primaryContactId.HasValue == nullable.HasValue);
    })).ToList<(int?, bool)>();
  }
}
