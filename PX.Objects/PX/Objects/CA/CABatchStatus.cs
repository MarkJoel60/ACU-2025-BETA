// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABatchStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA;

public class CABatchStatus
{
  public const 
  #nullable disable
  string Balanced = "B";
  public const string Hold = "H";
  public const string Released = "R";
  public const string Exported = "P";
  public const string Canceled = "C";
  public const string Voided = "V";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[6]{ "B", "H", "R", "P", "C", "V" }, new string[6]
      {
        "Balanced",
        "On Hold",
        "Released",
        "Exported",
        "Canceled",
        "Voided"
      })
    {
    }
  }

  public class balanced : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CABatchStatus.balanced>
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
  CABatchStatus.hold>
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
  CABatchStatus.released>
  {
    public released()
      : base("R")
    {
    }
  }

  public class exported : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CABatchStatus.exported>
  {
    public exported()
      : base("P")
    {
    }
  }

  public class canceled : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CABatchStatus.canceled>
  {
    public canceled()
      : base("C")
    {
    }
  }

  public class voided : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CABatchStatus.voided>
  {
    public voided()
      : base("V")
    {
    }
  }
}
