// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ContactMaint_Extensions.ContactMaint_CRRelationDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions;

#nullable disable
namespace PX.Objects.CR.ContactMaint_Extensions;

/// <inheritdoc />
public class ContactMaint_CRRelationDetailsExt : 
  CRRelationDetailsExt<ContactMaint, Contact, Contact.noteID>
{
  [PXDBChildIdentity(typeof (Contact.contactID))]
  [PXMergeAttributes]
  protected virtual void _(Events.CacheAttached<CRRelation.contactID> e)
  {
  }

  protected virtual void _(Events.RowSelected<Contact> e)
  {
    if (e.Row == null)
      return;
    bool flag = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<Contact>>) e).Cache.GetStatus((object) e.Row) != 2;
    ((PXSelectBase) this.Relations).Cache.AllowInsert = e.Row.ContactType == "PN" & flag;
    ((PXSelectBase) this.Relations).Cache.AllowDelete = e.Row.ContactType == "PN";
  }
}
