// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.SPCommnCalcTypes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AR;

public class SPCommnCalcTypes
{
  public const 
  #nullable disable
  string ByInvoice = "I";
  public const string ByPayment = "P";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "I", "P" }, new string[2]
      {
        "Invoice",
        "Payment"
      })
    {
    }
  }

  public class byInvoice : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SPCommnCalcTypes.byInvoice>
  {
    public byInvoice()
      : base("I")
    {
    }
  }

  public class byPayment : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SPCommnCalcTypes.byPayment>
  {
    public byPayment()
      : base("P")
    {
    }
  }
}
