// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.Descriptor.PaymentTransactionState
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor.Descriptor;

public class PaymentTransactionState
{
  public const 
  #nullable disable
  string Open = "O";
  public const string Successful = "S";
  public const string Error = "E";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(PaymentTransactionState.ListAttribute.GetStates)
    {
    }

    public static (string, string)[] GetStates
    {
      get
      {
        return new (string, string)[3]
        {
          ("O", "Open"),
          ("S", "Successful"),
          ("E", "Error")
        };
      }
    }
  }

  public class open : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PaymentTransactionState.open>
  {
    public open()
      : base("O")
    {
    }
  }

  public class successful : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PaymentTransactionState.successful>
  {
    public successful()
      : base("S")
    {
    }
  }

  public class error : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PaymentTransactionState.error>
  {
    public error()
      : base("E")
    {
    }
  }
}
