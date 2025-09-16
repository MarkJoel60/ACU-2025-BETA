// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.TranslationStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CM;

public class TranslationStatus
{
  public const 
  #nullable disable
  string Hold = "H";
  public const string Balanced = "U";
  public const string Released = "R";
  public const string Voided = "V";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "H", "U", "R", "V" }, new string[4]
      {
        "On Hold",
        "Balanced",
        "Released",
        "Voided"
      })
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TranslationStatus.hold>
  {
    public hold()
      : base("H")
    {
    }
  }

  public class balanced : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TranslationStatus.balanced>
  {
    public balanced()
      : base("U")
    {
    }
  }

  public class released : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TranslationStatus.released>
  {
    public released()
      : base("R")
    {
    }
  }

  public class voided : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TranslationStatus.voided>
  {
    public voided()
      : base("V")
    {
    }
  }
}
