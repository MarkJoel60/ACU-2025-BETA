// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAExpenseTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("CAExpenseTax")]
[Serializable]
public class CAExpenseTax : TaxDetail, IBqlTable, IBqlTableSystemDataStorage, ITranTax
{
  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault(typeof (CATranType.cATransferExp))]
  [PXUIField]
  public virtual 
  #nullable disable
  string TranType { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXDBDefault(typeof (CAExpense.lineNbr))]
  [PXParent(typeof (Select<CAExpense, Where<CAExpense.refNbr, Equal<Current<CAExpenseTax.refNbr>>, And<CAExpense.lineNbr, Equal<Current<CAExpenseTax.lineNbr>>>>>))]
  public virtual int? LineNbr { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (CAExpense.refNbr))]
  [PXParent(typeof (Select<CAExpense, Where<CAExpense.refNbr, Equal<Current<CAExpenseTax.refNbr>>, And<CAExpense.lineNbr, Equal<Current<CAExpenseTax.lineNbr>>>>>))]
  [PXUIField]
  public virtual string RefNbr { get; set; }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Tax ID")]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr))]
  public override string TaxID { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (CAExpense.curyInfoID))]
  public override long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (CATax.curyInfoID), typeof (CATax.origTaxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOrigTaxableAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigTaxableAmt { get; set; }

  [PXDBCurrency(typeof (CATax.curyInfoID), typeof (CATax.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxableAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxableAmt { get; set; }

  [PXDBCurrency(typeof (CATax.curyInfoID), typeof (CATax.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxAmt { get; set; }

  [PXDBCurrency(typeof (CATax.curyInfoID), typeof (CATax.expenseAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryExpenseAmt { get; set; }

  /// <summary>The exempted amount in the record currency.</summary>
  [PXDBCurrency(typeof (CATax.curyInfoID), typeof (CATax.exemptedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryExemptedAmt { get; set; }

  /// <summary>The exempted amount in the base currency.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ExemptedAmt { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpenseTax.tranType>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAExpenseTax.lineNbr>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpenseTax.refNbr>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpenseTax.taxID>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAExpenseTax.taxRate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CAExpenseTax.curyInfoID>
  {
  }

  public abstract class curyOrigTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAExpenseTax.curyOrigTaxableAmt>
  {
  }

  public abstract class origTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAExpenseTax.origTaxableAmt>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAExpenseTax.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAExpenseTax.taxableAmt>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAExpenseTax.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAExpenseTax.taxAmt>
  {
  }

  public abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAExpenseTax.curyExpenseAmt>
  {
  }

  public abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAExpenseTax.expenseAmt>
  {
  }

  public abstract class curyExemptedAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class exemptedAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CAExpenseTax.Tstamp>
  {
  }
}
