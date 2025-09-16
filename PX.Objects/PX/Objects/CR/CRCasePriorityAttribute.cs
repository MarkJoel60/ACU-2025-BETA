// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCasePriorityAttribute
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
public sealed class CRCasePriorityAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string _LOW = "L";
  public const string _MEDIUM = "M";
  public const string _HIGH = "H";

  public CRCasePriorityAttribute()
    : base(new string[3]{ "L", "M", "H" }, new string[3]
    {
      "Low",
      "Medium",
      "High"
    })
  {
  }

  public sealed class low : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRCasePriorityAttribute.low>
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
  CRCasePriorityAttribute.medium>
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
  CRCasePriorityAttribute.high>
  {
    public high()
      : base("H")
    {
    }
  }
}
