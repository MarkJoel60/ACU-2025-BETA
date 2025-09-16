// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMBillingContactAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public class PMBillingContactAttribute : ContactAttribute
{
  protected Type customerID;

  public PMBillingContactAttribute(Type SelectType, Type customerID)
    : base(typeof (PMBillingContact.contactID), typeof (PMBillingContact.isDefaultContact), SelectType)
  {
    this.customerID = customerID;
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
    PMBillingContactAttribute contactAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) contactAttribute, __vmethodptr(contactAttribute, Record_Override_FieldVerifying));
    fieldVerifying.AddHandler<PMBillingContact.overrideContact>(pxFieldVerifying);
  }

  public override void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(this.customerID != (Type) null) || !((int?) sender.GetValue(e.Row, this.customerID.Name)).HasValue)
      return;
    base.RowInserted(sender, e);
  }

  public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    object key = sender.GetValue(e.Row, this._FieldOrdinal);
    if (key == null)
      return;
    PXCache billingCache = sender.Graph.Caches[this._RecordType];
    PXCache cach = sender.Graph.Caches[typeof (PMContact)];
    if (Convert.ToInt32(key) < 0)
    {
      PMBillingContact pmBillingContact = GraphHelper.RowCast<PMBillingContact>(billingCache.Inserted).Where<PMBillingContact>((Func<PMBillingContact, bool>) (x => object.Equals(key, billingCache.GetValue((object) x, this._RecordID)))).FirstOrDefault<PMBillingContact>();
      if (pmBillingContact == null)
        return;
      object copy = cach.CreateCopy((object) pmBillingContact);
      object obj = cach.Insert(copy);
      cach.Persist((PXDBOperation) 2);
      cach.SetStatus(obj, (PXEntryStatus) 0);
      int? contactId = (obj as PMContact).ContactID;
      this._KeyToAbort = sender.GetValue(e.Row, this._FieldOrdinal);
      sender.SetValue(e.Row, this._FieldOrdinal, (object) contactId);
      billingCache.Clear();
    }
    else
    {
      PMBillingContact pmBillingContact = GraphHelper.RowCast<PMBillingContact>(billingCache.Updated).Where<PMBillingContact>((Func<PMBillingContact, bool>) (x => object.Equals(key, billingCache.GetValue((object) x, this._RecordID)))).FirstOrDefault<PMBillingContact>();
      if (pmBillingContact == null || pmBillingContact.IsDefaultContact.GetValueOrDefault())
        return;
      object copy = cach.CreateCopy((object) pmBillingContact);
      cach.Update(copy);
      billingCache.Clear();
    }
  }

  public override void DefaultRecord(PXCache sender, object DocumentRow, object Row)
  {
    this.DefaultContact<PMBillingContact, PMBillingContact.contactID>(sender, DocumentRow, Row);
  }

  public override void CopyRecord(
    PXCache sender,
    object DocumentRow,
    object SourceRow,
    bool clone)
  {
    this.CopyContact<PMBillingContact, PMBillingContact.contactID>(sender, DocumentRow, SourceRow, clone);
  }

  public override void Record_IsDefault_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
  }

  public virtual void Record_Override_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    PXFieldVerifyingEventArgs verifyingEventArgs1 = e;
    object obj1;
    if (e.NewValue != null)
    {
      bool? newValue = (bool?) e.NewValue;
      bool flag = false;
      obj1 = (object) (newValue.GetValueOrDefault() == flag & newValue.HasValue);
    }
    else
      obj1 = e.NewValue;
    verifyingEventArgs1.NewValue = obj1;
    try
    {
      this.Contact_IsDefaultContact_FieldVerifying<PMBillingContact>(sender, e);
    }
    finally
    {
      PXFieldVerifyingEventArgs verifyingEventArgs2 = e;
      object obj2;
      if (e.NewValue != null)
      {
        bool? newValue = (bool?) e.NewValue;
        bool flag = false;
        obj2 = (object) (newValue.GetValueOrDefault() == flag & newValue.HasValue);
      }
      else
        obj2 = e.NewValue;
      verifyingEventArgs2.NewValue = obj2;
    }
  }

  protected override void Record_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    base.Record_RowSelected(sender, e);
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<PMBillingContact.overrideContact>(sender, e.Row, sender.AllowUpdate);
  }
}
