// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.RestrictCustomerClassByUserBranches
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.AR;

public class RestrictCustomerClassByUserBranches : PXRestrictorAttribute
{
  public RestrictCustomerClassByUserBranches()
    : base(BqlCommand.Compose(new Type[5]
    {
      typeof (Where<,>),
      typeof (CustomerClass.orgBAccountID),
      typeof (RestrictByUserBranches<>),
      typeof (Current<>),
      typeof (AccessInfo.userName)
    }), "The usage of the {0} customer class is restricted in the current organization or branch.", new Type[1]
    {
      typeof (CustomerClass.customerClassID)
    })
  {
  }
}
