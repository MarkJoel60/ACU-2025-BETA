// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorBusinessAccount_VEAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorBusinessAccount_VEAttribute : PXDimensionSelectorAttribute
{
  public FSSelectorBusinessAccount_VEAttribute()
    : base("VENDOR", BqlCommand.Compose(new System.Type[9]
    {
      typeof (Search2<,,>),
      typeof (PX.Objects.AP.Vendor.bAccountID),
      typeof (LeftJoin<,,>),
      typeof (PX.Objects.CR.Contact),
      typeof (On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AP.Vendor.defContactID>>>),
      typeof (LeftJoin<,>),
      typeof (PX.Objects.CR.Address),
      typeof (On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.AP.Vendor.defAddressID>>>),
      typeof (Where<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>)
    }), typeof (PX.Objects.AP.Vendor.acctCD), FSSelectorBusinessAccount_BaseAttribute.VendorSelectorColumns)
  {
    this.DescriptionField = typeof (PX.Objects.AP.Vendor.acctName);
  }
}
