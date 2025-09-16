// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLotSerIssueMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INLotSerIssueMethod
{
  public const 
  #nullable disable
  string FIFO = "F";
  public const string LIFO = "L";
  public const string Sequential = "S";
  public const string Expiration = "E";
  public const string UserEnterable = "U";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[5]
      {
        PXStringListAttribute.Pair("F", "FIFO"),
        PXStringListAttribute.Pair("L", "LIFO"),
        PXStringListAttribute.Pair("S", "Sequential"),
        PXStringListAttribute.Pair("E", "Expiration"),
        PXStringListAttribute.Pair("U", "User-Enterable")
      })
    {
    }
  }

  public class fIFO : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INLotSerIssueMethod.fIFO>
  {
    public fIFO()
      : base("F")
    {
    }
  }

  public class lIFO : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INLotSerIssueMethod.lIFO>
  {
    public lIFO()
      : base("L")
    {
    }
  }

  public class sequential : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INLotSerIssueMethod.sequential>
  {
    public sequential()
      : base("S")
    {
    }
  }

  public class expiration : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INLotSerIssueMethod.expiration>
  {
    public expiration()
      : base("E")
    {
    }
  }

  public class userEnterable : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INLotSerIssueMethod.userEnterable>
  {
    public userEnterable()
      : base("U")
    {
    }
  }
}
