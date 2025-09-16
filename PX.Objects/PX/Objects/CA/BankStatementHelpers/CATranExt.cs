// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankStatementHelpers.CATranExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.CA.BankStatementHelpers;

[Serializable]
public class CATranExt : CATran, IBankMatchRelevance
{
  protected bool? _IsMatched;
  protected Decimal? _MatchRelevance;
  protected bool? _IsBestMatch;

  [PXBool]
  [PXUIField(DisplayName = "Matched")]
  public virtual bool? IsMatched
  {
    get => this._IsMatched;
    set => this._IsMatched = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDecimal(3)]
  [PXUIField(DisplayName = "Match Relevance", Enabled = false)]
  public virtual Decimal? MatchRelevance
  {
    get => this._MatchRelevance;
    set => this._MatchRelevance = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDecimal(3)]
  [PXUIField(DisplayName = "Match Relevance, %", Enabled = false)]
  [PXFormula(typeof (Mult<CATranExt.matchRelevance, decimal100>))]
  public virtual Decimal? MatchRelevancePercent { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Released", Enabled = false)]
  [PXDefault(false)]
  public override bool? Released { get; set; }

  [PXCurrency(typeof (CATran.curyInfoID), typeof (CATran.tranAmt))]
  [PXUIField]
  public virtual Decimal? CuryTranAbsAmt
  {
    get => new Decimal?(Math.Abs(this.CuryTranAmt.GetValueOrDefault()));
    set
    {
    }
  }

  [PXDecimal(4)]
  [PXUIField(DisplayName = "Tran. Amount")]
  public virtual Decimal? TranAbsAmt
  {
    get => new Decimal?(Math.Abs(this.TranAmt.GetValueOrDefault()));
    set
    {
    }
  }

  [PXCurrency(typeof (CATran.curyInfoID), typeof (CATranExt.tranAmtCalc))]
  [PXUIField]
  public virtual Decimal? CuryTranAmtCalc { get; set; }

  [PXDecimal(4)]
  [PXUIField(DisplayName = "Tran. Amount")]
  public virtual Decimal? TranAmtCalc { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Best Match")]
  public virtual bool? IsBestMatch
  {
    get => this._IsBestMatch;
    set => this._IsBestMatch = value;
  }

  public new abstract class tranID : BqlType<IBqlLong, long>.Field<
  #nullable disable
  CATranExt.tranID>
  {
  }

  public abstract class isMatched : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CATranExt.isMatched>
  {
  }

  public abstract class matchRelevance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CATranExt.matchRelevance>
  {
  }

  public abstract class matchRelevancePercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CATranExt.matchRelevancePercent>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CATranExt.released>
  {
  }

  public abstract class curyTranAbsAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CATranExt.curyTranAbsAmt>
  {
  }

  public abstract class tranAbsAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATranExt.tranAbsAmt>
  {
  }

  public abstract class curyTranAmtCalc : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CATranExt.curyTranAmtCalc>
  {
  }

  public abstract class tranAmtCalc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATranExt.tranAmtCalc>
  {
  }

  public abstract class isBestMatch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CATranExt.isBestMatch>
  {
  }

  public new abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATranExt.referenceID>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATranExt.status>
  {
  }
}
