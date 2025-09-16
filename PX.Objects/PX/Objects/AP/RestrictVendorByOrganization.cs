// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.RestrictVendorByOrganization
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.AP;

public class RestrictVendorByOrganization : RestrictBAccountBySource
{
  public RestrictVendorByOrganization(System.Type orgBAccountID = null)
    : base(BqlCommand.Compose(typeof (Where<,>), typeof (Vendor.vOrgBAccountID), typeof (RestrictByOrganization<>), typeof (Current<>), orgBAccountID), orgBAccountID, typeof (Vendor.acctCD), "The usage of the {0} vendor is restricted in the {1} branch.")
  {
    this.ResetBAccount = true;
  }
}
