// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CarrierBillingTypes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CS;

public static class CarrierBillingTypes
{
  public const 
  #nullable disable
  string Receiver = "R";
  public const string ThirdParty = "T";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "R", "T" }, new string[2]
      {
        "Receiver",
        "Third Party"
      })
    {
    }
  }

  public class ReceiverType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CarrierBillingTypes.ReceiverType>
  {
    public ReceiverType()
      : base("R")
    {
    }
  }

  public class ThirdPartyType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CarrierBillingTypes.ThirdPartyType>
  {
    public ThirdPartyType()
      : base("T")
    {
    }
  }
}
