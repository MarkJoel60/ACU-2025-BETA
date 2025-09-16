// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.RevaluedAPHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CM;

[PXCacheName("Revalued AP History")]
[PXBreakInheritance]
[Serializable]
public class RevaluedAPHistory : CuryAPHistory
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _CuryRateTypeID;
  protected Decimal? _CuryRate = new Decimal?(1M);
  protected Decimal? _RateReciprocal;
  protected DateTime? _CuryEffDate;
  protected string _CuryMultDiv = "M";
  protected string _VendorClassID;
  protected Decimal? _FinPrevRevalued;
  protected Decimal? _FinYtdRevalued;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXString(6, IsUnicode = true)]
  [PXUIField(DisplayName = "Currency Rate Type")]
  [PXSelector(typeof (CurrencyRateType.curyRateTypeID))]
  public virtual string CuryRateTypeID
  {
    get => this._CuryRateTypeID;
    set => this._CuryRateTypeID = value;
  }

  [PXDecimal(6)]
  [PXUIField(DisplayName = "Currency Rate")]
  public virtual Decimal? CuryRate
  {
    get => this._CuryRate;
    set => this._CuryRate = value;
  }

  [PXDecimal(8)]
  public virtual Decimal? RateReciprocal
  {
    get => this._RateReciprocal;
    set => this._RateReciprocal = value;
  }

  [PXDate]
  public virtual DateTime? CuryEffDate
  {
    get => this._CuryEffDate;
    set => this._CuryEffDate = value;
  }

  [PXString(1, IsFixed = true)]
  public virtual string CuryMultDiv
  {
    get => this._CuryMultDiv;
    set => this._CuryMultDiv = value;
  }

  [Account(IsKey = true, DescriptionField = typeof (PX.Objects.GL.Account.description))]
  public override int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount(IsKey = true, DescriptionField = typeof (Sub.description))]
  public override int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [Branch(null, null, true, true, true, IsKey = true, IsDetail = true)]
  public override int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [Vendor(IsKey = true, DescriptionField = typeof (Vendor.acctName))]
  public override int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXString(10, IsUnicode = true)]
  [PXSelector(typeof (VendorClass.vendorClassID), DescriptionField = typeof (VendorClass.descr), CacheGlobal = true)]
  [PXUIField(DisplayName = "Vendor Class", Enabled = false)]
  public virtual string VendorClassID
  {
    get => this._VendorClassID;
    set => this._VendorClassID = value;
  }

  [PXDBCury(typeof (RevaluedAPHistory.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Foreign Currency Balance", Enabled = false)]
  public override Decimal? CuryFinYtdBalance
  {
    get => this._CuryFinYtdBalance;
    set => this._CuryFinYtdBalance = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Balance", Enabled = false)]
  public override Decimal? FinYtdBalance
  {
    get => this._FinYtdBalance;
    set => this._FinYtdBalance = value;
  }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "PTD Gain or Loss", Enabled = false)]
  public virtual Decimal? FinPrevRevalued
  {
    get => this._FinPrevRevalued;
    set => this._FinPrevRevalued = value;
  }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revalued Balance", Enabled = false)]
  [PXFormula(typeof (Add<Add<RevaluedAPHistory.finYtdBalance, RevaluedAPHistory.finPrevRevalued>, RevaluedAPHistory.finPtdRevalued>))]
  public virtual Decimal? FinYtdRevalued
  {
    get => this._FinYtdRevalued;
    set => this._FinYtdRevalued = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Difference", Enabled = true)]
  public override Decimal? FinPtdRevalued
  {
    get => this._FinPtdRevalued;
    set => this._FinPtdRevalued = value;
  }

  [PXUIField(DisplayName = "Last Revaluation Period")]
  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsDBField = false)]
  public virtual string LastRevaluedFinPeriodID { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RevaluedAPHistory.selected>
  {
  }

  public abstract class curyRateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RevaluedAPHistory.curyRateTypeID>
  {
  }

  public abstract class curyRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RevaluedAPHistory.curyRate>
  {
  }

  public abstract class rateReciprocal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RevaluedAPHistory.rateReciprocal>
  {
  }

  public abstract class curyEffDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RevaluedAPHistory.curyEffDate>
  {
  }

  public abstract class curyMultDiv : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RevaluedAPHistory.curyMultDiv>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RevaluedAPHistory.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RevaluedAPHistory.subID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RevaluedAPHistory.finPeriodID>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RevaluedAPHistory.branchID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RevaluedAPHistory.vendorID>
  {
  }

  public abstract class vendorClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RevaluedAPHistory.vendorClassID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RevaluedAPHistory.curyID>
  {
  }

  public new abstract class curyFinYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RevaluedAPHistory.curyFinYtdBalance>
  {
  }

  public new abstract class finYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RevaluedAPHistory.finYtdBalance>
  {
  }

  public new abstract class curyFinYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RevaluedAPHistory.curyFinYtdDeposits>
  {
  }

  public new abstract class finYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RevaluedAPHistory.finYtdDeposits>
  {
  }

  public abstract class finPrevRevalued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RevaluedAPHistory.finPrevRevalued>
  {
  }

  public abstract class finYtdRevalued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RevaluedAPHistory.finYtdRevalued>
  {
  }

  public new abstract class finPtdRevalued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RevaluedAPHistory.finPtdRevalued>
  {
  }

  public abstract class lastRevaluedFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RevaluedAPHistory.lastRevaluedFinPeriodID>
  {
  }
}
