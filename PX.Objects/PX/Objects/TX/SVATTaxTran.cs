// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.SVATTaxTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.TX;

[PXHidden]
[Serializable]
public class SVATTaxTran : TaxTran
{
  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault]
  public override 
  #nullable disable
  string Module { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  public override string TranType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public override string RefNbr { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  public override string TaxPeriodID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, typeof (TaxTran.branchID), null, null, null, null, true, false, null, null, null, true, true)]
  [PXDefault]
  public override string FinPeriodID { get; set; }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public override string TaxID { get; set; }

  [PXDBInt]
  public override int? VendorID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  public override string TaxZoneID { get; set; }

  [PXDBInt]
  [PXDefault]
  public override int? AccountID { get; set; }

  [PXDBInt]
  [PXDefault]
  public override int? SubID { get; set; }

  [PXDBDate]
  [PXDefault]
  public override DateTime? TranDate { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  public override string TaxType { get; set; }

  [PXDBInt]
  [PXDefault]
  public override int? TaxBucketID { get; set; }

  [PXDBLong]
  [PXDefault]
  public override long? CuryInfoID { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? CuryTaxableAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? CuryTaxAmt { get; set; }

  [PXDBCurrency(typeof (SVATTaxTran.curyInfoID), typeof (SVATTaxTran.taxAmtSumm))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? CuryTaxAmtSumm { get; set; }

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SVATTaxTran.selected>
  {
  }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SVATTaxTran.module>
  {
  }

  public new abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SVATTaxTran.tranType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SVATTaxTran.refNbr>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SVATTaxTran.released>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SVATTaxTran.voided>
  {
  }

  public new abstract class taxPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SVATTaxTran.taxPeriodID>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SVATTaxTran.finPeriodID>
  {
  }

  public new abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SVATTaxTran.taxID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SVATTaxTran.vendorID>
  {
  }

  public new abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SVATTaxTran.taxZoneID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SVATTaxTran.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SVATTaxTran.subID>
  {
  }

  public new abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SVATTaxTran.tranDate>
  {
  }

  public new abstract class taxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SVATTaxTran.taxType>
  {
  }

  public new abstract class taxBucketID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SVATTaxTran.taxBucketID>
  {
  }

  public new abstract class taxInvoiceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SVATTaxTran.taxInvoiceNbr>
  {
  }

  public new abstract class taxInvoiceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SVATTaxTran.taxInvoiceDate>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SVATTaxTran.curyInfoID>
  {
  }

  public new abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SVATTaxTran.curyTaxableAmt>
  {
  }

  public new abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SVATTaxTran.taxableAmt>
  {
  }

  public new abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SVATTaxTran.curyTaxAmt>
  {
  }

  public new abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SVATTaxTran.taxAmt>
  {
  }

  public new abstract class curyTaxAmtSumm : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SVATTaxTran.curyTaxAmtSumm>
  {
  }

  public new abstract class taxAmtSumm : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SVATTaxTran.taxAmtSumm>
  {
  }
}
