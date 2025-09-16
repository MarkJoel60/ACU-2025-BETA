// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.CalculatePricesFilter
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
namespace PX.Objects.AP;

[Serializable]
public class CalculatePricesFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Decimal? _CorrectionPercent;
  protected short? _Rounding;
  protected 
  #nullable disable
  string _PriceBasis;
  protected bool? _UpdateOnZero;

  [PXDefault("100.00")]
  [PXDecimal(6, MinValue = 0.0, MaxValue = 1000.0)]
  [PXUIField(DisplayName = "Multiplier (% of Price Basis)", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? CorrectionPercent
  {
    get => this._CorrectionPercent;
    set => this._CorrectionPercent = value;
  }

  [PXDefault(2, typeof (Search<CommonSetup.decPlPrcCst>))]
  [PXDBShort(MinValue = 0, MaxValue = 6)]
  [PXUIField(DisplayName = "Decimal Places", Visibility = PXUIVisibility.Visible)]
  public virtual short? Rounding
  {
    get => this._Rounding;
    set => this._Rounding = value;
  }

  [PXString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Price Basis")]
  [PriceBasisTypes.List]
  [PXDefault("P")]
  public virtual string PriceBasis
  {
    get => this._PriceBasis;
    set => this._PriceBasis = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Update with Zero Price if Basis Is Zero", Visibility = PXUIVisibility.Service)]
  public virtual bool? UpdateOnZero
  {
    get => this._UpdateOnZero;
    set => this._UpdateOnZero = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [BaseCurrencySelector]
  public virtual string BaseCuryID { get; set; }

  public abstract class correctionPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CalculatePricesFilter.correctionPercent>
  {
  }

  public abstract class rounding : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CalculatePricesFilter.rounding>
  {
  }

  public abstract class priceBasis : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CalculatePricesFilter.priceBasis>
  {
  }

  public abstract class updateOnZero : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CalculatePricesFilter.updateOnZero>
  {
  }

  public abstract class baseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CalculatePricesFilter.baseCuryID>
  {
  }
}
