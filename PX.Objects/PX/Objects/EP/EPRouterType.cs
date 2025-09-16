// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPRouterType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP;

public static class EPRouterType
{
  public const 
  #nullable disable
  string Workgroup = "W";
  public const string Router = "R";
  public const string Group = "G";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "W", "G", "R" }, new string[3]
      {
        "Assign",
        "Group",
        "Jump"
      })
    {
    }
  }

  public class router : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPRouterType.router>
  {
    public router()
      : base("R")
    {
    }
  }

  public class group : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPRouterType.group>
  {
    public group()
      : base("G")
    {
    }
  }

  public class workgroup : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPRouterType.workgroup>
  {
    public workgroup()
      : base("G")
    {
    }
  }
}
