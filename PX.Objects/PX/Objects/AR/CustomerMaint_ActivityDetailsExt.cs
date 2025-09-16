// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerMaint_ActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;

#nullable disable
namespace PX.Objects.AR;

public class CustomerMaint_ActivityDetailsExt : ActivityDetailsExt<CustomerMaint, Customer>
{
  public override System.Type GetLinkConditionClause()
  {
    return typeof (Where<CRPMTimeActivity.bAccountID, Equal<Current<Customer.bAccountID>>>);
  }

  public override System.Type GetBAccountIDCommand() => typeof (Customer.bAccountID);

  public override string GetCustomMailTo()
  {
    Customer current = ((PXSelectBase<Customer>) this.Base.BAccount).Current;
    if (current == null)
      return (string) null;
    PX.Objects.CR.Contact contact = PX.Objects.CR.Contact.PK.Find((PXGraph) this.Base, current.DefContactID);
    return string.IsNullOrWhiteSpace(contact?.EMail) ? (string) null : PXDBEmailAttribute.FormatAddressesWithSingleDisplayName(contact.EMail, contact.DisplayName);
  }
}
