// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPPostOptions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.EP;

[ExcludeFromCodeCoverage]
public class EPPostOptions
{
  public const 
  #nullable disable
  string Post = "P";
  public const string DoNotPost = "N";
  public const string PostToOffBalance = "O";
  public const string OverridePMInPayroll = "M";
  public const string OverridePMAndGLInPayroll = "G";
  public const string PostPMAndGLFromPayroll = "A";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("P", "Post PM and GL Transactions"),
        PXStringListAttribute.Pair("N", "Do not Post"),
        PXStringListAttribute.Pair("O", "Post PM to Off-Balance Account Group")
      })
    {
    }
  }

  public class post : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPPostOptions.post>
  {
    public post()
      : base("P")
    {
    }
  }

  public class doNotPost : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPPostOptions.doNotPost>
  {
    public doNotPost()
      : base("N")
    {
    }
  }

  public class postToOffBalance : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPPostOptions.postToOffBalance>
  {
    public postToOffBalance()
      : base("O")
    {
    }
  }

  public class overridePMInPayroll : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPPostOptions.overridePMInPayroll>
  {
    public overridePMInPayroll()
      : base("M")
    {
    }
  }
}
