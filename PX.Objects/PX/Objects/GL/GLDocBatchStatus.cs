// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLDocBatchStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.GL;

public class GLDocBatchStatus
{
  public const 
  #nullable disable
  string Hold = "H";
  public const string Balanced = "B";
  public const string Completed = "C";
  public const string Voided = "V";
  public const string Released = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[5]{ "H", "B", "C", "V", "R" }, new string[5]
      {
        "On Hold",
        "Balanced",
        "Completed",
        "Voided",
        "Released"
      })
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  GLDocBatchStatus.hold>
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
  GLDocBatchStatus.balanced>
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
  GLDocBatchStatus.released>
  {
    public released()
      : base("R")
    {
    }
  }

  public class completed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  GLDocBatchStatus.completed>
  {
    public completed()
      : base("C")
    {
    }
  }

  public class voided : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  GLDocBatchStatus.voided>
  {
    public voided()
      : base("V")
    {
    }
  }
}
