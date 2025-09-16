// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.RestrictVendorClassByUserBranches
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AP;

public class RestrictVendorClassByUserBranches : PXRestrictorAttribute
{
  public RestrictVendorClassByUserBranches()
    : base(BqlCommand.Compose(typeof (Where<,>), typeof (VendorClass.orgBAccountID), typeof (RestrictByUserBranches<>), typeof (Current<>), typeof (AccessInfo.userName)), "The usage of the {0} vendor class is restricted in the current organization or branch.", typeof (VendorClass.vendorClassID))
  {
  }
}
