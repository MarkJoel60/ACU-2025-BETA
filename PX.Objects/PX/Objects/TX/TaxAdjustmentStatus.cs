// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxAdjustmentStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.TX;

public class TaxAdjustmentStatus
{
  public const 
  #nullable disable
  string Hold = "H";
  public const string Balanced = "B";
  public const string Released = "C";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "H", "B", "C" }, new string[3]
      {
        "On Hold",
        "Balanced",
        "Released"
      })
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TaxAdjustmentStatus.hold>
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
  TaxAdjustmentStatus.balanced>
  {
    public balanced()
      : base("B")
    {
    }
  }

  public class released : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TaxAdjustmentStatus.released>
  {
    public released()
      : base("C")
    {
    }
  }
}
