// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.SVATConversionHist
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.TX;

[PXCacheName("SVAT Conversion History")]
[Serializable]
public class SVATConversionHist : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Module")]
  [BatchModule.List]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string Module { get; set; }

  [Branch(null, null, true, true, true)]
  public virtual int? AdjdBranchID { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Type")]
  public virtual string AdjdDocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Reference Nbr.")]
  public virtual string AdjdRefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Line Nbr.")]
  [PXDefault(0)]
  public virtual int? AdjdLineNbr { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault(typeof (SVATConversionHist.adjdDocType))]
  [PXUIField(DisplayName = "AdjgDocType")]
  public virtual string AdjgDocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (SVATConversionHist.adjdRefNbr))]
  [PXUIField(DisplayName = "AdjgRefNbr")]
  public virtual string AdjgRefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault(-1)]
  [PXUIField(DisplayName = "Adjustment Nbr.")]
  public virtual int? AdjNbr { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Date")]
  public virtual DateTime? AdjdDocDate { get; set; }

  [FinPeriodID(null, typeof (SVATConversionHist.adjdBranchID), null, null, null, null, true, false, null, typeof (SVATConversionHist.adjdTranPeriodID), null, true, true)]
  [PXDefault]
  [PXUIField(DisplayName = "Post Period")]
  public virtual string AdjdFinPeriodID { get; set; }

  [PeriodID(null, null, null, true)]
  public virtual string AdjdTranPeriodID { get; set; }

  [FinPeriodID(null, typeof (SVATConversionHist.adjdBranchID), null, null, null, null, true, false, null, typeof (SVATConversionHist.adjgTranPeriodID), null, true, true)]
  [PXUIField(DisplayName = "Application Period")]
  public virtual string AdjgFinPeriodID { get; set; }

  [PeriodID(null, null, null, true)]
  public virtual string AdjgTranPeriodID { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleGL>, And<Batch.draft, Equal<False>>>, OrderBy<Desc<Batch.batchNbr>>>))]
  [PXUIField(DisplayName = "Batch Number")]
  public virtual string AdjBatchNbr { get; set; }

  [PXDBInt]
  public virtual int? TaxRecordID { get; set; }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Tax ID")]
  public virtual string TaxID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  public virtual string TaxType { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tax Rate")]
  public virtual Decimal? TaxRate { get; set; }

  [PXDBInt]
  public virtual int? VendorID { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Doc. Nbr")]
  public virtual string TaxInvoiceNbr { get; set; }

  [PXDBDate(InputMask = "d", DisplayMask = "d")]
  [PXUIField(DisplayName = "Tax Doc. Date")]
  public virtual DateTime? TaxInvoiceDate { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("D")]
  [SVATTaxReversalMethods.List]
  [PXUIField(DisplayName = "VAT Recognition Method")]
  public virtual string ReversalMethod { get; set; }

  [PXDBLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (SVATConversionHist.curyInfoID), typeof (SVATConversionHist.taxableAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Taxable Amount")]
  public virtual Decimal? CuryTaxableAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Taxable Amount")]
  public virtual Decimal? TaxableAmt { get; set; }

  [PXDBCurrency(typeof (SVATConversionHist.curyInfoID), typeof (SVATConversionHist.taxAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tax Amount")]
  public virtual Decimal? CuryTaxAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tax Amount")]
  public virtual Decimal? TaxAmt { get; set; }

  [PXDBCurrency(typeof (SVATConversionHist.curyInfoID), typeof (SVATConversionHist.unrecognizedTaxAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unrecognized VAT")]
  public virtual Decimal? CuryUnrecognizedTaxAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unrecognized VAT")]
  public virtual Decimal? UnrecognizedTaxAmt { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Processed { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  /// <summary>
  /// Signed <see cref="P:PX.Objects.TX.SVATConversionHist.CuryTaxableAmt" />
  /// </summary>
  [PXDecimal]
  [PXDependsOnFields(new Type[] {typeof (SVATConversionHist.adjgDocType)})]
  [PXDBCalced(typeof (Mult<SVATConversionHist.curyTaxableAmt, Switch<Case<Where<BqlOperand<SVATConversionHist.adjgDocType, IBqlString>.IsEqual<ARDocType.invoice>>, decimal_1, Case<Where<BqlOperand<SVATConversionHist.adjgDocType, IBqlString>.IsIn<ARDocType.payment, ARDocType.prepayment>>, decimal1>>, decimal0>>), typeof (Decimal))]
  public virtual Decimal? CuryTaxableAmtBalance { get; set; }

  /// <summary>
  /// Signed <see cref="P:PX.Objects.TX.SVATConversionHist.TaxableAmt" />
  /// </summary>
  [PXDecimal]
  [PXDependsOnFields(new Type[] {typeof (SVATConversionHist.adjgDocType)})]
  [PXDBCalced(typeof (Mult<SVATConversionHist.taxableAmt, Switch<Case<Where<BqlOperand<SVATConversionHist.adjgDocType, IBqlString>.IsEqual<ARDocType.invoice>>, decimal_1, Case<Where<BqlOperand<SVATConversionHist.adjgDocType, IBqlString>.IsIn<ARDocType.payment, ARDocType.prepayment>>, decimal1>>, decimal0>>), typeof (Decimal))]
  public virtual Decimal? TaxableAmtBalance { get; set; }

  /// <summary>
  /// Signed <see cref="P:PX.Objects.TX.SVATConversionHist.CuryTaxAmt" />
  /// </summary>
  [PXDecimal]
  [PXDependsOnFields(new Type[] {typeof (SVATConversionHist.adjgDocType)})]
  [PXDBCalced(typeof (Mult<SVATConversionHist.curyTaxAmt, Switch<Case<Where<BqlOperand<SVATConversionHist.adjgDocType, IBqlString>.IsEqual<ARDocType.invoice>>, decimal_1, Case<Where<BqlOperand<SVATConversionHist.adjgDocType, IBqlString>.IsIn<ARDocType.payment, ARDocType.prepayment>>, decimal1>>, decimal0>>), typeof (Decimal))]
  public virtual Decimal? CuryTaxAmtBalance { get; set; }

  /// <summary>
  /// Signed <see cref="P:PX.Objects.TX.SVATConversionHist.TaxAmt" />
  /// </summary>
  [PXDecimal]
  [PXDependsOnFields(new Type[] {typeof (SVATConversionHist.adjgDocType)})]
  [PXDBCalced(typeof (Mult<SVATConversionHist.taxAmt, Switch<Case<Where<BqlOperand<SVATConversionHist.adjgDocType, IBqlString>.IsEqual<ARDocType.invoice>>, decimal_1, Case<Where<BqlOperand<SVATConversionHist.adjgDocType, IBqlString>.IsIn<ARDocType.payment, ARDocType.prepayment>>, decimal1>>, decimal0>>), typeof (Decimal))]
  public virtual Decimal? TaxAmtBalance { get; set; }

  public class PK : 
    PrimaryKeyOf<SVATConversionHist>.By<SVATConversionHist.module, SVATConversionHist.adjdDocType, SVATConversionHist.adjdRefNbr, SVATConversionHist.adjdLineNbr, SVATConversionHist.adjgDocType, SVATConversionHist.adjgRefNbr, SVATConversionHist.adjNbr, SVATConversionHist.taxID>
  {
    public static SVATConversionHist Find(
      PXGraph graph,
      string module,
      string adjdDocType,
      string adjdRefNbr,
      int? adjdLineNbr,
      string adjgDocType,
      string adjgRefNbr,
      int? adjNbr,
      int? taxID,
      PKFindOptions options = 0)
    {
      return (SVATConversionHist) PrimaryKeyOf<SVATConversionHist>.By<SVATConversionHist.module, SVATConversionHist.adjdDocType, SVATConversionHist.adjdRefNbr, SVATConversionHist.adjdLineNbr, SVATConversionHist.adjgDocType, SVATConversionHist.adjgRefNbr, SVATConversionHist.adjNbr, SVATConversionHist.taxID>.FindBy(graph, (object) module, (object) adjdDocType, (object) adjdRefNbr, (object) adjdLineNbr, (object) adjgDocType, (object) adjgRefNbr, (object) adjNbr, (object) taxID, options);
    }
  }

  public static class FK
  {
    public class AdjdBranch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<SVATConversionHist>.By<SVATConversionHist.adjdBranchID>
    {
    }

    public class Tax : 
      PrimaryKeyOf<Tax>.By<Tax.taxID>.ForeignKeyOf<SVATConversionHist>.By<SVATConversionHist.taxID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<SVATConversionHist>.By<SVATConversionHist.vendorID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<SVATConversionHist>.By<SVATConversionHist.curyInfoID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SVATConversionHist.selected>
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SVATConversionHist.module>
  {
  }

  public abstract class adjdBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SVATConversionHist.adjdBranchID>
  {
  }

  public abstract class adjdDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SVATConversionHist.adjdDocType>
  {
  }

  public abstract class adjdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SVATConversionHist.adjdRefNbr>
  {
  }

  public abstract class adjdLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SVATConversionHist.adjdLineNbr>
  {
  }

  public abstract class adjgDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SVATConversionHist.adjgDocType>
  {
  }

  public abstract class adjgRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SVATConversionHist.adjgRefNbr>
  {
  }

  public abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SVATConversionHist.adjNbr>
  {
  }

  public abstract class adjdDocDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SVATConversionHist.adjdDocDate>
  {
  }

  public abstract class adjdFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SVATConversionHist.adjdFinPeriodID>
  {
  }

  public abstract class adjdTranPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class adjgFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SVATConversionHist.adjgFinPeriodID>
  {
  }

  public abstract class adjgTranPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class adjBatchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SVATConversionHist.adjBatchNbr>
  {
  }

  public abstract class taxRecordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SVATConversionHist.taxRecordID>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SVATConversionHist.taxID>
  {
  }

  public abstract class taxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SVATConversionHist.taxType>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SVATConversionHist.taxRate>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SVATConversionHist.vendorID>
  {
  }

  public abstract class taxInvoiceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SVATConversionHist.taxInvoiceNbr>
  {
  }

  public abstract class taxInvoiceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SVATConversionHist.taxInvoiceDate>
  {
  }

  public abstract class reversalMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SVATConversionHist.reversalMethod>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SVATConversionHist.curyInfoID>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SVATConversionHist.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SVATConversionHist.taxableAmt>
  {
  }

  public abstract class curyTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SVATConversionHist.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SVATConversionHist.taxAmt>
  {
  }

  public abstract class curyUnrecognizedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SVATConversionHist.curyUnrecognizedTaxAmt>
  {
  }

  public abstract class unrecognizedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SVATConversionHist.unrecognizedTaxAmt>
  {
  }

  public abstract class processed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SVATConversionHist.processed>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SVATConversionHist.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SVATConversionHist.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SVATConversionHist.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SVATConversionHist.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SVATConversionHist.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SVATConversionHist.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SVATConversionHist.lastModifiedDateTime>
  {
  }

  public abstract class curyTaxableAmtBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SVATConversionHist.curyTaxableAmtBalance>
  {
  }

  public abstract class taxableAmtBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SVATConversionHist.taxableAmtBalance>
  {
  }

  public abstract class curyTaxAmtBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SVATConversionHist.curyTaxAmtBalance>
  {
  }

  public abstract class taxAmtBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SVATConversionHist.taxAmtBalance>
  {
  }
}
