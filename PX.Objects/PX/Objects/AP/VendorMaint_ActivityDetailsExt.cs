// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorMaint_ActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;

#nullable disable
namespace PX.Objects.AP;

public class VendorMaint_ActivityDetailsExt : ActivityDetailsExt<VendorMaint, VendorR>
{
  public override System.Type GetLinkConditionClause()
  {
    return typeof (Where<CRPMTimeActivity.bAccountID, Equal<Current<VendorR.bAccountID>>>);
  }

  public override System.Type GetBAccountIDCommand() => typeof (Vendor.bAccountID);

  public override string GetCustomMailTo()
  {
    VendorR current = this.Base.BAccount.Current;
    if (current == null)
      return (string) null;
    PX.Objects.CR.Contact contact = PX.Objects.CR.Contact.PK.Find((PXGraph) this.Base, current.DefContactID);
    return string.IsNullOrWhiteSpace(contact?.EMail) ? (string) null : PXDBEmailAttribute.FormatAddressesWithSingleDisplayName(contact.EMail, contact.DisplayName);
  }
}
