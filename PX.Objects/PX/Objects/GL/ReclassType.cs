// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ReclassType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.GL;

public class ReclassType
{
  public const 
  #nullable disable
  string Common = "C";
  public const string Split = "S";

  public class List : PXStringListAttribute
  {
    public List()
      : base(new string[2]{ "C", "S" }, new string[2]
      {
        "Common",
        "Split"
      })
    {
    }
  }

  public class common : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReclassType.common>
  {
    public common()
      : base("C")
    {
    }
  }

  public class split : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReclassType.split>
  {
    public split()
      : base("S")
    {
    }
  }
}
