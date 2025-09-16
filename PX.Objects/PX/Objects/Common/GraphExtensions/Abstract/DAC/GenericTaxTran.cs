// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.DAC.GenericTaxTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.Common.GraphExtensions.Abstract.DAC;

[PXHidden]
public class GenericTaxTran : PXMappedCacheExtension
{
  public 
  #nullable disable
  string TaxID { get; set; }

  public Decimal? TaxRate { get; set; }

  public Decimal? CuryTaxableAmt { get; set; }

  public Decimal? CuryTaxAmt { get; set; }

  public Decimal? CuryTaxAmtSumm { get; set; }

  public Decimal? CuryExpenseAmt { get; set; }

  public Decimal? NonDeductibleTaxRate { get; set; }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GenericTaxTran.taxID>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GenericTaxTran.taxRate>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GenericTaxTran.curyTaxableAmt>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GenericTaxTran.curyTaxAmt>
  {
  }

  public abstract class curyTaxAmtSumm : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GenericTaxTran.curyTaxAmtSumm>
  {
  }

  public abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GenericTaxTran.curyExpenseAmt>
  {
  }

  public abstract class nonDeductibleTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GenericTaxTran.nonDeductibleTaxRate>
  {
  }
}
