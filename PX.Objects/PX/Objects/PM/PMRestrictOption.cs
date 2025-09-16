// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMRestrictOption
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class PMRestrictOption
{
  public const 
  #nullable disable
  string AllProjects = "A";
  public const string CustomerProjects = "C";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "A", "C" }, new string[2]
      {
        "All Projects",
        "Customer Projects"
      })
    {
    }
  }

  public class allProjects : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMRestrictOption.allProjects>
  {
    public allProjects()
      : base("A")
    {
    }
  }

  public class customerProjects : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PMRestrictOption.customerProjects>
  {
    public customerProjects()
      : base("C")
    {
    }
  }
}
