// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.MaritalStatusesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class MaritalStatusesAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string Single = "S";
  public const string Married = "M";
  public const string Divorced = "D";
  public const string Widowed = "W";

  public MaritalStatusesAttribute()
    : base(new string[4]{ "S", "M", "D", "W" }, new string[4]
    {
      nameof (Single),
      nameof (Married),
      nameof (Divorced),
      nameof (Widowed)
    })
  {
  }

  public class single : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  MaritalStatusesAttribute.single>
  {
    public single()
      : base("S")
    {
    }
  }

  public class married : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  MaritalStatusesAttribute.married>
  {
    public married()
      : base("M")
    {
    }
  }

  public class divorced : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  MaritalStatusesAttribute.divorced>
  {
    public divorced()
      : base("D")
    {
    }
  }

  public class widowed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  MaritalStatusesAttribute.widowed>
  {
    public widowed()
      : base("W")
    {
    }
  }
}
