// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.TranslationLineType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CM;

public class TranslationLineType
{
  public const 
  #nullable disable
  string Translation = "T";
  public const string GainLoss = "G";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "T", "G" }, new string[2]
      {
        "Translation Line",
        "Transl.Gain/Loss"
      })
    {
    }
  }

  public class translation : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TranslationLineType.translation>
  {
    public translation()
      : base("T")
    {
    }
  }

  public class gainLoss : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TranslationLineType.gainLoss>
  {
    public gainLoss()
      : base("G")
    {
    }
  }
}
