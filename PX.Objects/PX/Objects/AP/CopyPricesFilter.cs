// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.CopyPricesFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.AP;

[Serializable]
public class CopyPricesFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SourceVendorID;
  protected 
  #nullable disable
  string _SourceCuryID;
  protected int? _SourceSiteID;
  protected System.DateTime? _EffectiveDate;
  protected bool? _IsPromotional;
  protected int? _DestinationVendorID;
  protected string _DestinationCuryID;
  protected int? _DestinationSiteID;
  protected string _RateTypeID;
  protected System.DateTime? _CurrencyDate;
  protected Decimal? _CustomRate;

  [Vendor(DisplayName = "Source Vendor")]
  [PXDefault]
  public virtual int? SourceVendorID
  {
    get => this._SourceVendorID;
    set => this._SourceVendorID = value;
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

  [PXDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Effective As Of", Required = true)]
  public virtual System.DateTime? EffectiveDate
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

  [Vendor(DisplayName = "Destination Vendor")]
  [PXDefault]
  public virtual int? DestinationVendorID
  {
    get => this._DestinationVendorID;
    set => this._DestinationVendorID = value;
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
  [PXDefault]
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
  public virtual System.DateTime? CurrencyDate
  {
    get => this._CurrencyDate;
    set => this._CurrencyDate = value;
  }

  [PXDefault("1.00")]
  [PXDecimal(6, MinValue = 0.0)]
  [PXUIField(DisplayName = "Currency Rate", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? CustomRate
  {
    get => this._CustomRate;
    set => this._CustomRate = value;
  }

  public abstract class sourceVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CopyPricesFilter.sourceVendorID>
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

  public abstract class effectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
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

  public abstract class destinationVendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CopyPricesFilter.destinationVendorID>
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
    IBqlDateTime, System.DateTime>.Field<
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
