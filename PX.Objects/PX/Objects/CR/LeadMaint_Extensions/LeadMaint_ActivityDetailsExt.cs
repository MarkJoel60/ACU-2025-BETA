// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.LeadMaint_Extensions.LeadMaint_ActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR.Extensions;

#nullable disable
namespace PX.Objects.CR.LeadMaint_Extensions;

public class LeadMaint_ActivityDetailsExt : ActivityDetailsExt<LeadMaint, CRLead, CRLead.noteID>
{
  public override System.Type GetBAccountIDCommand() => typeof (CRLead.bAccountID);

  public override System.Type GetContactIDCommand() => typeof (CRLead.refContactID);

  public override string GetCustomMailTo()
  {
    CRLead current = ((PXSelectBase<CRLead>) this.Base.Lead).Current;
    return string.IsNullOrWhiteSpace(current?.EMail) ? (string) null : PXDBEmailAttribute.FormatAddressesWithSingleDisplayName(current.EMail, current.DisplayName);
  }

  [PXDBChildIdentity(typeof (CRLead.contactID))]
  [PXMergeAttributes]
  protected virtual void _(Events.CacheAttached<CRPMTimeActivity.contactID> e)
  {
  }

  [PopupMessage]
  [PXDBDefault(typeof (CRLead.bAccountID))]
  [PXMergeAttributes]
  protected virtual void _(
    Events.CacheAttached<CRPMTimeActivity.bAccountID> e)
  {
  }

  protected virtual void _(Events.RowSelected<CRLead> e)
  {
    if (e.Row == null)
      return;
    CRLeadClass parent = (CRLeadClass) PrimaryKeyOf<CRLeadClass>.By<CRLeadClass.classID>.ForeignKeyOf<CRLead>.By<CRLead.classID>.FindParent((PXGraph) this.Base, (CRLead.classID) e.Row, (PKFindOptions) 0);
    if (parent == null)
      return;
    this.DefaultEmailAccountID = parent.DefaultEMailAccountID;
  }
}
