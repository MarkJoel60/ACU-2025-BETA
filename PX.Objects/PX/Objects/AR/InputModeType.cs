// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.InputModeType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AR;

public class InputModeType
{
  public const 
  #nullable disable
  string Token = "T";
  public const string Details = "D";

  public class token : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  InputModeType.token>
  {
    public token()
      : base("T")
    {
    }
  }

  public class details : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  InputModeType.details>
  {
    public details()
      : base("D")
    {
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "T", "D" }, new string[2]
      {
        "Profile ID",
        "Card Details"
      })
    {
    }
  }
}
