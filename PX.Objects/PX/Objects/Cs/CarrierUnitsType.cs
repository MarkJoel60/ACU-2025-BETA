// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CarrierUnitsType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CS;

public static class CarrierUnitsType
{
  public const 
  #nullable disable
  string SI = "S";
  public const string US = "U";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "S", "U" }, new string[2]
      {
        "SI Units (Kilogram/Centimeter)",
        "US Units (Pound/Inch)"
      })
    {
    }
  }

  public sealed class si : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CarrierUnitsType.si>
  {
    public si()
      : base("S")
    {
    }
  }

  public sealed class us : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CarrierUnitsType.us>
  {
    public us()
      : base("U")
    {
    }
  }
}
