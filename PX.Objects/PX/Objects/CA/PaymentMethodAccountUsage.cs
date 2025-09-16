// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PaymentMethodAccountUsage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA;

public class PaymentMethodAccountUsage
{
  public const 
  #nullable disable
  string UseForAP = "P";
  public const string UseForAR = "R";

  public class useForAP : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PaymentMethodAccountUsage.useForAP>
  {
    public useForAP()
      : base("P")
    {
    }
  }

  public class useForAR : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PaymentMethodAccountUsage.useForAR>
  {
    public useForAR()
      : base("R")
    {
    }
  }
}
