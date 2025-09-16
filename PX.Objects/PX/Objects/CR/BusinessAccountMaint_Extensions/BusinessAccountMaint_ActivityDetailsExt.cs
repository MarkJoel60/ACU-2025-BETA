// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BusinessAccountMaint_Extensions.BusinessAccountMaint_ActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR.Extensions;

#nullable disable
namespace PX.Objects.CR.BusinessAccountMaint_Extensions;

public class BusinessAccountMaint_ActivityDetailsExt : 
  ActivityDetailsExt<BusinessAccountMaint, BAccount>
{
  public override System.Type GetLinkConditionClause()
  {
    return typeof (Where<CRPMTimeActivity.bAccountID, Equal<Current<BAccount.bAccountID>>>);
  }

  public override System.Type GetBAccountIDCommand() => typeof (BAccount.bAccountID);

  public override string GetCustomMailTo()
  {
    BAccount current = ((PXSelectBase<BAccount>) this.Base.BAccount).Current;
    if (current == null)
      return (string) null;
    Contact contact = Contact.PK.Find((PXGraph) this.Base, current.DefContactID);
    return string.IsNullOrWhiteSpace(contact?.EMail) ? (string) null : PXDBEmailAttribute.FormatAddressesWithSingleDisplayName(contact.EMail, contact.DisplayName);
  }

  [PXSelector(typeof (Contact.contactID), DescriptionField = typeof (Contact.memberName))]
  [PXMergeAttributes]
  protected virtual void _(Events.CacheAttached<CRPMTimeActivity.contactID> e)
  {
  }

  [PXDBDefault(typeof (BAccount.bAccountID))]
  [PXDBChildIdentity(typeof (BAccount.bAccountID))]
  [PXMergeAttributes]
  protected virtual void _(
    Events.CacheAttached<CRPMTimeActivity.bAccountID> e)
  {
  }

  protected virtual void _(Events.RowSelected<BAccount> e)
  {
    if (e.Row == null)
      return;
    CRCustomerClass parent = (CRCustomerClass) PrimaryKeyOf<CRCustomerClass>.By<CRCustomerClass.cRCustomerClassID>.ForeignKeyOf<BAccount>.By<BAccount.classID>.FindParent((PXGraph) this.Base, (BAccount.classID) e.Row, (PKFindOptions) 0);
    if (parent == null)
      return;
    this.DefaultEmailAccountID = parent.DefaultEMailAccountID;
  }
}
