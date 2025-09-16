// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPaymentBy
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AP;

public class APPaymentBy
{
  public const int DueDate = 0;
  public const int DiscountDate = 1;

  public class List : PXIntListAttribute
  {
    public List()
      : base(new int[2]{ 0, 1 }, new string[2]
      {
        "Due Date",
        "Discount Date"
      })
    {
    }
  }

  public class dueDate : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  APPaymentBy.dueDate>
  {
    public dueDate()
      : base(0)
    {
    }
  }

  public class discountDate : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  APPaymentBy.discountDate>
  {
    public discountDate()
      : base(1)
    {
    }
  }
}
