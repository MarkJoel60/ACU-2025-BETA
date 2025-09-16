// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPTaxAggregate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.EP;

/// <summary>
/// Represents a tax detail of an Expense Claim document.
/// The entities of this type are edited on the Expense Claim
/// (EP301000) form, which correspond to
/// the <see cref="T:PX.Objects.EP.ExpenseClaimEntry" /> graph.
/// </summary>
[Serializable]
public class EPTaxAggregate : TaxDetail, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (EPExpenseClaim.refNbr))]
  [PXUIField]
  [PXParent(typeof (Select<EPExpenseClaim, Where<EPExpenseClaim.refNbr, Equal<Current<EPTaxAggregate.refNbr>>>>))]
  public 
  #nullable disable
  string RefNbr { get; set; }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Tax.taxID), DescriptionField = typeof (Tax.descr))]
  public override string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXLong]
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

  [PXDBCurrency(typeof (EPTaxAggregate.curyInfoID), typeof (EPTaxAggregate.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public Decimal? CuryTaxableAmt { get; set; }

  [PXDBBaseCury(null, null)]
  public Decimal? TaxableAmt { get; set; }

  [PXDBCurrency(typeof (EPTaxAggregate.curyInfoID), typeof (EPTaxAggregate.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public Decimal? CuryTaxAmt { get; set; }

  [PXDBBaseCury(null, null)]
  public Decimal? TaxAmt { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "100.0")]
  [PXUIField]
  public override Decimal? NonDeductibleTaxRate { get; set; }

  [PXDBCurrency(typeof (EPTaxAggregate.curyInfoID), typeof (EPTaxAggregate.expenseAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryExpenseAmt { get; set; }

  [PXBaseCury]
  public override Decimal? ExpenseAmt { get; set; }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPTaxAggregate.refNbr>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPTaxAggregate.taxID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  EPTaxAggregate.curyInfoID>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPTaxAggregate.taxRate>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPTaxAggregate.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPTaxAggregate.taxableAmt>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPTaxAggregate.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPTaxAggregate.taxAmt>
  {
  }

  public abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPTaxAggregate.curyExpenseAmt>
  {
  }

  public abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPTaxAggregate.expenseAmt>
  {
  }
}
