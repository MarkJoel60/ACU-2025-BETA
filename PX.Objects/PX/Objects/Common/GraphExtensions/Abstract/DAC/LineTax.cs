// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.DAC.LineTax
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
public class LineTax : PXMappedCacheExtension
{
  public int? LineNbr { get; set; }

  public 
  #nullable disable
  string TaxID { get; set; }

  public Decimal? TaxRate { get; set; }

  public Decimal? CuryTaxableAmt { get; set; }

  public virtual Decimal? CuryTaxAmt { get; set; }

  public Decimal? CuryExpenseAmt { get; set; }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LineTax.lineNbr>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LineTax.taxID>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  LineTax.taxRate>
  {
  }

  public abstract class curyTaxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  LineTax.curyTaxableAmt>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  LineTax.curyTaxAmt>
  {
  }

  public abstract class curyExpenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  LineTax.curyExpenseAmt>
  {
  }
}
