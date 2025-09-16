// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorCustomerAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorCustomerAttribute : PXDimensionSelectorAttribute
{
  public FSSelectorCustomerAttribute()
    : base("BIZACCT", BqlCommand.Compose(new System.Type[9]
    {
      typeof (Search2<,,>),
      typeof (PX.Objects.AR.Customer.bAccountID),
      typeof (LeftJoin<,,>),
      typeof (PX.Objects.CR.Contact),
      typeof (On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AR.Customer.defContactID>>>),
      typeof (LeftJoin<,>),
      typeof (PX.Objects.CR.Address),
      typeof (On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.AR.Customer.defAddressID>>>),
      typeof (Where<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>)
    }), typeof (PX.Objects.AR.Customer.acctCD), FSSelectorBusinessAccount_BaseAttribute.CustomerSelectorColumns)
  {
    this.DescriptionField = typeof (PX.Objects.AR.Customer.acctName);
    this.Filterable = true;
  }
}
