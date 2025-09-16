// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPIHdrStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INPIHdrStatus
{
  public const 
  #nullable disable
  string Counting = "N";
  public const string Entering = "E";
  public const string InReview = "R";
  public const string Completed = "C";
  public const string Cancelled = "X";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[5]
      {
        PXStringListAttribute.Pair("N", "Counting In Progress"),
        PXStringListAttribute.Pair("E", "Data Entering"),
        PXStringListAttribute.Pair("R", "In Review"),
        PXStringListAttribute.Pair("C", "Completed"),
        PXStringListAttribute.Pair("X", "Canceled")
      })
    {
    }
  }

  public class counting : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPIHdrStatus.counting>
  {
    public counting()
      : base("N")
    {
    }
  }

  public class entering : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPIHdrStatus.entering>
  {
    public entering()
      : base("E")
    {
    }
  }

  public class onReview : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPIHdrStatus.onReview>
  {
    public onReview()
      : base("R")
    {
    }
  }

  public class completed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPIHdrStatus.completed>
  {
    public completed()
      : base("C")
    {
    }
  }

  public class cancelled : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPIHdrStatus.cancelled>
  {
    public cancelled()
      : base("X")
    {
    }
  }
}
