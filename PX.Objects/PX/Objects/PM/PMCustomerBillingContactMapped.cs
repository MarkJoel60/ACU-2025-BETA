// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMCustomerBillingContactMapped
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable disable
namespace PX.Objects.PM;

public class PMCustomerBillingContactMapped : 
  BqlFieldMapper<PMCustomerBillingContact, PMBillingContact>
{
  public PMCustomerBillingContactMapped()
  {
    this.Map<BqlField<PMContact.contactID, IBqlInt>.EqualTo<PMCustomerBillingContact.projectContactID>>();
    this.Map<BqlField<PMContact.customerContactID, IBqlInt>.EqualTo<PX.Objects.CR.Contact.contactID>>();
    this.Map<BqlField<PMContact.customerID, IBqlInt>.EqualTo<PX.Objects.CR.Contact.bAccountID>>();
  }
}
