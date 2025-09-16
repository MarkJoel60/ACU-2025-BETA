// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankChargeTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("CABankChargeTax")]
[Serializable]
public class CABankChargeTax : TaxDetail, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (CABankTran.tranID))]
  [PXParent(typeof (CABankChargeTax.FK.BankTranMatch))]
  public virtual int? BankTranID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? LineNbr { get; set; }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Tax ID")]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr))]
  public override 
  #nullable disable
  string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault(typeof (CABankTranMatch.matchType.charge))]
  [CABankTranMatch.matchType.List]
  [PXUIField]
  public virtual string MatchType { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (CABankTran.curyInfoID))]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (CABankChargeTax.curyInfoID), typeof (CABankChargeTax.origTaxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOrigTaxableAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigTaxableAmt { get; set; }

  [PXDBCurrency(typeof (CABankChargeTax.curyInfoID), typeof (CABankChargeTax.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxableAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxableAmt { get; set; }

  /// <summary>The exempted amount in the record currency.</summary>
  [PXDBCurrency(typeof (CABankChargeTax.curyInfoID), typeof (CABankChargeTax.exemptedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryExemptedAmt { get; set; }

  /// <summary>The exempted amount in the base currency.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ExemptedAmt { get; set; }

  [PXDBCurrency(typeof (CABankChargeTax.curyInfoID), typeof (CABankChargeTax.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxAmt { get; set; }

  [PXDBCurrency(typeof (CABankChargeTax.curyInfoID), typeof (CABankChargeTax.expenseAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryExpenseAmt { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<CABankChargeTax>.By<CABankChargeTax.bankTranID, CABankChargeTax.lineNbr, CABankChargeTax.taxID, CABankChargeTax.matchType>
  {
    public static CABankChargeTax Find(
      PXGraph graph,
      int? tranID,
      int? lineNbr,
      string taxID,
      string tranType,
      PKFindOptions options = 0)
    {
      return (CABankChargeTax) PrimaryKeyOf<CABankChargeTax>.By<CABankChargeTax.bankTranID, CABankChargeTax.lineNbr, CABankChargeTax.taxID, CABankChargeTax.matchType>.FindBy(graph, (object) tranID, (object) lineNbr, (object) taxID, (object) tranType, options);
    }
  }

  public static class FK
  {
    public class BankTranMatch : 
      PrimaryKeyOf<CABankTranMatch>.By<CABankTranMatch.tranID, CABankTranMatch.lineNbr, CABankTranMatch.matchType>.ForeignKeyOf<CABankChargeTax>.By<CABankChargeTax.bankTranID, CABankChargeTax.lineNbr, CABankChargeTax.matchType>
    {
    }
  }

  public abstract class bankTranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankChargeTax.bankTranID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankChargeTax.lineNbr>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankChargeTax.taxID>
  {
  }

  public abstract class matchType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankChargeTax.matchType>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankChargeTax.taxRate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CABankChargeTax.curyInfoID>
  {
  }

  public abstract class curyOrigTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankChargeTax.curyOrigTaxableAmt>
  {
  }

  public abstract class origTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankChargeTax.origTaxableAmt>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankChargeTax.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankChargeTax.taxableAmt>
  {
  }

  public abstract class curyExemptedAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class exemptedAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankChargeTax.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankChargeTax.taxAmt>
  {
  }

  public abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankChargeTax.curyExpenseAmt>
  {
  }

  public abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankChargeTax.expenseAmt>
  {
  }

  public abstract class nonDeductibleTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankChargeTax.nonDeductibleTaxRate>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CABankChargeTax.Tstamp>
  {
  }
}
