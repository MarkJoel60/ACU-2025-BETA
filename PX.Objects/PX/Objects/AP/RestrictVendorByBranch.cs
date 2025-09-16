// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.RestrictVendorByBranch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.AP;

public class RestrictVendorByBranch : RestrictBAccountBySource
{
  public bool ResetVendor
  {
    set => this.ResetBAccount = value;
    get => this.ResetBAccount;
  }

  public RestrictVendorByBranch(System.Type branchID = null)
    : base(BqlCommand.Compose(typeof (Where<,>), typeof (Vendor.vOrgBAccountID), typeof (RestrictByBranch<>), typeof (Current<>), branchID), branchID, typeof (Vendor.acctCD), "The usage of the {0} vendor is restricted in the {1} branch.")
  {
  }

  public RestrictVendorByBranch(System.Type source, System.Type branchID = null)
    : base(BqlCommand.Compose(typeof (Where<,>), source, typeof (RestrictByBranch<>), typeof (Current<>), branchID), branchID, source, "The usage of the {0} vendor is restricted in the {1} branch.")
  {
  }

  public RestrictVendorByBranch(System.Type source, System.Type WhereType, System.Type branchID = null)
    : base(BqlCommand.Compose(typeof (Where2<,>), typeof (Where<,>), source, typeof (RestrictByBranch<>), typeof (Current<>), branchID, WhereType), branchID, source, "The usage of the {0} vendor is restricted in the {1} branch.")
  {
  }
}
