// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA;

public class CABankTranType
{
  public const 
  #nullable disable
  string Statement = "S";
  public const string PaymentImport = "I";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "S", "I" }, new string[2]
      {
        "Bank Statement Import",
        "Payments Import"
      })
    {
    }
  }

  public class statement : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CABankTranType.statement>
  {
    public statement()
      : base("S")
    {
    }
  }

  public class paymentImport : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CABankTranType.paymentImport>
  {
    public paymentImport()
      : base("I")
    {
    }
  }
}
