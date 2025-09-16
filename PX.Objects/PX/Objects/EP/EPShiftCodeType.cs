// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPShiftCodeType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP;

public class EPShiftCodeType
{
  public const 
  #nullable disable
  string Amount = "AMT";
  public const string Percent = "PCT";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "AMT", "PCT" }, new string[2]
      {
        "Amount",
        "Percent"
      })
    {
    }
  }

  public class amount : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPShiftCodeType.amount>
  {
    public amount()
      : base("AMT")
    {
    }
  }

  public class percent : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPShiftCodeType.percent>
  {
    public percent()
      : base("PCT")
    {
    }
  }
}
