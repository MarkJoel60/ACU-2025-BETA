// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSrvOrdContactAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class FSSrvOrdContactAttribute(System.Type selectType) : FSDocumentContactAttribute(selectType)
{
  public override void DefaultContact<TContact, TContactID>(
    PXCache sender,
    object documentRow,
    object contactRow)
  {
    PXView pxView = (PXView) null;
    object obj1 = (object) null;
    FSSrvOrdType fsSrvOrdType = (FSSrvOrdType) null;
    bool flag1 = false;
    if (sender.Graph is ServiceOrderEntry)
      fsSrvOrdType = ((PXSelectBase<FSSrvOrdType>) ((ServiceOrderEntry) sender.Graph).ServiceOrderTypeSelected).Current;
    else if (sender.Graph is AppointmentEntry)
      fsSrvOrdType = ((PXSelectBase<FSSrvOrdType>) ((AppointmentEntry) sender.Graph).ServiceOrderTypeSelected).Current;
    if (fsSrvOrdType != null && fsSrvOrdType.AppAddressSource == "BL" && sender.GetValue<FSServiceOrder.branchLocationID>(documentRow) != null)
    {
      obj1 = sender.GetValue<FSServiceOrder.branchLocationID>(documentRow);
      BqlCommand instance = BqlCommand.CreateInstance(new System.Type[1]
      {
        typeof (Select2<FSBLOCContact, InnerJoin<FSBranchLocation, On<FSBranchLocation.branchLocationContactID, Equal<FSBLOCContact.contactID>>>, Where<FSBranchLocation.branchLocationID, Equal<PX.Data.Required<FSBranchLocation.branchLocationID>>>>)
      });
      pxView = sender.Graph.TypedViews.GetView(instance, false);
      flag1 = true;
    }
    else if (fsSrvOrdType != null && fsSrvOrdType.AppAddressSource == "CC" && sender.GetValue<FSServiceOrder.contactID>(documentRow) != null)
    {
      obj1 = sender.GetValue<FSServiceOrder.contactID>(documentRow);
      BqlCommand instance = BqlCommand.CreateInstance(new System.Type[1]
      {
        typeof (Select2<PX.Objects.CR.Contact, LeftJoin<FSContact, On<FSContact.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<FSContact.bAccountContactID, Equal<PX.Objects.CR.Contact.contactID>, And<FSContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<FSContact.isDefaultContact, Equal<boolTrue>, And<FSContact.entityType, Equal<ListField.ACEntityType.ServiceOrder>>>>>>>, Where<PX.Objects.CR.Contact.contactID, Equal<PX.Data.Required<FSServiceOrder.contactID>>>>)
      });
      pxView = sender.Graph.TypedViews.GetView(instance, false);
    }
    else if (fsSrvOrdType != null && fsSrvOrdType.AppAddressSource == "BA" && sender.GetValue<FSServiceOrder.locationID>(documentRow) != null)
    {
      obj1 = sender.GetValue<FSServiceOrder.locationID>(documentRow);
      BqlCommand instance = BqlCommand.CreateInstance(new System.Type[1]
      {
        typeof (Select2<PX.Objects.CR.Contact, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<PX.Data.Required<FSServiceOrder.locationID>>>, LeftJoin<FSContact, On<FSContact.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<FSContact.bAccountContactID, Equal<PX.Objects.CR.Contact.contactID>, And<FSContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<FSContact.isDefaultContact, Equal<boolTrue>, And<FSContact.entityType, Equal<ListField.ACEntityType.ServiceOrder>>>>>>>>, Where<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Location.defContactID>>>)
      });
      pxView = sender.Graph.TypedViews.GetView(instance, false);
    }
    if (pxView != null)
    {
      int num1 = -1;
      int num2 = 0;
      bool flag2 = false;
      using (List<object>.Enumerator enumerator = pxView.Select(new object[1]
      {
        documentRow
      }, new object[1]{ obj1 }, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref num1, 1, ref num2).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PXResult current = (PXResult) enumerator.Current;
          flag2 = !flag1 ? ContactAttribute.DefaultContact<TContact, TContactID>(sender, this.FieldName, documentRow, contactRow, (object) current) : this.DefaultBLOCContact<FSBLOCContact, FSBLOCContact.contactID>(sender, this.FieldName, documentRow, contactRow, (object) current);
        }
      }
      if (flag2 || this._Required)
        return;
      this.ClearRecord(sender, documentRow);
    }
    else
    {
      this.ClearRecord(sender, documentRow);
      if (!this._Required || sender.GetValue(documentRow, this._FieldOrdinal) != null)
        return;
      using (new ReadOnlyScope(new PXCache[1]
      {
        sender.Graph.Caches[this._RecordType]
      }))
      {
        object obj2 = sender.Graph.Caches[this._RecordType].Insert();
        object obj3 = sender.Graph.Caches[this._RecordType].GetValue(obj2, this._RecordID);
        sender.SetValue(documentRow, this._FieldOrdinal, obj3);
      }
    }
  }

  public virtual bool DefaultBLOCContact<TContact, TContactID>(
    PXCache sender,
    string fieldName,
    object documentRow,
    object contactRow,
    object sourceRow)
    where TContact : class, IBqlTable, IContact, new()
    where TContactID : IBqlField
  {
    bool flag = false;
    if (sourceRow != null)
    {
      if (!(contactRow is FSContact fsContact1))
        fsContact1 = FSContact.PK.Find(sender.Graph, (int?) sender.GetValue(documentRow, fieldName));
      FSContact fsContact2 = PXResult.Unwrap<FSContact>(sourceRow);
      if ((fsContact2 != null ? (!fsContact2.ContactID.HasValue ? 1 : 0) : 1) != 0 || sender.GetValue(documentRow, fieldName) == null)
      {
        int? nullable;
        if (fsContact1 != null)
        {
          nullable = fsContact1.ContactID;
          int num = 0;
          if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
            goto label_7;
        }
        fsContact1 = new FSContact();
label_7:
        fsContact1.BAccountContactID = PXResult.Unwrap<TContact>(sourceRow).ContactID;
        fsContact1.BAccountID = PXResult.Unwrap<TContact>(sourceRow).BAccountID;
        fsContact1.RevisionID = PXResult.Unwrap<TContact>(sourceRow).RevisionID;
        fsContact1.IsDefaultContact = new bool?(true);
        fsContact1.FullName = PXResult.Unwrap<TContact>(sourceRow).FullName;
        fsContact1.Salutation = PXResult.Unwrap<TContact>(sourceRow).Salutation;
        fsContact1.Attention = PXResult.Unwrap<TContact>(sourceRow).Attention;
        fsContact1.Title = PXResult.Unwrap<TContact>(sourceRow).Title;
        fsContact1.Phone1 = PXResult.Unwrap<TContact>(sourceRow).Phone1;
        fsContact1.Phone1Type = PXResult.Unwrap<TContact>(sourceRow).Phone1Type;
        fsContact1.Phone2 = PXResult.Unwrap<TContact>(sourceRow).Phone2;
        fsContact1.Phone2Type = PXResult.Unwrap<TContact>(sourceRow).Phone2Type;
        fsContact1.Phone3 = PXResult.Unwrap<TContact>(sourceRow).Phone3;
        fsContact1.Phone3Type = PXResult.Unwrap<TContact>(sourceRow).Phone3Type;
        fsContact1.Fax = PXResult.Unwrap<TContact>(sourceRow).Fax;
        fsContact1.FaxType = PXResult.Unwrap<TContact>(sourceRow).FaxType;
        fsContact1.Email = PXResult.Unwrap<TContact>(sourceRow).Email;
        nullable = fsContact1.BAccountContactID;
        int num1;
        if (nullable.HasValue)
        {
          nullable = fsContact1.BAccountID;
          if (nullable.HasValue)
          {
            nullable = fsContact1.RevisionID;
            num1 = nullable.HasValue ? 1 : 0;
            goto label_11;
          }
        }
        num1 = 0;
label_11:
        flag = num1 != 0;
        nullable = fsContact1.ContactID;
        if (!nullable.HasValue)
        {
          FSContact fsContact3 = (FSContact) sender.Graph.Caches[typeof (FSContact)].Insert((object) fsContact1);
          sender.SetValue(documentRow, fieldName, (object) fsContact3.ContactID);
        }
        else if (contactRow == null)
          sender.Graph.Caches[typeof (FSContact)].Update((object) fsContact1);
      }
      else
      {
        int? contactId;
        if (fsContact1 != null)
        {
          contactId = fsContact1.ContactID;
          int num = 0;
          if (contactId.GetValueOrDefault() < num & contactId.HasValue)
            sender.Graph.Caches[typeof (FSContact)].Delete((object) fsContact1);
        }
        sender.SetValue(documentRow, fieldName, (object) PXResult.Unwrap<TContact>(sourceRow).ContactID);
        contactId = PXResult.Unwrap<FSContact>(sourceRow).ContactID;
        flag = contactId.HasValue;
      }
    }
    return flag;
  }
}
