// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.ContactAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.GDPR;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.CS;

public abstract class ContactAttribute : SharedRecordAttribute
{
  protected 
  #nullable disable
  Dictionary<object, bool> _canceled;

  public ContactAttribute(System.Type AddressIDType, System.Type IsDefaultAddressType, System.Type SelectType)
    : base(AddressIDType, IsDefaultAddressType, SelectType)
  {
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.RowUpdatingEvents rowUpdating = sender.Graph.RowUpdating;
    System.Type recordType1 = this._RecordType;
    ContactAttribute contactAttribute1 = this;
    // ISSUE: virtual method pointer
    PXRowUpdating pxRowUpdating = new PXRowUpdating((object) contactAttribute1, __vmethodptr(contactAttribute1, Record_RowUpdating));
    rowUpdating.AddHandler(recordType1, pxRowUpdating);
    PXGraph.RowInsertingEvents rowInserting = sender.Graph.RowInserting;
    System.Type recordType2 = this._RecordType;
    ContactAttribute contactAttribute2 = this;
    // ISSUE: virtual method pointer
    PXRowInserting pxRowInserting = new PXRowInserting((object) contactAttribute2, __vmethodptr(contactAttribute2, Record_RowInserting));
    rowInserting.AddHandler(recordType2, pxRowInserting);
    this._canceled = new Dictionary<object, bool>();
    sender.Graph.OnBeforeCommit += (Action<PXGraph>) (graph =>
    {
      foreach (object obj in NonGenericIEnumerableExtensions.Concat_(sender.Inserted, sender.Updated))
      {
        int? nullable = (int?) sender.GetValue(obj, this._FieldName);
        int num = 0;
        if (nullable.GetValueOrDefault() < num & nullable.HasValue && graph.Views.Caches.Contains(this._RecordType))
        {
          string displayName = graph.Caches[this._RecordType].DisplayName;
          throw new PXException("The document cannot be saved because the {0} field in the database record that corresponds to this document is corrupted. Please try to save the document again. In case the issue remains, contact your Acumatica support provider for the assistance.", new object[1]
          {
            (object) (!string.IsNullOrEmpty(displayName) ? $"{PXUIFieldAttribute.GetDisplayName(sender, this._FieldName)} ({displayName})" : PXUIFieldAttribute.GetDisplayName(sender, this._FieldName))
          });
        }
      }
    });
  }

