// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ContactMaint_Extensions.ContactMaint_ActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR.Extensions;

#nullable disable
namespace PX.Objects.CR.ContactMaint_Extensions;

public class ContactMaint_ActivityDetailsExt : ActivityDetailsExt<ContactMaint, Contact>
{
  public override System.Type GetLinkConditionClause()
  {
    return typeof (Where<CRPMTimeActivity.contactID, Equal<Current<Contact.contactID>>>);
  }

  public override System.Type GetBAccountIDCommand() => typeof (Contact.bAccountID);

  public override System.Type GetContactIDCommand() => typeof (Contact.contactID);

  public override string GetCustomMailTo()
  {
    Contact current = ((PXSelectBase<Contact>) this.Base.Contact).Current;
    return string.IsNullOrWhiteSpace(current?.EMail) ? (string) null : PXDBEmailAttribute.FormatAddressesWithSingleDisplayName(current.EMail, current.DisplayName);
  }

  [PXDBChildIdentity(typeof (Contact.contactID))]
  [PXSelector(typeof (Contact.contactID), DescriptionField = typeof (Contact.memberName), DirtyRead = true)]
  [PXMergeAttributes]
  protected virtual void _(Events.CacheAttached<CRPMTimeActivity.contactID> e)
  {
  }

  [PXDBDefault(typeof (Contact.bAccountID))]
  [PXMergeAttributes]
  protected virtual void _(
    Events.CacheAttached<CRPMTimeActivity.bAccountID> e)
  {
  }

  protected virtual void _(Events.RowSelected<Contact> e)
  {
    if (e.Row == null)
      return;
    CRContactClass parent = (CRContactClass) PrimaryKeyOf<CRContactClass>.By<CRContactClass.classID>.ForeignKeyOf<Contact>.By<Contact.classID>.FindParent((PXGraph) this.Base, (Contact.classID) e.Row, (PKFindOptions) 0);
    if (parent == null)
      return;
    this.DefaultEmailAccountID = parent.DefaultEMailAccountID;
  }
}
