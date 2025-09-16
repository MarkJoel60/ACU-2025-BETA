// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CADrCr
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;

#nullable disable
namespace PX.Objects.CA;

public class CADrCr : DrCr
{
  public const string CADebit = "D";
  public const string CACredit = "C";

  public static Decimal DebitAmt(string drCr, Decimal curyTranAmt)
  {
    switch (drCr)
    {
      case "C":
        return 0M;
      case "D":
        return curyTranAmt;
      default:
        return 0M;
    }
  }

  public static Decimal CreditAmt(string drCr, Decimal curyTranAmt)
  {
    switch (drCr)
    {
      case "C":
        return curyTranAmt;
      case "D":
        return 0M;
      default:
        return 0M;
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "D", "C" }, new string[2]
      {
        "Receipt",
        "Disbursement"
      })
    {
    }
  }

  public class cADebit : DrCr.debit
  {
  }

  public class cACredit : DrCr.credit
  {
  }
}
