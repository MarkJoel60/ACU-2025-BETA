// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.RevaluedGLHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CM;

[PXBreakInheritance]
[Serializable]
public class RevaluedGLHistory : GLHistory
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _CuryRateTypeID;
  protected Decimal? _CuryRate = new Decimal?(1M);
  protected Decimal? _RateReciprocal;
  protected DateTime? _CuryEffDate;
  protected string _CuryMultDiv = "M";
  protected string _AccountType;
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

  [PXString(1)]
  [PX.Objects.GL.AccountType.List]
  [PXUIField(DisplayName = "Type")]
  public virtual string AccountType
  {
    get => this._AccountType;
    set => this._AccountType = value;
  }

  [PXUIField(TabOrder = 0)]
  [Branch(null, null, true, true, true, IsKey = true, IsDetail = true)]
  public override int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
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

  [PXDBCury(typeof (RevaluedGLHistory.curyID))]
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
  [PXUIField(DisplayName = "Revalued Balance", Enabled = false)]
  [PXFormula(typeof (Add<RevaluedGLHistory.finYtdBalance, RevaluedGLHistory.finPtdRevalued>))]
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
  RevaluedGLHistory.selected>
  {
  }

  public abstract class curyRateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RevaluedGLHistory.curyRateTypeID>
  {
  }

  public abstract class curyRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RevaluedGLHistory.curyRate>
  {
  }

  public abstract class rateReciprocal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RevaluedGLHistory.rateReciprocal>
  {
  }

  public abstract class curyEffDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RevaluedGLHistory.curyEffDate>
  {
  }

  public abstract class curyMultDiv : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RevaluedGLHistory.curyMultDiv>
  {
  }

  public abstract class accountType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RevaluedGLHistory.accountType>
  {
  }

  public new abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RevaluedGLHistory.ledgerID>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RevaluedGLHistory.branchID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RevaluedGLHistory.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RevaluedGLHistory.subID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RevaluedGLHistory.finPeriodID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RevaluedGLHistory.curyID>
  {
  }

  public new abstract class curyFinYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RevaluedGLHistory.curyFinYtdBalance>
  {
  }

  public new abstract class finYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RevaluedGLHistory.finYtdBalance>
  {
  }

  public abstract class finYtdRevalued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RevaluedGLHistory.finYtdRevalued>
  {
  }

  public new abstract class finPtdRevalued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RevaluedGLHistory.finPtdRevalued>
  {
  }

  public abstract class lastRevaluedFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RevaluedGLHistory.lastRevaluedFinPeriodID>
  {
  }
}
