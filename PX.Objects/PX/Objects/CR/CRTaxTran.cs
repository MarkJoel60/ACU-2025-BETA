// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRTaxTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXBreakInheritance]
[Serializable]
public class CRTaxTran : TaxDetail, IBqlTable, IBqlTableSystemDataStorage
{
  protected Decimal? _TaxableAmt;

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID { get; set; }

  [PXDBGuid(false, IsKey = true)]
  [PXDBDefault(typeof (CROpportunity.quoteNoteID))]
  public virtual Guid? QuoteID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault(2147483647 /*0x7FFFFFFF*/)]
  [PXUIField]
  [PXParent(typeof (Select<CROpportunity, Where<CROpportunity.quoteNoteID, Equal<Current<CRTaxTran.quoteID>>>>))]
  public virtual int? LineNbr { get; set; }

  [PX.Objects.TX.TaxID]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Tax.taxID), DescriptionField = typeof (Tax.descr), DirtyRead = true)]
  public override 
  #nullable disable
  string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBString(9, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Jurisdiction Type")]
  public virtual string JurisType { get; set; }

  [PXDBString(200, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Jurisdiction Name")]
  public virtual string JurisName { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (CROpportunity.curyInfoID))]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (CRTaxTran.curyInfoID), typeof (CRTaxTran.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxableAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxableAmt
  {
    get => this._TaxableAmt;
    set => this._TaxableAmt = value;
  }

  /// <summary>The exempted amount in the record currency.</summary>
  [PXDBCurrency(typeof (CRTaxTran.curyInfoID), typeof (CRTaxTran.exemptedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public Decimal? CuryExemptedAmt { get; set; }

  /// <summary>The exempted amount in the base currency.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public Decimal? ExemptedAmt { get; set; }

  [PXDBCurrency(typeof (CRTaxTran.curyInfoID), typeof (CRTaxTran.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxAmt { get; set; }

  [PXString(10, IsUnicode = true)]
  public virtual string TaxZoneID { get; set; }

  [PXDBTimestamp(RecordComesFirst = true)]
  public virtual byte[] tstamp { get; set; }

  /// <summary>
  /// A Boolean value that specifies that the tax transaction is tax inclusive or not
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Inclusive")]
  public virtual bool? IsTaxInclusive { get; set; }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRTaxTran.recordID>
  {
  }

  public abstract class quoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRTaxTran.quoteID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRTaxTran.lineNbr>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRTaxTran.taxID>
  {
  }

  public abstract class jurisType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRTaxTran.jurisType>
  {
  }

  public abstract class jurisName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRTaxTran.jurisName>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CRTaxTran.curyInfoID>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRTaxTran.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRTaxTran.taxableAmt>
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
  CRTaxTran.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRTaxTran.taxAmt>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRTaxTran.taxZoneID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRTaxTran.Tstamp>
  {
  }

  public abstract class isTaxInclusive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRTaxTran.isTaxInclusive>
  {
  }
}
