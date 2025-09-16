// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CopyPricesFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.AR;

[Serializable]
public class CopyPricesFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _SourcePriceType;
  protected string _SourcePriceCode;
  protected string _SourceCuryID;
  protected int? _SourceSiteID;
  protected DateTime? _EffectiveDate;
  protected bool? _IsPromotional;
  protected string _DestinationPriceType;
  protected string _DestinationPriceCode;
  protected string _DestinationCuryID;
  protected int? _DestinationSiteID;
  protected string _RateTypeID;
  protected DateTime? _CurrencyDate;
  protected Decimal? _CustomRate;

  [PXString(1, IsFixed = true)]
  [PXDefault("P")]
  [PriceTypeList.List]
  [PXUIField]
  public virtual string SourcePriceType
  {
    get => this._SourcePriceType;
    set => this._SourcePriceType = value;
  }

  [PXString(30, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [ARPriceCodeSelector(typeof (CopyPricesFilter.sourcePriceType))]
  public virtual string SourcePriceCode
  {
    get => this._SourcePriceCode;
    set => this._SourcePriceCode = value;
  }

  [PXDBString(5)]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  [PXUIField(DisplayName = "Source Currency", Required = true)]
  public virtual string SourceCuryID
  {
    get => this._SourceCuryID;
    set => this._SourceCuryID = value;
  }

  [NullableSite]
  public virtual int? SourceSiteID
  {
    get => this._SourceSiteID;
    set => this._SourceSiteID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("U")]
  [PriceTaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  [PXDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Effective As Of", Required = true)]
  public virtual DateTime? EffectiveDate
  {
    get => this._EffectiveDate;
    set => this._EffectiveDate = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Promotional Price")]
  public virtual bool? IsPromotional
  {
    get => this._IsPromotional;
    set => this._IsPromotional = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsFairValue { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsProrated { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Discountable { get; set; }

  [PXString(1, IsFixed = true)]
  [PXDefault("P")]
  [PriceTypeList.List]
  [PXUIField]
  public virtual string DestinationPriceType
  {
    get => this._DestinationPriceType;
    set => this._DestinationPriceType = value;
  }

  [PXString(30, InputMask = ">aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
  [PXDefault]
  [PXUIField]
  [ARPriceCodeSelector(typeof (CopyPricesFilter.destinationPriceType))]
  public virtual string DestinationPriceCode
  {
    get => this._DestinationPriceCode;
    set => this._DestinationPriceCode = value;
  }

  [PXDBString(5)]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  [PXUIField(DisplayName = "Destination Currency", Required = true)]
  public virtual string DestinationCuryID
  {
    get => this._DestinationCuryID;
    set => this._DestinationCuryID = value;
  }

  [NullableSite]
  public virtual int? DestinationSiteID
  {
    get => this._DestinationSiteID;
    set => this._DestinationSiteID = value;
  }

  [PXString(6)]
  [PXDefault(typeof (ARSetup.defaultRateTypeID))]
  [PXSelector(typeof (PX.Objects.CM.CurrencyRateType.curyRateTypeID))]
  [PXUIField(DisplayName = "Rate Type")]
  public virtual string RateTypeID
  {
    get => this._RateTypeID;
    set => this._RateTypeID = value;
  }

  [PXDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Currency Effective Date")]
  public virtual DateTime? CurrencyDate
  {
    get => this._CurrencyDate;
    set => this._CurrencyDate = value;
  }

  [PXDefault("1.00")]
  [PXDecimal(6, MinValue = 0.0)]
  [PXUIField]
  public virtual Decimal? CustomRate
  {
    get => this._CustomRate;
    set => this._CustomRate = value;
  }

  public abstract class sourcePriceType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CopyPricesFilter.sourcePriceType>
  {
  }

  public abstract class sourcePriceCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CopyPricesFilter.sourcePriceCode>
  {
  }

  public abstract class sourceCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CopyPricesFilter.sourceCuryID>
  {
  }

  public abstract class sourceSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CopyPricesFilter.sourceSiteID>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CopyPricesFilter.taxCalcMode>
  {
  }

  public abstract class effectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CopyPricesFilter.effectiveDate>
  {
  }

  public abstract class isPromotional : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CopyPricesFilter.isPromotional>
  {
  }

  public abstract class isFairValue : IBqlField, IBqlOperand
  {
  }

  public abstract class isProrated : IBqlField, IBqlOperand
  {
  }

  public abstract class discountable : IBqlField, IBqlOperand
  {
  }

  public abstract class destinationPriceType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CopyPricesFilter.destinationPriceType>
  {
  }

  public abstract class destinationPriceCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CopyPricesFilter.destinationPriceCode>
  {
  }

  public abstract class destinationCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CopyPricesFilter.destinationCuryID>
  {
  }

  public abstract class destinationSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CopyPricesFilter.destinationSiteID>
  {
  }

  public abstract class rateTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CopyPricesFilter.rateTypeID>
  {
  }

  public abstract class currencyDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CopyPricesFilter.currencyDate>
  {
  }

  public abstract class customRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CopyPricesFilter.customRate>
  {
  }
}
