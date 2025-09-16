// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxHistorySum
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.TX;

[PXProjection(typeof (Select4<TaxHistory, Aggregate<GroupBy<TaxHistory.branchID, GroupBy<TaxHistory.vendorID, GroupBy<TaxHistory.taxReportRevisionID, GroupBy<TaxHistory.taxPeriodID, GroupBy<TaxHistory.lineNbr, GroupBy<TaxHistory.revisionID, Sum<TaxHistory.filedAmt, Sum<TaxHistory.unfiledAmt, Sum<TaxHistory.reportFiledAmt, Sum<TaxHistory.reportUnfiledAmt>>>>>>>>>>>>))]
[PXCacheName("Tax History Sum")]
[Serializable]
public class TaxHistorySum : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected int? _VendorID;
  protected 
  #nullable disable
  string _TaxPeriodID;
  protected int? _RevisionID;
  protected int? _LineNbr;
  protected Decimal? _FiledAmt;
  protected Decimal? _UnfiledAmt;
  protected Decimal? _ReportFiledAmt;
  protected Decimal? _ReportUnfiledAmt;

  [PXDBInt(IsKey = true, BqlTable = typeof (TaxHistory))]
  [PXDefault]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBInt(IsKey = true, BqlTable = typeof (TaxHistory))]
  [PXDefault]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBInt(IsKey = true, BqlTable = typeof (TaxHistory))]
  [PXDefault]
  public virtual int? TaxReportRevisionID { get; set; }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlTable = typeof (TaxHistory))]
  [PXDefault]
  public virtual string TaxPeriodID
  {
    get => this._TaxPeriodID;
    set => this._TaxPeriodID = value;
  }

  [PXDBInt(IsKey = true, BqlTable = typeof (TaxHistory))]
  [PXDefault]
  [PXUIField]
  public virtual int? RevisionID
  {
    get => this._RevisionID;
    set => this._RevisionID = value;
  }

  [PXDBInt(IsKey = true, BqlTable = typeof (TaxHistory))]
  [PXDefault]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBVendorCury(typeof (TaxHistory.vendorID), typeof (TaxHistory.branchID), BqlTable = typeof (TaxHistory))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? FiledAmt
  {
    get => this._FiledAmt;
    set => this._FiledAmt = value;
  }

  [PXDBVendorCury(typeof (TaxHistory.vendorID), typeof (TaxHistory.branchID), BqlTable = typeof (TaxHistory))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? UnfiledAmt
  {
    get => this._UnfiledAmt;
    set => this._UnfiledAmt = value;
  }

  [PXDBVendorCury(typeof (TaxHistory.vendorID), typeof (TaxHistory.branchID), BqlTable = typeof (TaxHistory))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ReportFiledAmt
  {
    get => this._ReportFiledAmt;
    set => this._ReportFiledAmt = value;
  }

  [PXDBVendorCury(typeof (TaxHistory.vendorID), typeof (TaxHistory.branchID), BqlTable = typeof (TaxHistory))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ReportUnfiledAmt
  {
    get => this._ReportUnfiledAmt;
    set => this._ReportUnfiledAmt = value;
  }

  public class PK : 
    PrimaryKeyOf<TaxHistorySum>.By<TaxHistorySum.branchID, TaxHistorySum.vendorID, TaxHistorySum.taxReportRevisionID, TaxHistorySum.taxPeriodID, TaxHistorySum.lineNbr, TaxHistorySum.revisionID>
  {
    public static TaxHistorySum Find(
      PXGraph graph,
      int? branchID,
      int? vendorID,
      int? taxReportRevisionID,
      string taxPeriodID,
      int? lineNbr,
      int? revisionID,
      PKFindOptions options = 0)
    {
      return (TaxHistorySum) PrimaryKeyOf<TaxHistorySum>.By<TaxHistorySum.branchID, TaxHistorySum.vendorID, TaxHistorySum.taxReportRevisionID, TaxHistorySum.taxPeriodID, TaxHistorySum.lineNbr, TaxHistorySum.revisionID>.FindBy(graph, (object) branchID, (object) vendorID, (object) taxReportRevisionID, (object) taxPeriodID, (object) lineNbr, (object) revisionID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<TaxHistorySum>.By<TaxHistorySum.branchID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<TaxHistorySum>.By<TaxHistorySum.vendorID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxHistorySum.branchID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxHistorySum.vendorID>
  {
  }

  public abstract class taxReportRevisionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaxHistorySum.taxReportRevisionID>
  {
  }

  public abstract class taxPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxHistorySum.taxPeriodID>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxHistorySum.revisionID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxHistorySum.lineNbr>
  {
  }

  public abstract class filedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxHistorySum.filedAmt>
  {
  }

  public abstract class unfiledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxHistorySum.unfiledAmt>
  {
  }

  public abstract class reportFiledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxHistorySum.reportFiledAmt>
  {
  }

  public abstract class reportUnfiledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxHistorySum.reportUnfiledAmt>
  {
  }
}
