// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.TX;

[PXCacheName("Tax History")]
[Serializable]
public class TaxHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected int? _AccountID;
  protected int? _SubID;
  protected int? _VendorID;
  protected 
  #nullable disable
  string _TaxID;
  protected string _TaxPeriodID;
  protected int? _LineNbr;
  protected int? _RevisionID;
  protected string _CuryID;
  protected Decimal? _FiledAmt;
  protected Decimal? _UnfiledAmt;
  protected Decimal? _ReportFiledAmt;
  protected Decimal? _ReportUnfiledAmt;
  protected byte[] _tstamp;

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(1)]
  public virtual int? TaxReportRevisionID { get; set; }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public virtual string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXDefault]
  public virtual string TaxPeriodID
  {
    get => this._TaxPeriodID;
    set => this._TaxPeriodID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField]
  public virtual int? RevisionID
  {
    get => this._RevisionID;
    set => this._RevisionID = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency")]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? FiledAmt
  {
    get => this._FiledAmt;
    set => this._FiledAmt = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? UnfiledAmt
  {
    get => this._UnfiledAmt;
    set => this._UnfiledAmt = value;
  }

  [PXDBVendorCury(typeof (TaxHistory.vendorID), typeof (TaxHistory.branchID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ReportFiledAmt
  {
    get => this._ReportFiledAmt;
    set => this._ReportFiledAmt = value;
  }

  [PXDBVendorCury(typeof (TaxHistory.vendorID), typeof (TaxHistory.branchID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ReportUnfiledAmt
  {
    get => this._ReportUnfiledAmt;
    set => this._ReportUnfiledAmt = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<TaxHistory>.By<TaxHistory.branchID, TaxHistory.vendorID, TaxHistory.taxReportRevisionID, TaxHistory.accountID, TaxHistory.subID, TaxHistory.taxID, TaxHistory.taxPeriodID, TaxHistory.lineNbr, TaxHistory.revisionID>
  {
    public static TaxHistory Find(
      PXGraph graph,
      int? branchID,
      int? vendorID,
      int? taxReportRevisionID,
      int? accountID,
      int? subID,
      string taxID,
      string taxPeriodID,
      int? lineNbr,
      int? revisionID,
      PKFindOptions options = 0)
    {
      return (TaxHistory) PrimaryKeyOf<TaxHistory>.By<TaxHistory.branchID, TaxHistory.vendorID, TaxHistory.taxReportRevisionID, TaxHistory.accountID, TaxHistory.subID, TaxHistory.taxID, TaxHistory.taxPeriodID, TaxHistory.lineNbr, TaxHistory.revisionID>.FindBy(graph, (object) branchID, (object) vendorID, (object) taxReportRevisionID, (object) accountID, (object) subID, (object) taxID, (object) taxPeriodID, (object) lineNbr, (object) revisionID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<TaxHistory>.By<TaxHistory.branchID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<TaxHistory>.By<TaxHistory.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<TaxHistory>.By<TaxHistory.subID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<TaxHistory>.By<TaxHistory.vendorID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<TaxHistory>.By<TaxHistory.curyID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxHistory.branchID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxHistory.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxHistory.subID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxHistory.vendorID>
  {
  }

  public abstract class taxReportRevisionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaxHistory.taxReportRevisionID>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxHistory.taxID>
  {
  }

  public abstract class taxPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxHistory.taxPeriodID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxHistory.lineNbr>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxHistory.revisionID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxHistory.curyID>
  {
  }

  public abstract class filedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxHistory.filedAmt>
  {
  }

  public abstract class unfiledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxHistory.unfiledAmt>
  {
  }

  public abstract class reportFiledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxHistory.reportFiledAmt>
  {
  }

  public abstract class reportUnfiledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxHistory.reportUnfiledAmt>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TaxHistory.Tstamp>
  {
  }
}
