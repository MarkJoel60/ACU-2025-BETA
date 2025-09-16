// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPIDetLineType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INPIDetLineType
{
  public const 
  #nullable disable
  string Normal = "N";
  public const string Blank = "B";
  public const string UserEntered = "U";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("N", "Normal"),
        PXStringListAttribute.Pair("B", "Blank"),
        PXStringListAttribute.Pair("U", "UserEntered")
      })
    {
    }
  }

  public class normal : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPIDetLineType.normal>
  {
    public normal()
      : base("N")
    {
    }
  }

  public class blank : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPIDetLineType.blank>
  {
    public blank()
      : base("B")
    {
    }
  }

  public class userEntered : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPIDetLineType.userEntered>
  {
    public userEntered()
      : base("U")
    {
    }
  }
}
