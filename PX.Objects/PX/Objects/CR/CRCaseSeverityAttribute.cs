// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCaseSeverityAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public sealed class CRCaseSeverityAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string _LOW = "L";
  public const string _MEDIUM = "M";
  public const string _HIGH = "H";
  public const string _URGENT = "U";

  public CRCaseSeverityAttribute()
    : base(new string[4]{ "L", "M", "H", "U" }, new string[4]
    {
      "Low",
      "Medium",
      "High",
      "Urgent"
    })
  {
  }

  public sealed class low : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRCaseSeverityAttribute.low>
  {
    public low()
      : base("L")
    {
    }
  }

  public sealed class medium : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRCaseSeverityAttribute.medium>
  {
    public medium()
      : base("M")
    {
    }
  }

  public sealed class high : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRCaseSeverityAttribute.high>
  {
    public high()
      : base("H")
    {
    }
  }

  public sealed class urgent : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRCaseSeverityAttribute.urgent>
  {
    public urgent()
      : base("U")
    {
    }
  }
}
