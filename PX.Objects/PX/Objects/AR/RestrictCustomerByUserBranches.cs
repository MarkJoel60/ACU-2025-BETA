// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.RestrictCustomerByUserBranches
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

public class RestrictCustomerByUserBranches : PXRestrictorAttribute
{
  protected Type AcctCD;

  public RestrictCustomerByUserBranches()
    : base(BqlCommand.Compose(new Type[5]
    {
      typeof (Where<,>),
      typeof (Customer.cOrgBAccountID),
      typeof (RestrictByUserBranches<>),
      typeof (Current<>),
      typeof (AccessInfo.userName)
    }), "'{0}' cannot be found in the system. Please verify whether you have proper access rights to this object.", Array.Empty<Type>())
  {
    this.AcctCD = typeof (Customer.cOrgBAccountID);
  }

  public RestrictCustomerByUserBranches(Type source)
    : base(BqlCommand.Compose(new Type[5]
    {
      typeof (Where<,>),
      source,
      typeof (RestrictByUserBranches<>),
      typeof (Current<>),
      typeof (AccessInfo.userName)
    }), "'{0}' cannot be found in the system. Please verify whether you have proper access rights to this object.", Array.Empty<Type>())
  {
    this.AcctCD = source;
  }

  public RestrictCustomerByUserBranches(Type source, Type WhereType)
    : base(BqlCommand.Compose(new Type[7]
    {
      typeof (Where2<,>),
      typeof (Where<,>),
      source,
      typeof (RestrictByUserBranches<>),
      typeof (Current<>),
      typeof (AccessInfo.userName),
      WhereType
    }), "The usage of the {0} customer is restricted in the {1} branch.", Array.Empty<Type>())
  {
    this.AcctCD = source;
  }

  public virtual object[] GetMessageParameters(PXCache sender, object itemres, object row)
  {
    return new List<object>()
    {
      sender.Graph.Caches[BqlCommand.GetItemType(this.AcctCD)].GetStateExt((object) PXResult.Unwrap(itemres, BqlCommand.GetItemType(this.AcctCD)), "acctCD")
    }.ToArray();
  }
}
