// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMBillingOption
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class PMBillingOption
{
  public const 
  #nullable disable
  string OnBilling = "B";
  public const string OnTaskCompletion = "T";
  public const string OnProjectCompetion = "P";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("B", "By Billing Period"),
        PXStringListAttribute.Pair("T", "On Task Completion"),
        PXStringListAttribute.Pair("P", "On Project Completion")
      })
    {
    }
  }

  public class onBilling : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMBillingOption.onBilling>
  {
    public onBilling()
      : base("B")
    {
    }
  }

  public class onTaskCompletion : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PMBillingOption.onTaskCompletion>
  {
    public onTaskCompletion()
      : base("T")
    {
    }
  }

  public class onProjectCompetion : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PMBillingOption.onProjectCompetion>
  {
    public onProjectCompetion()
      : base("P")
    {
    }
  }
}
