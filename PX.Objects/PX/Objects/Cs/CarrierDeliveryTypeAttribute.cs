// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CarrierDeliveryTypeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CS;

public static class CarrierDeliveryTypeAttribute
{
  public const 
  #nullable disable
  string NA = "N";
  public const string Parcel = "P";
  public const string LTL = "L";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "N", "P", "L" }, new string[3]
      {
        "N/A",
        "Parcel",
        "LTL/FTL"
      })
    {
    }
  }

  public sealed class na : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CarrierDeliveryTypeAttribute.na>
  {
    public na()
      : base("N")
    {
    }
  }

  public sealed class parcel : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CarrierDeliveryTypeAttribute.parcel>
  {
    public parcel()
      : base("P")
    {
    }
  }

  public sealed class ltl : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CarrierDeliveryTypeAttribute.ltl>
  {
    public ltl()
      : base("L")
    {
    }
  }
}