  protected virtual void Record_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!e.ExternalCall)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void Record_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (this._canceled.ContainsKey(e.NewRow))
    {
      ((CancelEventArgs) e).Cancel = true;
      if (sender.GetStatus(e.NewRow) == 1)
        sender.SetStatus(e.NewRow, (PXEntryStatus) 0);
    }
    object obj1 = sender.GetValue(e.NewRow, this._RecordID);
    object obj2 = sender.GetValue(e.NewRow, this._IsDefault);
    if (Convert.ToInt32(obj1) <= 0 || !Convert.ToBoolean(obj2) || sender.GetStatus(e.NewRow) != 1)
      return;
    sender.SetStatus(e.NewRow, (PXEntryStatus) 0);
  }

  public void Contact_IsDefaultContact_FieldVerifying<TContact>(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
    where TContact : class, IBqlTable, IContact, new()
  {
    PXCache cach = sender.Graph.Caches[this._ItemType];
    int? nullable1 = (int?) cach.GetValue(cach.Current, this._FieldOrdinal);
    int? contactId = ((TContact) e.Row).ContactID;
    int? nullable2 = nullable1;
    if (!(contactId.GetValueOrDefault() == nullable2.GetValueOrDefault() & contactId.HasValue == nullable2.HasValue))
      return;
    bool? newValue = (bool?) e.NewValue;
    bool flag = false;
    if (newValue.GetValueOrDefault() == flag & newValue.HasValue)
    {
      int? nullable3 = ((TContact) e.Row).ContactID;
      int num = 0;
      if (nullable3.GetValueOrDefault() > num & nullable3.HasValue)
      {
        ((CancelEventArgs) e).Cancel = true;
        e.NewValue = (object) true;
        this._canceled[e.Row] = true;
        TContact copy = (TContact) sender.CreateCopy((object) (TContact) e.Row);
        // ISSUE: variable of a boxed type
        __Boxed<TContact> local = (object) copy;
        nullable3 = new int?();
        int? nullable4 = nullable3;
        local.ContactID = nullable4;
        copy.IsDefaultContact = new bool?(false);
        TContact contact = (TContact) sender.Insert((object) copy);
        cach.SetValue(cach.Current, this._FieldOrdinal, (object) contact.ContactID);
        GraphHelper.MarkUpdated(cach, cach.Current);
        return;
      }
    }
    if (e.NewValue == null || !(bool) e.NewValue)
      return;
    newValue = (bool?) e.NewValue;
    if (!newValue.GetValueOrDefault())
      return;
    ((CancelEventArgs) e).Cancel = true;
    e.NewValue = (object) false;
    this.DefaultRecord(cach, cach.Current, e.Row);
    if (e.ExternalCall)
    {
      sender.SetValuePending<ContactAttribute.title>(e.Row, PXCache.NotSetValue);
      sender.SetValuePending<ContactAttribute.salutation>(e.Row, PXCache.NotSetValue);
      sender.SetValuePending<ContactAttribute.attention>(e.Row, PXCache.NotSetValue);
      sender.SetValuePending<ContactAttribute.fullName>(e.Row, PXCache.NotSetValue);
      sender.SetValuePending<ContactAttribute.email>(e.Row, PXCache.NotSetValue);
      sender.SetValuePending<ContactAttribute.phone1>(e.Row, PXCache.NotSetValue);
      sender.SetValuePending<ContactAttribute.phone1Type>(e.Row, PXCache.NotSetValue);
      sender.SetValuePending<ContactAttribute.phone2>(e.Row, PXCache.NotSetValue);
      sender.SetValuePending<ContactAttribute.phone2Type>(e.Row, PXCache.NotSetValue);
      sender.SetValuePending<ContactAttribute.phone3>(e.Row, PXCache.NotSetValue);
      sender.SetValuePending<ContactAttribute.phone3Type>(e.Row, PXCache.NotSetValue);
      sender.SetValuePending<ContactAttribute.fax>(e.Row, PXCache.NotSetValue);
      sender.SetValuePending<ContactAttribute.faxType>(e.Row, PXCache.NotSetValue);
    }
    GraphHelper.MarkUpdated(cach, cach.Current);
  }

  public virtual void DefaultContact<TContact, TContactID>(
    PXCache sender,
    object DocumentRow,
    object ContactRow)
    where TContact : class, IBqlTable, IContact, new()
    where TContactID : IBqlField
  {
    PXView view = sender.Graph.TypedViews.GetView(this._Select, false);
    int num1 = -1;
    int num2 = 0;
    bool flag = false;
    object[] objArray = new object[1]{ DocumentRow };
    ref int local1 = ref num1;
    ref int local2 = ref num2;
    using (List<object>.Enumerator enumerator = view.Select(objArray, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref local1, 1, ref local2).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXResult current = (PXResult) enumerator.Current;
        flag = ContactAttribute.DefaultContact<TContact, TContactID>(sender, this.FieldName, DocumentRow, ContactRow, (object) current);
      }
    }
    if (flag || this._Required)
      return;
    this.ClearRecord(sender, DocumentRow);
  }

  public static bool DefaultContact<TContact, TContactID>(
    PXCache sender,
    string FieldName,
    object DocumentRow,
    object ContactRow,
    object SourceRow)
    where TContact : class, IBqlTable, IContact, new()
    where TContactID : IBqlField
  {
    bool flag = false;
    if (SourceRow != null)
    {
      if (!(ContactRow is TContact contact1))
        contact1 = PXResultset<TContact>.op_Implicit(PXSelectBase<TContact, PXSelect<TContact, Where<TContactID, Equal<PX.Data.Required<TContactID>>>>.Config>.Select(sender.Graph, new object[1]
        {
          sender.GetValue(DocumentRow, FieldName)
        }));
      if (!PXResult.Unwrap<TContact>(SourceRow).ContactID.HasValue || sender.GetValue(DocumentRow, FieldName) == null)
      {
        int? nullable;
        if ((object) contact1 != null)
        {
          nullable = contact1.ContactID;
          int num = 0;
          if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
            goto label_7;
        }
        contact1 = new TContact();
label_7:
        contact1.BAccountContactID = PXResult.Unwrap<Contact>(SourceRow).ContactID;
        contact1.BAccountID = PXResult.Unwrap<Contact>(SourceRow).BAccountID;
        contact1.RevisionID = PXResult.Unwrap<Contact>(SourceRow).RevisionID;
        contact1.IsDefaultContact = new bool?(true);
        contact1.FullName = PXResult.Unwrap<Contact>(SourceRow).FullName;
        contact1.Salutation = PXResult.Unwrap<Contact>(SourceRow).Salutation;
        contact1.Attention = PXResult.Unwrap<Contact>(SourceRow).Attention;
        contact1.Title = PXResult.Unwrap<Contact>(SourceRow).Title;
        contact1.Phone1 = PXResult.Unwrap<Contact>(SourceRow).Phone1;
        contact1.Phone1Type = PXResult.Unwrap<Contact>(SourceRow).Phone1Type;
        contact1.Phone2 = PXResult.Unwrap<Contact>(SourceRow).Phone2;
        contact1.Phone2Type = PXResult.Unwrap<Contact>(SourceRow).Phone2Type;
        contact1.Phone3 = PXResult.Unwrap<Contact>(SourceRow).Phone3;
        contact1.Phone3Type = PXResult.Unwrap<Contact>(SourceRow).Phone3Type;
        contact1.Fax = PXResult.Unwrap<Contact>(SourceRow).Fax;
        contact1.FaxType = PXResult.Unwrap<Contact>(SourceRow).FaxType;
        contact1.Email = PXResult.Unwrap<Contact>(SourceRow).EMail;
        if (contact1 is IPersonalContact personalContact)
        {
          personalContact.FirstName = PXResult.Unwrap<Contact>(SourceRow).FirstName;
          personalContact.LastName = PXResult.Unwrap<Contact>(SourceRow).LastName;
          personalContact.WebSite = PXResult.Unwrap<Contact>(SourceRow).WebSite;
        }
        if (contact1 is IConsentable consentable)
        {
          consentable.ConsentAgreement = PXResult.Unwrap<Contact>(SourceRow).ConsentAgreement;
          consentable.ConsentDate = PXResult.Unwrap<Contact>(SourceRow).ConsentDate;
          consentable.ConsentExpirationDate = PXResult.Unwrap<Contact>(SourceRow).ConsentExpirationDate;
        }
        nullable = contact1.BAccountContactID;
        int num1;
        if (nullable.HasValue)
        {
          nullable = contact1.BAccountID;
          if (nullable.HasValue)
          {
            nullable = contact1.RevisionID;
            num1 = nullable.HasValue ? 1 : 0;
            goto label_15;
          }
        }
        num1 = 0;
label_15:
        flag = num1 != 0;
        nullable = contact1.ContactID;
        if (!nullable.HasValue)
        {
          TContact contact2 = (TContact) sender.Graph.Caches[typeof (TContact)].Insert((object) contact1);
          sender.SetValue(DocumentRow, FieldName, (object) contact2.ContactID);
        }
        else if (ContactRow == null)
          sender.Graph.Caches[typeof (TContact)].Update((object) contact1);
      }
      else
      {
        int? contactId;
        if ((object) contact1 != null)
        {
          contactId = contact1.ContactID;
          int num = 0;
          if (contactId.GetValueOrDefault() < num & contactId.HasValue)
            sender.Graph.Caches[typeof (TContact)].Delete((object) contact1);
        }
        sender.SetValue(DocumentRow, FieldName, (object) PXResult.Unwrap<TContact>(SourceRow).ContactID);
        contactId = PXResult.Unwrap<TContact>(SourceRow).ContactID;
        flag = contactId.HasValue;
      }
    }
    return flag;
  }

  protected void CopyContact<TContact, TContactID>(
    PXCache sender,
    object DocumentRow,
    object SourceRow,
    bool clone)
    where TContact : class, IBqlTable, IContact, new()
    where TContactID : IBqlField
  {
    if (!(SourceRow is IContact contact1))
      contact1 = (IContact) PXResultset<TContact>.op_Implicit(PXSelectBase<TContact, PXSelect<TContact, Where<TContactID, Equal<PX.Data.Required<TContactID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        sender.GetValue(SourceRow, this._FieldOrdinal)
      }));
    IContact source = contact1;
    if (source != null && (clone || !source.IsDefaultContact.GetValueOrDefault()))
    {
      int? nullable = (int?) sender.GetValue(DocumentRow, this._FieldOrdinal);
      int num = 0;
      if (nullable.GetValueOrDefault() < num & nullable.HasValue)
      {
        TContact contact2 = new TContact();
        contact2.ContactID = (int?) sender.GetValue(DocumentRow, this._FieldOrdinal);
        ContactAttribute.CopyContact((IContact) sender.Graph.Caches[typeof (TContact)].Locate((object) contact2), source);
      }
      else
      {
        TContact dest = new TContact();
        ContactAttribute.CopyContact((IContact) dest, source);
        TContact contact3 = (TContact) sender.Graph.Caches[typeof (TContact)].Insert((object) dest);
        if ((object) contact3 == null)
          return;
        sender.SetValue(DocumentRow, this.FieldOrdinal, (object) contact3.ContactID);
      }
    }
    else
      this.DefaultContact<TContact, TContactID>(sender, DocumentRow, (object) null);
  }

  public static void CopyContact(IContact dest, IContact source)
  {
    dest.BAccountID = source.BAccountID;
    dest.BAccountContactID = source.BAccountContactID;
    dest.RevisionID = source.RevisionID;
    dest.IsDefaultContact = source.IsDefaultContact;
    dest.FullName = source.FullName;
    dest.Salutation = source.Salutation;
    dest.Attention = source.Attention;
    dest.Title = source.Title;
    dest.Phone1 = source.Phone1;
    dest.Phone1Type = source.Phone1Type;
    dest.Phone2 = source.Phone2;
    dest.Phone2Type = source.Phone2Type;
    dest.Phone3 = source.Phone3;
    dest.Phone3Type = source.Phone3Type;
    dest.Fax = source.Fax;
    dest.FaxType = source.FaxType;
    dest.Email = source.Email;
    ((INotable) dest).NoteID = new Guid?();
  }

  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactAttribute.title>
  {
  }

  public abstract class salutation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactAttribute.salutation>
  {
  }

  public abstract class attention : IBqlField, IBqlOperand
  {
  }

  public abstract class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactAttribute.fullName>
  {
  }

  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactAttribute.email>
  {
  }

  public abstract class fax : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactAttribute.fax>
  {
  }

  public abstract class faxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactAttribute.faxType>
  {
  }

  public abstract class phone1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactAttribute.phone1>
  {
  }

  public abstract class phone1Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactAttribute.phone1Type>
  {
  }

  public abstract class phone2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactAttribute.phone2>
  {
  }

  public abstract class phone2Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactAttribute.phone2Type>
  {
  }

  public abstract class phone3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactAttribute.phone3>
  {
  }

  public abstract class phone3Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactAttribute.phone3Type>
  {
  }
}
