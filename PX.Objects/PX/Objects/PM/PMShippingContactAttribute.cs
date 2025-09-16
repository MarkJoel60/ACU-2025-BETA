// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMShippingContactAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.PM;

/// <summary>Attribute for shipping contact field.</summary>
public class PMShippingContactAttribute : ContactAttribute
{
  protected Type customerID;

  public PMShippingContactAttribute(Type SelectType, Type customerID)
    : base(typeof (PMShippingContact.contactID), typeof (PMShippingContact.isDefaultContact), SelectType)
  {
    this.customerID = customerID;
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
    PMShippingContactAttribute contactAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) contactAttribute, __vmethodptr(contactAttribute, Record_Override_FieldVerifying));
    fieldVerifying.AddHandler<PMShippingContact.overrideContact>(pxFieldVerifying);
  }

  public override void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(this.customerID != (Type) null) || !((int?) sender.GetValue(e.Row, this.customerID.Name)).HasValue)
      return;
    base.RowInserted(sender, e);
  }

  public override void DefaultRecord(PXCache sender, object DocumentRow, object Row)
  {
    this.DefaultContact<PMShippingContact, PMShippingContact.contactID>(sender, DocumentRow, Row);
  }

  public override void CopyRecord(
    PXCache sender,
    object DocumentRow,
    object SourceRow,
    bool clone)
  {
    this.CopyContact<PMShippingContact, PMShippingContact.contactID>(sender, DocumentRow, SourceRow, clone);
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
      this.Contact_IsDefaultContact_FieldVerifying<PMShippingContact>(sender, e);
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
    PXUIFieldAttribute.SetEnabled<PMShippingContact.overrideContact>(sender, e.Row, sender.AllowUpdate);
  }
}
