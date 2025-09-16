// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPTaxTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.EP;

/// <summary>
/// Represents a tax detail of an Expense Receipt document.
/// The entities of this type are edited on the Expense Receipt
/// (EP301020) form, which correspond to
/// the <see cref="T:PX.Objects.EP.ExpenseClaimDetailEntry" /> graph.
/// </summary>
/// <remarks>
/// Tax details are aggregates combined by <see cref="T:PX.Objects.TX.TaxBaseAttribute" />
/// descendants from the line-level <see cref="T:PX.Objects.EP.EPTax" /> records.
/// </remarks>
[Serializable]
public class EPTaxTran : TaxDetail, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (EPExpenseClaimDetails.claimDetailID))]
  [PXParent(typeof (Select<EPExpenseClaimDetails, Where<EPExpenseClaimDetails.claimDetailID, Equal<Current<EPTaxTran.claimDetailID>>>>))]
  public virtual int? ClaimDetailID { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXDBDefault(typeof (Search<EPExpenseClaimDetails.refNbr, Where<EPExpenseClaimDetails.claimDetailID, Equal<Current<EPTaxTran.claimDetailID>>>>))]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  [PX.Objects.TX.TaxID]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Tax.taxID), DescriptionField = typeof (Tax.descr))]
  public override string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBBool(IsKey = true)]
  [PXDefault(false)]
  public virtual bool? IsTipTax { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (EPExpenseClaimDetails.curyInfoID))]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  /// <summary>
  /// The tax rate of the relevant <see cref="T:PX.Objects.TX.Tax" /> record.
  /// </summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? TaxRate
  {
    get => this._TaxRate;
    set => this._TaxRate = value;
  }

  [PXDBCurrency(typeof (EPTaxTran.curyInfoID), typeof (EPTaxTran.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXUnboundFormula(typeof (Switch<Case<Where<WhereExempt<EPTaxTran.taxID>>, EPTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<EPExpenseClaimDetails.curyVatExemptTotal>), CancelParentUpdate = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<WhereTaxable<EPTaxTran.taxID>>, EPTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<EPExpenseClaimDetails.curyVatTaxableTotal>), CancelParentUpdate = true)]
  public Decimal? CuryTaxableAmt { get; set; }

  [PXDBBaseCury(null, null)]
  public Decimal? TaxableAmt { get; set; }

  [PXDBCurrency(typeof (EPTaxTran.curyInfoID), typeof (EPTaxTran.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public Decimal? CuryTaxAmt { get; set; }

  [PXDBBaseCury(null, null)]
  public Decimal? TaxAmt { get; set; }

  [PXDBCurrency(typeof (EPTaxTran.curyInfoID), typeof (EPTaxTran.expenseAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryExpenseAmt { get; set; }

  [PXDBBaseCury(null, null)]
  public override Decimal? ExpenseAmt { get; set; }

  [PXDBCurrency(typeof (EPTaxTran.curyInfoID), typeof (EPTaxTran.taxableAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public Decimal? ClaimCuryTaxableAmt { get; set; }

  [PXDBCurrency(typeof (EPTaxTran.curyInfoID), typeof (EPTaxTran.taxAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public Decimal? ClaimCuryTaxAmt { get; set; }

  [PXDBCurrency(typeof (EPTaxTran.curyInfoID), typeof (EPTaxTran.expenseAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public Decimal? ClaimCuryExpenseAmt { get; set; }

  /// <summary>
  /// A Boolean value that specifies that the tax transaction is tax inclusive or not
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Inclusive")]
  public virtual bool? IsTaxInclusive { get; set; }

  public abstract class claimDetailID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTaxTran.claimDetailID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPTaxTran.refNbr>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPTaxTran.taxID>
  {
  }

  public abstract class isTipTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPTaxTran.isTipTax>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  EPTaxTran.curyInfoID>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPTaxTran.taxRate>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPTaxTran.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPTaxTran.taxableAmt>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPTaxTran.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPTaxTran.taxAmt>
  {
  }

  public abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPTaxTran.curyExpenseAmt>
  {
  }

  public abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPTaxTran.expenseAmt>
  {
  }

  public abstract class claimCuryTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPTaxTran.claimCuryTaxableAmt>
  {
  }

  public abstract class claimCuryTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPTaxTran.claimCuryTaxAmt>
  {
  }

  public abstract class claimCuryExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPTaxTran.claimCuryExpenseAmt>
  {
  }

  public abstract class nonDeductibleTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPTaxTran.nonDeductibleTaxRate>
  {
  }

  public abstract class isTaxInclusive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPTaxTran.isTaxInclusive>
  {
  }
}
