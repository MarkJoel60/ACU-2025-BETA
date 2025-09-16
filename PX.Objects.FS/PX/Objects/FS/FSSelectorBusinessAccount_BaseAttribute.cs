// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorBusinessAccount_BaseAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorBusinessAccount_BaseAttribute : PXDimensionSelectorAttribute
{
  public static readonly System.Type[] BAccountSelectorColumns = new System.Type[11]
  {
    typeof (BAccountSelectorBase.acctCD),
    typeof (BAccountSelectorBase.acctName),
    typeof (BAccountSelectorBase.type),
    typeof (PX.Objects.AR.Customer.customerClassID),
    typeof (PX.Objects.CR.BAccount.status),
    typeof (PX.Objects.CR.Contact.phone1),
    typeof (PX.Objects.CR.Address.addressLine1),
    typeof (PX.Objects.CR.Address.addressLine2),
    typeof (PX.Objects.CR.Address.postalCode),
    typeof (PX.Objects.CR.Address.city),
    typeof (PX.Objects.CR.Address.countryID)
  };
  public static readonly System.Type[] CustomerSelectorColumns = new System.Type[11]
  {
    typeof (PX.Objects.AR.Customer.acctCD),
    typeof (PX.Objects.AR.Customer.acctName),
    typeof (PX.Objects.AR.Customer.type),
    typeof (PX.Objects.AR.Customer.customerClassID),
    typeof (PX.Objects.AR.Customer.status),
    typeof (PX.Objects.CR.Contact.phone1),
    typeof (PX.Objects.CR.Address.addressLine1),
    typeof (PX.Objects.CR.Address.addressLine2),
    typeof (PX.Objects.CR.Address.postalCode),
    typeof (PX.Objects.CR.Address.city),
    typeof (PX.Objects.CR.Address.countryID)
  };
  public static readonly System.Type[] VendorSelectorColumns = new System.Type[11]
  {
    typeof (PX.Objects.AP.Vendor.acctCD),
    typeof (PX.Objects.AP.Vendor.acctName),
    typeof (PX.Objects.AP.Vendor.type),
    typeof (PX.Objects.AP.Vendor.vendorClassID),
    typeof (PX.Objects.AP.Vendor.vStatus),
    typeof (PX.Objects.CR.Contact.phone1),
    typeof (PX.Objects.CR.Address.addressLine1),
    typeof (PX.Objects.CR.Address.addressLine2),
    typeof (PX.Objects.CR.Address.postalCode),
    typeof (PX.Objects.CR.Address.city),
    typeof (PX.Objects.CR.Address.countryID)
  };

  public FSSelectorBusinessAccount_BaseAttribute(string dimensionName, System.Type whereType)
    : base(dimensionName, BqlCommand.Compose(new System.Type[14]
    {
      typeof (Search2<,,>),
      typeof (BAccountSelectorBase.bAccountID),
      typeof (LeftJoin<,,>),
      typeof (PX.Objects.CR.Contact),
      typeof (On<PX.Objects.CR.Contact.bAccountID, Equal<BAccountSelectorBase.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<BAccountSelectorBase.defContactID>>>),
      typeof (LeftJoin<,,>),
      typeof (PX.Objects.CR.Address),
      typeof (On<PX.Objects.CR.Address.bAccountID, Equal<BAccountSelectorBase.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<BAccountSelectorBase.defAddressID>>>),
      typeof (LeftJoin<,>),
      typeof (PX.Objects.AR.Customer),
      typeof (On<PX.Objects.AR.Customer.bAccountID, Equal<BAccountSelectorBase.bAccountID>>),
      typeof (Where2<,>),
      whereType,
      typeof (And<Match<Current<AccessInfo.userName>>>)
    }), typeof (BAccountSelectorBase.acctCD), FSSelectorBusinessAccount_BaseAttribute.BAccountSelectorColumns)
  {
    this.DescriptionField = typeof (BAccountSelectorBase.acctName);
  }
}
