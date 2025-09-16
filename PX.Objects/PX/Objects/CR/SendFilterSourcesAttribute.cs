// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.SendFilterSourcesAttribute
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
public sealed class SendFilterSourcesAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string _ALL = "A";
  public const string _NEVERSENT = "N";

  public SendFilterSourcesAttribute()
    : base(new string[2]{ "A", "N" }, new string[2]
    {
      "All",
      "Never Sent"
    })
  {
  }

  public sealed class all : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SendFilterSourcesAttribute.all>
  {
    public all()
      : base("A")
    {
    }
  }

  public sealed class neverSent : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SendFilterSourcesAttribute.neverSent>
  {
    public neverSent()
      : base("N")
    {
    }
  }
}
