// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CADepositDetailType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA;

public class CADepositDetailType
{
  public const 
  #nullable disable
  string CheckDeposit = "CHD";
  public const string VoidCheckDeposit = "VCD";
  public const string CashDeposit = "CSD";
  public const string VoidCashDeposit = "VSD";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "CHD", "VCD", "CSD", "VSD" }, new string[4]
      {
        "Check Deposit",
        "Void Check Deposit",
        "Cash Deposit",
        "Void Cash Deposit"
      })
    {
    }
  }

  public class checkDeposit : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADepositDetailType.checkDeposit>
  {
    public checkDeposit()
      : base("CHD")
    {
    }
  }

  public class voidCheckDeposit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CADepositDetailType.voidCheckDeposit>
  {
    public voidCheckDeposit()
      : base("VCD")
    {
    }
  }

  public class cashDeposit : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADepositDetailType.cashDeposit>
  {
    public cashDeposit()
      : base("CSD")
    {
    }
  }

  public class voidCashDeposit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CADepositDetailType.voidCashDeposit>
  {
    public voidCashDeposit()
      : base("VSD")
    {
    }
  }
}
