// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxDetailReport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.TX;

[PXProjection(typeof (Select2<TaxPeriodForReportShowing, InnerJoin<TaxReport, On<TaxReport.vendorID, Equal<TaxPeriodForReportShowing.vendorID>, And<Add<TaxPeriodForReportShowing.endDate, int_1>, Between<TaxReport.validFrom, TaxReport.validTo>>>, InnerJoin<TaxReportLine, On<TaxReportLine.vendorID, Equal<TaxPeriodForReportShowing.vendorID>, And<TaxReportLine.taxReportRevisionID, Equal<TaxReport.revisionID>>>, InnerJoin<TaxBucketLine, On<TaxBucketLine.vendorID, Equal<TaxReportLine.vendorID>, And<TaxBucketLine.taxReportRevisionID, Equal<TaxReportLine.taxReportRevisionID>, And<TaxBucketLine.lineNbr, Equal<TaxReportLine.lineNbr>>>>, InnerJoin<PX.Objects.GL.Branch, On<TaxPeriodForReportShowing.organizationID, Equal<PX.Objects.GL.Branch.organizationID>>, LeftJoin<TaxTran, On<TaxTran.vendorID, Equal<TaxBucketLine.vendorID>, And<TaxTran.released, Equal<True>, And<TaxTran.voided, Equal<False>, And<TaxTran.taxType, NotEqual<PX.Objects.TX.TaxType.pendingSales>, And<TaxTran.taxType, NotEqual<PX.Objects.TX.TaxType.pendingPurchase>, And<TaxTran.taxBucketID, Equal<TaxBucketLine.bucketID>, And2<Where<TaxReportLine.taxZoneID, IsNull, And<TaxReportLine.tempLine, Equal<False>, Or<TaxReportLine.taxZoneID, Equal<TaxTran.taxZoneID>>>>, And<TaxTran.taxPeriodID, Equal<TaxPeriodForReportShowing.taxPeriodID>, And<TaxTran.branchID, Equal<PX.Objects.GL.Branch.branchID>>>>>>>>>>>>>>>>))]
[PXCacheName("Tax Report Detail")]
public class TaxDetailReport : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _LineNbr;
  protected short? _LineMult;
  protected 
  #nullable disable
  string _LineType;
  protected string _Module;
  protected string _TranType;
  protected string _TranTypeInvoiceDiscriminated;
  protected string _RefNbr;
  protected int? _RecordID;
  protected bool? _Released;
  protected bool? _Voided;
  protected string _TaxPeriodID;
  protected string _TaxID;
  protected int? _VendorID;
  protected int? _BranchID;
  protected string _TaxZoneID;
  protected int? _AccountID;
  protected int? _SubID;
  protected DateTime? _TranDate;
  protected string _TaxType;
  protected Decimal? _TaxRate;
  protected Decimal? _ReportTaxableAmt;
  protected Decimal? _ReportExemptedAmt;
  protected Decimal? _ReportTaxAmt;

  [PXDBInt(IsKey = true, BqlField = typeof (TaxReportLine.lineNbr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBShort(BqlField = typeof (TaxReportLine.lineMult))]
  public virtual short? LineMult
  {
    get => this._LineMult;
    set => this._LineMult = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (TaxReportLine.lineType))]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (TaxTran.module))]
  public virtual string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (TaxTran.tranType))]
  [TaxAdjustmentType.List]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXString]
  [PXDBCalced(typeof (Switch<Case<Where<TaxTran.module, Equal<BatchModule.moduleAP>, And<TaxTran.tranType, Equal<APDocType.invoice>>>, TaxTranReport.tranTypeInvoiceDiscriminated.apInvoice, Case<Where<TaxTran.module, Equal<BatchModule.moduleAR>, And<TaxTran.tranType, Equal<ARDocType.invoice>>>, TaxTranReport.tranTypeInvoiceDiscriminated.arInvoice>>, TaxTran.tranType>), typeof (string))]
  [LabelList(typeof (TaxTranReport.tranTypeInvoiceDiscriminated))]
  public virtual string TranTypeInvoiceDiscriminated
  {
    get => this._TranTypeInvoiceDiscriminated;
    set => this._TranTypeInvoiceDiscriminated = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (TaxTran.refNbr))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (TaxTran.recordID))]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [PXDBBool(BqlField = typeof (TaxTran.released))]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXDBBool(BqlField = typeof (TaxTran.voided))]
  public virtual bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (TaxPeriodForReportShowing.taxPeriodID))]
  public virtual string TaxPeriodID
  {
    get => this._TaxPeriodID;
    set => this._TaxPeriodID = value;
  }

  [PXDBString(60, IsUnicode = true, IsKey = true, BqlField = typeof (TaxTran.taxID))]
  public virtual string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [Vendor(BqlField = typeof (TaxTran.vendorID))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [Branch(null, null, true, true, true, BqlField = typeof (TaxTran.branchID))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (TaxTran.taxZoneID))]
  public virtual string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

  [Account(null, BqlField = typeof (TaxTran.accountID))]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount(typeof (TaxTran.accountID), BqlField = typeof (TaxTran.subID))]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBDate(BqlField = typeof (TaxTran.tranDate))]
  public virtual DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (TaxTran.taxType))]
  public virtual string TaxType
  {
    get => this._TaxType;
    set => this._TaxType = value;
  }

  [PXDBDecimal(6, BqlField = typeof (TaxTran.taxRate))]
  public virtual Decimal? TaxRate
  {
    get => this._TaxRate;
    set => this._TaxRate = value;
  }

  [PXDBVendorCury(typeof (TaxDetailReport.vendorID), typeof (TaxDetailReport.branchID), BqlField = typeof (TaxTran.reportTaxableAmt))]
  public virtual Decimal? ReportTaxableAmt
  {
    [PXDependsOnFields(new Type[] {typeof (TaxDetailReport.module), typeof (TaxDetailReport.tranType), typeof (TaxDetailReport.taxType), typeof (TaxDetailReport.lineMult)})] get
    {
      Decimal mult = ReportTaxProcess.GetMult(this._Module, this._TranType, this._TaxType, this._LineMult);
      Decimal? reportTaxableAmt = this._ReportTaxableAmt;
      return !reportTaxableAmt.HasValue ? new Decimal?() : new Decimal?(mult * reportTaxableAmt.GetValueOrDefault());
    }
    set => this._ReportTaxableAmt = value;
  }

  [PXDBDecimal(BqlField = typeof (TaxTran.reportExemptedAmt))]
  public virtual Decimal? ReportExemptedAmt
  {
    [PXDependsOnFields(new Type[] {typeof (TaxDetailReport.module), typeof (TaxDetailReport.tranType), typeof (TaxDetailReport.taxType), typeof (TaxDetailReport.lineMult)})] get
    {
      Decimal mult = ReportTaxProcess.GetMult(this._Module, this._TranType, this._TaxType, this._LineMult);
      Decimal? reportExemptedAmt = this._ReportExemptedAmt;
      return !reportExemptedAmt.HasValue ? new Decimal?() : new Decimal?(mult * reportExemptedAmt.GetValueOrDefault());
    }
    set => this._ReportExemptedAmt = value;
  }

  [PXDBVendorCury(typeof (TaxDetailReport.vendorID), typeof (TaxDetailReport.branchID), BqlField = typeof (TaxTran.reportTaxAmt))]
  public virtual Decimal? ReportTaxAmt
  {
    [PXDependsOnFields(new Type[] {typeof (TaxDetailReport.module), typeof (TaxDetailReport.tranType), typeof (TaxDetailReport.taxType), typeof (TaxDetailReport.lineMult)})] get
    {
      Decimal mult = ReportTaxProcess.GetMult(this._Module, this._TranType, this._TaxType, this._LineMult);
      Decimal? reportTaxAmt = this._ReportTaxAmt;
      return !reportTaxAmt.HasValue ? new Decimal?() : new Decimal?(mult * reportTaxAmt.GetValueOrDefault());
    }
    set => this._ReportTaxAmt = value;
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxDetailReport.lineNbr>
  {
  }

  public abstract class lineMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  TaxDetailReport.lineMult>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxDetailReport.lineType>
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxDetailReport.module>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxDetailReport.tranType>
  {
  }

  public abstract class tranTypeInvoiceDiscriminated : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxDetailReport.tranTypeInvoiceDiscriminated>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxDetailReport.refNbr>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxDetailReport.recordID>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxDetailReport.released>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxDetailReport.voided>
  {
  }

  public abstract class taxPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxDetailReport.taxPeriodID>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxDetailReport.taxID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxDetailReport.vendorID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxDetailReport.branchID>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxDetailReport.taxZoneID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxDetailReport.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxDetailReport.subID>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxDetailReport.tranDate>
  {
  }

  public abstract class taxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxDetailReport.taxType>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxDetailReport.taxRate>
  {
  }

  public abstract class reportTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxDetailReport.reportTaxableAmt>
  {
  }

  public abstract class reportExemptedAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class reportTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxDetailReport.reportTaxAmt>
  {
  }
}
