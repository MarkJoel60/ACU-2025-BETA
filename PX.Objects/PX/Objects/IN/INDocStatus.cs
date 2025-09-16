// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INDocStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN;

public class INDocStatus
{
  public const 
  #nullable disable
  string Hold = "H";
  public const string Balanced = "B";
  public const string Released = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public static readonly (string, string)[] ValuesToLabels = new (string, string)[3]
    {
      ("H", "On Hold"),
      ("B", "Balanced"),
      ("R", "Released")
    };

    public ListAttribute()
      : base(INDocStatus.ListAttribute.ValuesToLabels)
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INDocStatus.hold>
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
  INDocStatus.balanced>
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
  INDocStatus.released>
  {
    public released()
      : base("R")
    {
    }
  }
}
