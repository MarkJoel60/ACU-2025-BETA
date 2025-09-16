// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.RecurringOption
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CT;

public static class RecurringOption
{
  public const 
  #nullable disable
  string None = "N";
  public const string Prepay = "P";
  public const string Usage = "U";
  public const string Deposits = "D";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "N", "P", "U" }, new string[3]
      {
        "None",
        "Prepaid",
        "Postpaid"
      })
    {
    }
  }

  public class ListForDepositsAttribute : PXStringListAttribute
  {
    public ListForDepositsAttribute()
      : base(new string[4]{ "N", "P", "U", "D" }, new string[4]
      {
        "None",
        "Prepaid",
        "Postpaid",
        "Deposit"
      })
    {
    }
  }

  public class none : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RecurringOption.none>
  {
    public none()
      : base("N")
    {
    }
  }

  public class prepay : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RecurringOption.prepay>
  {
    public prepay()
      : base("P")
    {
    }
  }

  public class usage : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RecurringOption.usage>
  {
    public usage()
      : base("U")
    {
    }
  }

  public class deposits : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RecurringOption.deposits>
  {
    public deposits()
      : base("D")
    {
    }
  }
}
