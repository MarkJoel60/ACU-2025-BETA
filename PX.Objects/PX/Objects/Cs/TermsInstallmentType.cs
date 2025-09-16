// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.TermsInstallmentType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CS;

public class TermsInstallmentType
{
  public const 
  #nullable disable
  string Single = "S";
  public const string Multiple = "M";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "S", "M" }, new string[2]
      {
        "Single",
        "Multiple"
      })
    {
    }
  }

  public class single : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TermsInstallmentType.single>
  {
    public single()
      : base("S")
    {
    }
  }

  public class multiple : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TermsInstallmentType.multiple>
  {
    public multiple()
      : base("M")
    {
    }
  }
}
