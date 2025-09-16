// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CATax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("CA Tax Detail")]
[Serializable]
public class CATax : TaxDetail, IBqlTable, IBqlTableSystemDataStorage, ITranTax
{
  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (CAAdj.adjTranType))]
  [PXUIField]
  public virtual 
  #nullable disable
  string AdjTranType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (CAAdj.adjRefNbr))]
  [PXUIField]
  public virtual string AdjRefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXParent(typeof (Select<CASplit, Where<CASplit.adjTranType, Equal<Current<CATax.adjTranType>>, And<CASplit.adjRefNbr, Equal<Current<CATax.adjRefNbr>>, And<CASplit.lineNbr, Equal<Current<CATax.lineNbr>>>>>>))]
  public virtual int? LineNbr { get; set; }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Tax ID")]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr))]
  public override string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (CAAdj.curyInfoID))]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (CATax.curyInfoID), typeof (CATax.origTaxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOrigTaxableAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigTaxableAmt { get; set; }

  [PXDBCurrency(typeof (CATax.curyInfoID), typeof (CATax.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxableAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxableAmt { get; set; }

  [PXDBCurrency(typeof (CATax.curyInfoID), typeof (CATax.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxAmt { get; set; }

  [PXDBBaseCury]
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
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ExemptedAmt { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public string TranType
  {
    get => this.AdjTranType;
    set => this.AdjTranType = value;
  }

  public string RefNbr
  {
    get => this.AdjRefNbr;
    set => this.AdjRefNbr = value;
  }

  public class PK : 
    PrimaryKeyOf<CATax>.By<CATax.adjTranType, CATax.adjRefNbr, CATax.lineNbr, CATax.taxID>
  {
    public static CATax Find(
      PXGraph graph,
      string adjTranType,
      string adjRefNbr,
      int? lineNbr,
      string taxID,
      PKFindOptions options = 0)
    {
      return (CATax) PrimaryKeyOf<CATax>.By<CATax.adjTranType, CATax.adjRefNbr, CATax.lineNbr, CATax.taxID>.FindBy(graph, (object) adjTranType, (object) adjRefNbr, (object) lineNbr, (object) taxID, options);
    }
  }

  public static class FK
  {
    public class Tax : PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<CATax>.By<CATax.taxID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CATax>.By<CATax.curyInfoID>
    {
    }

    public class CashTransactionDetails : 
      PrimaryKeyOf<CASplit>.By<CASplit.adjTranType, CASplit.adjRefNbr, CASplit.lineNbr>.ForeignKeyOf<CATax>.By<CATax.adjTranType, CATax.adjRefNbr, CATax.lineNbr>
    {
    }
  }

  public abstract class adjTranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATax.adjTranType>
  {
  }

  public abstract class adjRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATax.adjRefNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATax.lineNbr>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATax.taxID>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATax.taxRate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CATax.curyInfoID>
  {
  }

  public abstract class curyOrigTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CATax.curyOrigTaxableAmt>
  {
  }

  public abstract class origTaxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATax.origTaxableAmt>
  {
  }

  public abstract class curyTaxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATax.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATax.taxableAmt>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATax.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATax.taxAmt>
  {
  }

  public abstract class curyExpenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATax.curyExpenseAmt>
  {
  }

  public abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATax.expenseAmt>
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
  CATax.Tstamp>
  {
  }
}
