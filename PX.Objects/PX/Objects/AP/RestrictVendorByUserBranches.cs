// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.RestrictVendorByUserBranches
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP;

public class RestrictVendorByUserBranches : PXRestrictorAttribute
{
  protected System.Type AcctCD;

  public RestrictVendorByUserBranches()
    : base(BqlCommand.Compose(typeof (Where<,>), typeof (Vendor.vOrgBAccountID), typeof (RestrictByUserBranches<>), typeof (Current<>), typeof (AccessInfo.userName)), "'{0}' cannot be found in the system. Please verify whether you have proper access rights to this object.")
  {
    this.AcctCD = typeof (Vendor.vOrgBAccountID);
  }

  public RestrictVendorByUserBranches(System.Type source)
    : base(BqlCommand.Compose(typeof (Where<,>), source, typeof (RestrictByUserBranches<>), typeof (Current<>), typeof (AccessInfo.userName)), "'{0}' cannot be found in the system. Please verify whether you have proper access rights to this object.")
  {
    this.AcctCD = source;
  }

  public override object[] GetMessageParameters(PXCache sender, object itemres, object row)
  {
    return new List<object>()
    {
      sender.Graph.Caches[BqlCommand.GetItemType(this.AcctCD)].GetStateExt((object) PXResult.Unwrap(itemres, BqlCommand.GetItemType(this.AcctCD)), "acctCD")
    }.ToArray();
  }
}
