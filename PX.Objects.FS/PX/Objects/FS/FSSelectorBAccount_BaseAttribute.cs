// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorBAccount_BaseAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorBAccount_BaseAttribute : PXDimensionSelectorAttribute
{
  public FSSelectorBAccount_BaseAttribute(string dimensionName, System.Type whereType)
    : base(dimensionName, BqlCommand.Compose(new System.Type[14]
    {
      typeof (Search2<,,>),
      typeof (PX.Objects.CR.BAccount.bAccountID),
      typeof (LeftJoin<,,>),
      typeof (PX.Objects.CR.Contact),
      typeof (On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.BAccount.defContactID>>>),
      typeof (LeftJoin<,,>),
      typeof (PX.Objects.CR.Address),
      typeof (On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.BAccount.defAddressID>>>),
      typeof (LeftJoin<,>),
      typeof (PX.Objects.AR.Customer),
      typeof (On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>>),
      typeof (Where2<,>),
      whereType,
      typeof (And<Match<Current<AccessInfo.userName>>>)
    }), typeof (PX.Objects.CR.BAccount.acctCD), new System.Type[11]
    {
      typeof (PX.Objects.CR.BAccount.acctCD),
      typeof (PX.Objects.CR.BAccount.acctName),
      typeof (PX.Objects.CR.BAccount.type),
      typeof (PX.Objects.AR.Customer.customerClassID),
      typeof (PX.Objects.CR.BAccount.status),
      typeof (PX.Objects.CR.Contact.phone1),
      typeof (PX.Objects.CR.Address.addressLine1),
      typeof (PX.Objects.CR.Address.addressLine2),
      typeof (PX.Objects.CR.Address.postalCode),
      typeof (PX.Objects.CR.Address.city),
      typeof (PX.Objects.CR.Address.countryID)
    })
  {
    this.DescriptionField = typeof (PX.Objects.CR.BAccount.acctName);
    this.Filterable = true;
  }
}
