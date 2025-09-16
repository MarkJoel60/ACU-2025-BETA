// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.WizardTaskTypesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.WZ;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public sealed class WizardTaskTypesAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string _ARTICLE = "AR";
  public const string _SCREEN = "SC";

  public WizardTaskTypesAttribute()
    : base(new string[2]{ "AR", "SC" }, new string[2]
    {
      "Article",
      "Screen"
    })
  {
  }

  public sealed class Article : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WizardTaskTypesAttribute.Article>
  {
    public Article()
      : base("AR")
    {
    }
  }

  public sealed class Screen : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  WizardTaskTypesAttribute.Screen>
  {
    public Screen()
      : base("SC")
    {
    }
  }
}
