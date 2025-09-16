// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.CuryMultDivType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CM.Extensions;

public class CuryMultDivType
{
  public const 
  #nullable disable
  string Mult = "M";
  public const string Div = "D";

  public class mult : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CuryMultDivType.mult>
  {
    public mult()
      : base("M")
    {
    }
  }

  public class div : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CuryMultDivType.div>
  {
    public div()
      : base("D")
    {
    }
  }
}
