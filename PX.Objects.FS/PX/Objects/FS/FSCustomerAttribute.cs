// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSCustomerAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.FS;

public class FSCustomerAttribute : PXDimensionSelectorAttribute
{
  public FSCustomerAttribute()
    : base("BIZACCT", typeof (Search2<PX.Objects.AR.Customer.bAccountID, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AR.Customer.defContactID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.AR.Customer.defAddressID>>>>>, Where<PX.Objects.AR.Customer.type, Equal<BAccountType.customerType>, Or<PX.Objects.AR.Customer.type, Equal<BAccountType.combinedType>>>>), typeof (PX.Objects.AR.Customer.acctCD), new System.Type[7]
    {
      typeof (PX.Objects.AR.Customer.acctCD),
      typeof (PX.Objects.AR.Customer.status),
      typeof (PX.Objects.AR.Customer.acctName),
      typeof (PX.Objects.CR.BAccount.classID),
      typeof (PX.Objects.CR.Contact.phone1),
      typeof (PX.Objects.CR.Address.city),
      typeof (PX.Objects.CR.Address.countryID)
    })
  {
    this.DescriptionField = typeof (PX.Objects.AR.Customer.acctName);
    this.DirtyRead = true;
    this.Filterable = true;
  }
}
