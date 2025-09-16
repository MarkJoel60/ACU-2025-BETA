// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.CSTaxCalcLevel
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.TX;

public static class CSTaxCalcLevel
{
  public const 
  #nullable disable
  string Inclusive = "0";
  public const string CalcOnItemAmt = "1";
  public const string CalcOnItemAmtPlusTaxAmt = "2";
  public const string CalcOnItemQtyInclusively = "0";
  public const string CalcOnItemQtyExclusively = "1";

  public class inclusive : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CSTaxCalcLevel.inclusive>
  {
    public inclusive()
      : base("0")
    {
    }
  }

  public class calcOnItemAmt : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CSTaxCalcLevel.calcOnItemAmt>
  {
    public calcOnItemAmt()
      : base("1")
    {
    }
  }

  public class calcOnItemAmtPlusTaxAmt : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CSTaxCalcLevel.calcOnItemAmtPlusTaxAmt>
  {
    public calcOnItemAmtPlusTaxAmt()
      : base("2")
    {
    }
  }

  public class calcOnItemQtyInclusively : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CSTaxCalcLevel.calcOnItemQtyInclusively>
  {
    public calcOnItemQtyInclusively()
      : base("0")
    {
    }
  }

  public class calcOnItemQtyExclusively : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CSTaxCalcLevel.calcOnItemQtyExclusively>
  {
    public calcOnItemQtyExclusively()
      : base("1")
    {
    }
  }
}
