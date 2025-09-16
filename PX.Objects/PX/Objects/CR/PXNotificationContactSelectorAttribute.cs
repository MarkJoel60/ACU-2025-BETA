// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PXNotificationContactSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.EP;

#nullable disable
namespace PX.Objects.CR;

public class PXNotificationContactSelectorAttribute : PXSelectorAttribute
{
  private System.Type contactType;

  public PXNotificationContactSelectorAttribute()
    : this((System.Type) null)
  {
  }

  public PXNotificationContactSelectorAttribute(System.Type contactType)
    : base(typeof (Search2<Contact.contactID, LeftJoin<BAccountR, On<BAccountR.bAccountID, Equal<Contact.bAccountID>>, LeftJoin<EPEmployee, On<EPEmployee.parentBAccountID, Equal<Contact.bAccountID>, And<EPEmployee.defContactID, Equal<Contact.contactID>>>>>, Where2<Where<Current<NotificationRecipient.contactType>, Equal<NotificationContactType.employee>, And<EPEmployee.acctCD, IsNotNull>>, Or<Where<Current<NotificationRecipient.contactType>, Equal<NotificationContactType.contact>, And<BAccountR.noteID, Equal<Current<NotificationRecipient.refNoteID>>, And<Contact.contactType, Equal<ContactTypesAttribute.person>>>>>>>))
  {
    this.SubstituteKey = typeof (Contact.displayName);
    this.contactType = contactType;
  }

  public PXNotificationContactSelectorAttribute(System.Type contactType, System.Type search)
    : base(search)
  {
    this.SubstituteKey = typeof (Contact.displayName);
    this.contactType = contactType;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!(this.contactType != (System.Type) null))
      return;
    PXGraph.RowSelectedEvents rowSelected = sender.Graph.RowSelected;
    System.Type itemType = sender.GetItemType();
    PXNotificationContactSelectorAttribute selectorAttribute = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) selectorAttribute, __vmethodptr(selectorAttribute, OnRowSelected));
    rowSelected.AddHandler(itemType, pxRowSelected);
  }

  protected virtual void OnRowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null || this.contactType == (System.Type) null)
      return;
    PXCache cach = sender.Graph.Caches[BqlCommand.GetItemType(this.contactType)];
    string str = (cach == sender ? sender.GetValue(e.Row, this.contactType.Name) : cach.GetValue(cach.Current, this.contactType.Name))?.ToString();
    bool flag = str == "C" || str == "E";
    PXUIFieldAttribute.SetEnabled(sender, e.Row, ((PXEventSubscriberAttribute) this)._FieldName, flag);
    PXDefaultAttribute.SetPersistingCheck(sender, ((PXEventSubscriberAttribute) this)._FieldName, e.Row, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }
}
