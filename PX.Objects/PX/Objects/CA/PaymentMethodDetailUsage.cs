// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PaymentMethodDetailUsage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA;

public class PaymentMethodDetailUsage
{
  public const 
  #nullable disable
  string UseForVendor = "V";
  public const string UseForCashAccount = "C";
  public const string UseForAll = "A";
  public const string UseForARCards = "R";
  public const string UseForAPCards = "P";

  public class useForVendor : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PaymentMethodDetailUsage.useForVendor>
  {
    public useForVendor()
      : base("V")
    {
    }
  }

  public class useForCashAccount : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PaymentMethodDetailUsage.useForCashAccount>
  {
    public useForCashAccount()
      : base("C")
    {
    }
  }

  public class useForAll : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PaymentMethodDetailUsage.useForAll>
  {
    public useForAll()
      : base("A")
    {
    }
  }

  public class useForARCards : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PaymentMethodDetailUsage.useForARCards>
  {
    public useForARCards()
      : base("R")
    {
    }
  }

  public class useForAPCards : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PaymentMethodDetailUsage.useForAPCards>
  {
    public useForAPCards()
      : base("P")
    {
    }
  }
}
