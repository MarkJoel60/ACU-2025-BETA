// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.SplitIcon
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.GL;

public class SplitIcon
{
  public const 
  #nullable disable
  string Parent = "~/Icons/parent_cc.svg";
  public const string Split = "~/Icons/subdirectory_arrow_right_cc.svg";

  public class parent : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SplitIcon.parent>
  {
    public parent()
      : base("~/Icons/parent_cc.svg")
    {
    }
  }

  public class split : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SplitIcon.split>
  {
    public split()
      : base("~/Icons/subdirectory_arrow_right_cc.svg")
    {
    }
  }
}
