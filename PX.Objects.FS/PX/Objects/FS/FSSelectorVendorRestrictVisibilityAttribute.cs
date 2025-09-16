// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorVendorRestrictVisibilityAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AP;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorVendorRestrictVisibilityAttribute : PXDimensionSelectorAttribute
{
  public FSSelectorVendorRestrictVisibilityAttribute()
    : base("VENDOR", typeof (Search2<PX.Objects.AP.Vendor.bAccountID, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AP.Vendor.defContactID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.AP.Vendor.defAddressID>>>>>, Where<FSxVendor.sDEnabled, Equal<True>, And<PX.Objects.AP.Vendor.vStatus, NotEqual<VendorStatus.inactive>, And<PX.Objects.AP.Vendor.vOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>>>), typeof (PX.Objects.AP.Vendor.acctCD), new System.Type[7]
    {
      typeof (PX.Objects.AP.Vendor.acctCD),
      typeof (PX.Objects.AP.Vendor.vStatus),
      typeof (PX.Objects.AP.Vendor.acctName),
      typeof (PX.Objects.CR.BAccount.classID),
      typeof (PX.Objects.CR.Contact.phone1),
      typeof (PX.Objects.CR.Address.city),
      typeof (PX.Objects.CR.Address.countryID)
    })
  {
    this.DescriptionField = typeof (PX.Objects.AP.Vendor.acctName);
    this.DirtyRead = true;
  }
}
