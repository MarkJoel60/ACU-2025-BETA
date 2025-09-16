// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.RestrictCustomerByBranch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.AR;

public class RestrictCustomerByBranch : RestrictBAccountBySource
{
  public bool ResetCustomer
  {
    set => this.ResetBAccount = value;
    get => this.ResetBAccount;
  }

  public RestrictCustomerByBranch(System.Type branchID = null)
    : base(BqlCommand.Compose(new System.Type[5]
    {
      typeof (Where<,>),
      typeof (Customer.cOrgBAccountID),
      typeof (RestrictByBranch<>),
      typeof (Current<>),
      branchID
    }), branchID, typeof (Customer.acctCD), "The usage of the {0} customer is restricted in the {1} branch.")
  {
  }

  public RestrictCustomerByBranch(System.Type source, System.Type branchID = null)
    : base(BqlCommand.Compose(new System.Type[5]
    {
      typeof (Where<,>),
      source,
      typeof (RestrictByBranch<>),
      typeof (Current<>),
      branchID
    }), branchID, source, "The usage of the {0} customer is restricted in the {1} branch.")
  {
  }

  public RestrictCustomerByBranch(System.Type source, System.Type WhereType, System.Type branchID = null)
    : base(BqlCommand.Compose(new System.Type[7]
    {
      typeof (Where2<,>),
      typeof (Where<,>),
      source,
      typeof (RestrictByBranch<>),
      typeof (Current<>),
      branchID,
      WhereType
    }), branchID, source, "The usage of the {0} customer is restricted in the {1} branch.")
  {
  }
}
