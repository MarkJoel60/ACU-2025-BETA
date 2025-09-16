// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxTranRevReport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.TX;

[PXProjection(typeof (Select2<TaxTran, InnerJoin<TaxRev, On<TaxTran.taxID, Equal<TaxRev.taxID>, And<TaxRev.outdated, Equal<boolFalse>, And<TaxTran.taxType, Equal<TaxRev.taxType>, And<TaxTran.tranDate, Between<TaxRev.startDate, TaxRev.endDate>, And<TaxTran.released, Equal<True>, And<TaxTran.voided, Equal<False>, And<TaxTran.taxPeriodID, IsNull, And<TaxTran.origRefNbr, Equal<Empty>, And<TaxTran.taxType, NotEqual<PX.Objects.TX.TaxType.pendingPurchase>, And<TaxTran.taxType, NotEqual<PX.Objects.TX.TaxType.pendingSales>, And2<Where<CurrentValue<PX.Objects.AP.Vendor.taxReportFinPeriod>, Equal<boolTrue>, Or<TaxTran.tranDate, Less<CurrentValue<TaxPeriod.endDate>>>>, And<Where<CurrentValue<PX.Objects.AP.Vendor.taxReportFinPeriod>, Equal<boolFalse>, Or<TaxTran.finDate, Less<CurrentValue<TaxPeriod.endDate>>>>>>>>>>>>>>>>>>))]
[PXHidden]
public class TaxTranRevReport : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _TranDate;
  protected 
  #nullable disable
  string _FinPeriodID;

  [Branch(null, null, true, true, true, BqlField = typeof (TaxTran.branchID))]
  public virtual int? BranchID { get; set; }

  [PXDBString(5, IsUnicode = true, BqlField = typeof (TaxTran.curyID))]
  public virtual string CuryID { get; set; }

  [PXDBDate(BqlField = typeof (TaxTran.tranDate))]
  public virtual DateTime? TranDate { get; set; }

  [PXDBString(BqlField = typeof (TaxTran.finPeriodID))]
  public virtual string FinPeriodID { get; set; }

  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (TaxTran.module))]
  public virtual string Module { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (TaxTran.recordID))]
  public virtual int? RecordID { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (TaxTran.tranType))]
  public virtual string TranType { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (TaxTran.taxType))]
  public virtual string TaxType { get; set; }

  [PXDBString(60, IsUnicode = true, IsKey = true, BqlField = typeof (TaxTran.taxID))]
  public virtual string TaxID { get; set; }

  [PXDBInt(BqlField = typeof (TaxTran.taxBucketID))]
  public virtual int? TaxBucketID { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (TaxTran.taxZoneID))]
  public virtual string TaxZoneID { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (TaxTran.refNbr))]
  public virtual string RefNbr { get; set; }

  [PXDBDecimal(4, BqlField = typeof (TaxTran.taxAmt))]
  public virtual Decimal? TaxAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (TaxTran.curyTaxAmt))]
  public virtual Decimal? CuryTaxAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (TaxTran.taxableAmt))]
  public virtual Decimal? TaxableAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (TaxTran.curyTaxableAmt))]
  public virtual Decimal? CuryTaxableAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (TaxTran.exemptedAmt))]
  public virtual Decimal? ExemptedAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (TaxTran.curyExemptedAmt))]
  public virtual Decimal? CuryExemptedAmt { get; set; }

  [PXDBBool(BqlField = typeof (TaxTran.released))]
  public virtual bool? Released { get; set; }

  [PXDBBool(BqlField = typeof (TaxTran.voided))]
  public virtual bool? Voided { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (TaxTran.taxPeriodID))]
  public virtual string TaxPeriodID { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (TaxTran.origRefNbr))]
  public virtual string OrigRefNbr { get; set; }

  [PXDBDate(BqlField = typeof (TaxTran.finDate))]
  public virtual DateTime? FinDate { get; set; }

  [PXDBInt(BqlField = typeof (TaxRev.taxVendorID))]
  public virtual int? TaxRevTaxVendorID { get; set; }

  [PXDBInt(BqlField = typeof (TaxRev.taxBucketID))]
  public virtual int? TaxRevTaxBucketID { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTranRevReport.branchID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranRevReport.curyID>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxTranRevReport.tranDate>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranRevReport.finPeriodID>
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranRevReport.module>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTranRevReport.recordID>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranRevReport.tranType>
  {
  }

  public abstract class taxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranRevReport.taxType>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranRevReport.taxID>
  {
  }

  public abstract class taxBucketID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxTranRevReport.taxBucketID>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranRevReport.taxZoneID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranRevReport.refNbr>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTranRevReport.taxAmt>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTranRevReport.curyTaxAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxTranRevReport.taxableAmt>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTranRevReport.curyTaxableAmt>
  {
  }

  public abstract class exemptedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTranRevReport.exemptedAmt>
  {
  }

  public abstract class curyExemptedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxTranRevReport.exemptedAmt>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxTranRevReport.released>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxTranRevReport.voided>
  {
  }

  public abstract class taxPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranRevReport.taxPeriodID>
  {
  }

  public abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxTranRevReport.origRefNbr>
  {
  }

  public abstract class finDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxTranRevReport.finDate>
  {
  }

  public abstract class taxRevTaxVendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaxTranRevReport.taxRevTaxVendorID>
  {
  }

  public abstract class taxRevTaxBucketID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaxTranRevReport.taxRevTaxBucketID>
  {
  }
}
