// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShippingContactAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO;

/// <summary>Shipping contact for the Sales Order document.</summary>
public class SOShippingContactAttribute(System.Type SelectType) : SOContactAttribute(typeof (SOShippingContact.contactID), typeof (SOShippingContact.isDefaultContact), SelectType)
{
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
    SOShippingContactAttribute contactAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) contactAttribute, __vmethodptr(contactAttribute, Record_Override_FieldVerifying));
    fieldVerifying.AddHandler<SOShippingContact.overrideContact>(pxFieldVerifying);
  }

  public override void DefaultRecord(PXCache sender, object DocumentRow, object Row)
  {
    this.DefaultContact<SOShippingContact, SOShippingContact.contactID>(sender, DocumentRow, Row);
  }

  public override void CopyRecord(
    PXCache sender,
    object DocumentRow,
    object SourceRow,
    bool clone)
  {
    this.CopyContact<SOShippingContact, SOShippingContact.contactID>(sender, DocumentRow, SourceRow, clone);
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
      this.Contact_IsDefaultContact_FieldVerifying<SOShippingContact>(sender, e);
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
    PXUIFieldAttribute.SetEnabled<SOShippingContact.overrideContact>(sender, e.Row, true);
  }

  public override void DefaultContact<TContact, TContactID>(
    PXCache sender,
    object DocumentRow,
    object AddressRow)
  {
    int? nullable = (int?) sender.GetValue<SOOrder.destinationSiteID>(DocumentRow);
    bool flag1 = false;
    PXView view;
    if (nullable.HasValue)
    {
      flag1 = true;
      BqlCommand instance = BqlCommand.CreateInstance(new System.Type[1]
      {
        typeof (Select2<PX.Objects.CR.Contact, InnerJoin<PX.Objects.IN.INSite, On2<PX.Objects.IN.INSite.FK.Contact, And<PX.Objects.IN.INSite.siteID, Equal<Current<SOOrder.destinationSiteID>>>>, LeftJoin<SOShippingContact, On<SOShippingContact.customerID, Equal<PX.Objects.CR.Contact.bAccountID>, And<SOShippingContact.customerContactID, Equal<PX.Objects.CR.Contact.contactID>, And<SOShippingContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<SOShippingContact.isDefaultContact, Equal<True>>>>>>>, Where<True, Equal<True>>>)
      });
      view = sender.Graph.TypedViews.GetView(instance, false);
    }
    else
      view = sender.Graph.TypedViews.GetView(this._Select, false);
    int num1 = -1;
    int num2 = 0;
    bool flag2 = false;
    using (List<object>.Enumerator enumerator = view.Select(new object[1]
    {
      DocumentRow
    }, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref num1, 1, ref num2).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXResult current = (PXResult) enumerator.Current;
        flag2 = ContactAttribute.DefaultContact<TContact, TContactID>(sender, this.FieldName, DocumentRow, AddressRow, (object) current);
      }
    }
    if (!flag2 && !this._Required)
      this.ClearRecord(sender, DocumentRow);
    if (((flag2 ? 0 : (this._Required ? 1 : 0)) & (flag1 ? 1 : 0)) != 0)
      throw new SharedRecordMissingException();
  }
}
