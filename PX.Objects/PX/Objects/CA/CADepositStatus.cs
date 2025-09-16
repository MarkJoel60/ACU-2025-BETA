// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CADepositStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA;

public class CADepositStatus
{
  public const 
  #nullable disable
  string Balanced = "B";
  public const string Hold = "H";
  public const string Released = "R";
  public const string Voided = "V";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "B", "H", "R", "V" }, new string[4]
      {
        "Balanced",
        "On Hold",
        "Released",
        "Voided"
      })
    {
    }
  }

  public class balanced : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADepositStatus.balanced>
  {
    public balanced()
      : base("B")
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADepositStatus.hold>
  {
    public hold()
      : base("H")
    {
    }
  }

  public class released : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADepositStatus.released>
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
  CADepositStatus.voided>
  {
    public voided()
      : base("V")
    {
    }
  }
}
