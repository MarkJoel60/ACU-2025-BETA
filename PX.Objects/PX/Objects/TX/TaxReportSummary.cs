// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxReportSummary
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.TX;

[PXProjection(typeof (Select2<TaxPeriodForReportShowing, InnerJoin<TaxReport, On<TaxReport.vendorID, Equal<TaxPeriodForReportShowing.vendorID>, And<Add<TaxPeriodForReportShowing.endDate, int_1>, Between<TaxReport.validFrom, TaxReport.validTo>>>, InnerJoin<TaxReportLine, On<TaxReportLine.vendorID, Equal<TaxPeriodForReportShowing.vendorID>, And<TaxReportLine.taxReportRevisionID, Equal<TaxReport.revisionID>, And<TaxReportLine.tempLine, NotEqual<True>>>>, InnerJoin<PX.Objects.GL.Branch, On<TaxPeriodForReportShowing.organizationID, Equal<PX.Objects.GL.Branch.organizationID>>, LeftJoin<TaxHistorySum, On<TaxHistorySum.branchID, Equal<PX.Objects.GL.Branch.branchID>, And<TaxHistorySum.vendorID, Equal<TaxReportLine.vendorID>, And<TaxHistorySum.lineNbr, Equal<TaxReportLine.lineNbr>, And<TaxHistorySum.taxReportRevisionID, Equal<TaxReport.revisionID>, And<TaxHistorySum.taxPeriodID, Equal<TaxPeriodForReportShowing.taxPeriodID>>>>>>>>>>, Where<TaxReportLine.tempLineNbr, IsNull, Or<TaxHistorySum.vendorID, IsNotNull>>>))]
[PXCacheName("Tax Report Summary")]
public class TaxReportSummary : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _LineNbr;
  protected short? _LineMult;
  protected 
  #nullable disable
  string _LineType;
  protected string _TaxPeriodID;
  protected int? _VendorID;
  protected int? _RevisionID;
  protected int? _BranchID;
  protected Decimal? _FiledAmt;
  protected Decimal? _UnfiledAmt;
  protected Decimal? _ReportFiledAmt;
  protected Decimal? _ReportUnfiledAmt;

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

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (TaxPeriodForReportShowing.taxPeriodID))]
  public virtual string TaxPeriodID
  {
    get => this._TaxPeriodID;
    set => this._TaxPeriodID = value;
  }

  [Vendor(BqlField = typeof (TaxReportLine.vendorID))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBInt(IsKey = true, BqlTable = typeof (TaxHistorySum))]
  [PXDefault]
  [PXUIField]
  public virtual int? RevisionID
  {
    get => this._RevisionID;
    set => this._RevisionID = value;
  }

  [Branch(null, null, true, true, true, BqlField = typeof (TaxHistorySum.branchID), IsKey = true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBBaseCury(BqlTable = typeof (TaxHistorySum))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? FiledAmt
  {
    get => this._FiledAmt;
    set => this._FiledAmt = value;
  }

  [PXDBBaseCury(BqlTable = typeof (TaxHistorySum))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? UnfiledAmt
  {
    get => this._UnfiledAmt;
    set => this._UnfiledAmt = value;
  }

  [PXDBVendorCury(typeof (TaxReportSummary.vendorID), typeof (TaxReportSummary.branchID), BqlTable = typeof (TaxHistorySum))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ReportFiledAmt
  {
    get => this._ReportFiledAmt;
    set => this._ReportFiledAmt = value;
  }

  [PXDBVendorCury(typeof (TaxReportSummary.vendorID), typeof (TaxReportSummary.branchID), BqlTable = typeof (TaxHistorySum))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ReportUnfiledAmt
  {
    get => this._ReportUnfiledAmt;
    set => this._ReportUnfiledAmt = value;
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxReportSummary.lineNbr>
  {
  }

  public abstract class lineMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  TaxReportSummary.lineMult>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxReportSummary.lineType>
  {
  }

  public abstract class taxPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxReportSummary.taxPeriodID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxReportSummary.vendorID>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxReportSummary.revisionID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxReportSummary.branchID>
  {
  }

  public abstract class filedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxReportSummary.filedAmt>
  {
  }

  public abstract class unfiledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxReportSummary.unfiledAmt>
  {
  }

  public abstract class reportFiledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxReportSummary.reportFiledAmt>
  {
  }

  public abstract class reportUnfiledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxReportSummary.reportUnfiledAmt>
  {
  }
}
