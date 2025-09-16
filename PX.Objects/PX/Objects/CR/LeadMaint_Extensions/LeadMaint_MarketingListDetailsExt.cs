// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.LeadMaint_Extensions.LeadMaint_MarketingListDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions;

#nullable disable
namespace PX.Objects.CR.LeadMaint_Extensions;

/// <exclude />
public class LeadMaint_MarketingListDetailsExt : 
  MarketingListDetailsExt<LeadMaint, CRLead, CRLead.contactID>
{
  [PXDBDefault(typeof (CRLead.contactID))]
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Name")]
  [PXSelector(typeof (Search<CRLead.contactID>), new System.Type[] {typeof (CRLead.fullName), typeof (CRLead.displayName), typeof (Contact.eMail), typeof (Contact.phone1), typeof (CRLead.bAccountID), typeof (CRLead.salutation), typeof (CRLead.contactType), typeof (CRLead.isActive), typeof (CRLead.memberName)}, DescriptionField = typeof (CRLead.memberName), Filterable = true, DirtyRead = true)]
  [PXParent(typeof (Select<CRLead, Where<CRLead.contactID, Equal<Current<CRMarketingListMember.contactID>>>>))]
  [PXMergeAttributes]
  protected virtual void _(
    Events.CacheAttached<CRMarketingListMember.contactID> e)
  {
  }
}
