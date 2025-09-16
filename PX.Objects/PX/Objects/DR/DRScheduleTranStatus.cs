// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRScheduleTranStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.DR;

public static class DRScheduleTranStatus
{
  public const 
  #nullable disable
  string Open = "O";
  public const string Posted = "P";
  public const string Projected = "J";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "O", "P", "J" }, new string[3]
      {
        "Open",
        "Posted",
        "Projected"
      })
    {
    }
  }

  public class OpenStatus : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DRScheduleTranStatus.OpenStatus>
  {
    public OpenStatus()
      : base("O")
    {
    }
  }

  public class PostedStatus : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DRScheduleTranStatus.PostedStatus>
  {
    public PostedStatus()
      : base("P")
    {
    }
  }

  public class ProjectedStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DRScheduleTranStatus.ProjectedStatus>
  {
    public ProjectedStatus()
      : base("J")
    {
    }
  }
}
