// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.CSTaxCalcType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.TX;

public static class CSTaxCalcType
{
  public const 
  #nullable disable
  string Item = "I";
  public const string Doc = "D";

  public class item : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CSTaxCalcType.item>
  {
    public item()
      : base("I")
    {
    }
  }

  public class doc : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CSTaxCalcType.doc>
  {
    public doc()
      : base("D")
    {
    }
  }
}
