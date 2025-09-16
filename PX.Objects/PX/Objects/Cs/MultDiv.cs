// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.MultDiv
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CS;

public class MultDiv
{
  public const 
  #nullable disable
  string Multiply = "M";
  public const string Divide = "D";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "M", "D" }, new string[2]
      {
        "Multiply",
        "Divide"
      })
    {
    }
  }

  public class multiply : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  MultDiv.multiply>
  {
    public multiply()
      : base("M")
    {
    }
  }

  public class divide : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  MultDiv.divide>
  {
    public divide()
      : base("D")
    {
    }
  }
}
