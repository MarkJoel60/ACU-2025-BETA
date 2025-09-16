// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APContactAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.AP;

/// <summary>Ctor</summary>
/// <param name="SelectType">Must have type IBqlSelect. This select is used for both selecting <br />
/// a source Contact record from which AP address is defaulted and for selecting version of APContact, <br />
/// created from source Contact (having  matching ContactID, revision and IsDefaultContact = true).<br />
/// - so it must include both records. See example above. <br />
/// </param>
public class APContactAttribute(System.Type SelectType) : ContactAttribute(typeof (APContact.contactID), typeof (APContact.isDefaultContact), SelectType)
{
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.Graph.FieldVerifying.AddHandler<APContact.overrideContact>(new PXFieldVerifying(this.Record_Override_FieldVerifying));
  }

  public override void DefaultRecord(PXCache sender, object DocumentRow, object Row)
  {
    this.DefaultContact<APContact, APContact.contactID>(sender, DocumentRow, Row);
    this.OverrideEmployeeFullName(sender, DocumentRow);
  }

  public override void CopyRecord(
    PXCache sender,
    object DocumentRow,
    object SourceRow,
    bool clone)
  {
    this.CopyContact<APContact, APContact.contactID>(sender, DocumentRow, SourceRow, clone);
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
      this.Contact_IsDefaultContact_FieldVerifying<APContact>(sender, e);
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
    PXUIFieldAttribute.SetEnabled<APContact.overrideContact>(sender, e.Row, sender.AllowUpdate);
  }

  /// <summary>
  /// For employees, the AP contact's full name should be defaulted from
  /// <see cref="P:PX.Objects.CR.Contact.DisplayName" /> and not <see cref="P:PX.Objects.CR.Contact.FullName" />.
  /// This method ensures correct defaulting in case of employees.
  /// </summary>
  private void OverrideEmployeeFullName(PXCache sender, object documentRow)
  {
    APContact row = (APContact) PXSelectBase<APContact, PXSelect<APContact, Where<APContact.contactID, Equal<PX.Data.Required<APContact.contactID>>>>.Config>.Select(sender.Graph, sender.GetValue(documentRow, this.FieldName));
    if (row == null)
      return;
    PX.Objects.CR.Contact contact = (PX.Objects.CR.Contact) PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<PX.Data.Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Select(sender.Graph, (object) row.BAccountContactID);
    if (contact?.ContactType != "EP")
      return;
    row.FullName = contact.DisplayName;
    sender.Graph.Caches<APContact>().MarkUpdated((object) row);
  }
}
