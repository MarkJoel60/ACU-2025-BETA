// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.CurrencyInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.Extensions.MultiCurrency;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CM.Extensions;

/// <summary>
/// Stores currency and exchange rate information for a particular document or transaction.
/// Usually, there is an individual record of this type for each document or transaction that involves monetary amounts and supports multi-currency.
/// The documents store a link to their instance of CurrencyInfo in the CuryInfoID field, such as <see cref="P:PX.Objects.GL.GLTran.CuryInfoID" />.
/// The exchange rate data for objects of this type is either entered by user or obtained from the <see cref="T:PX.Objects.CM.Extensions.CurrencyRate" /> records.
/// Records of this type are either created automatically by the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfoAttribute" /> or
/// explicitly inserted by the application code (such as in <see cref="T:PX.Objects.AR.ARReleaseProcess" />).
/// User must not be aware of existence of these records.
/// </summary>
[PXCacheName("Currency Info")]
[Serializable]
public class CurrencyInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// Key field. Database identity.
  /// Unique identifier of the Currency Info object.
  /// </summary>
  [PXDBLongIdentity(IsKey = true)]
  [PXUIField(Visible = false)]
  [PXDependsOnFields(new Type[] {typeof (CurrencyInfo.curyID), typeof (CurrencyInfo.baseCuryID), typeof (CurrencyInfo.sampleCuryRate)})]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>
  /// Identifier of the module, to which the Currency Info object belongs.
  /// The value of this field affects the choice of the default <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryRateTypeID">Rate Type</see>:
  /// for <c>"CA"</c> the Rate Type is taken from the <see cref="P:PX.Objects.CM.CMSetup.CARateTypeDflt" />,
  /// for <c>"AP"</c> the Rate Type is taken from the <see cref="P:PX.Objects.CM.CMSetup.APRateTypeDflt" />,
  /// for <c>"AR"</c> the Rate Type is taken from the <see cref="P:PX.Objects.CM.CMSetup.ARRateTypeDflt" />,
  /// for <c>"GL"</c> the Rate Type is taken from the <see cref="P:PX.Objects.CM.CMSetup.GLRateTypeDflt" />.
  /// </summary>
  public virtual 
  #nullable disable
  string ModuleCode { get; set; }

  /// <summary>
  /// When set to <c>true</c>, the system won't allow user to change the fields of this object.
  /// </summary>
  public virtual bool? IsReadOnly { get; set; }

  /// <summary>
  /// When <c>true</c>, indicates that the system must calculate the amounts in the
  /// <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.BaseCuryID">base currency</see> for the related document or transaction.
  /// Otherwise the changes in the amounts expressed in the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryID">currency of the document</see>
  /// won't result in an update to the amounts in base currency.
  /// </summary>
  /// <value>
  /// Defaults to <c>true</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? BaseCalc { get; set; }

  /// <summary>
  /// Identifier of the base <see cref="T:PX.Objects.CM.Extensions.Currency" />.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </value>
  [PXDBString(5, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Base Currency ID")]
  public virtual string BaseCuryID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.Extensions.Currency" /> of this Currency Info object.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </value>
  [CurrencyInfo.CuryIDString]
  [PXDefault]
  [PXUIField]
  [CurrencyInfo.CuryIDSelector]
  public virtual string CuryID { get; set; }

  /// <summary>
  /// The read-only property providing the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryID">Currency</see> for display in the User Interface.
  /// </summary>
  [PXString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Currency ID")]
  [PXDependsOnFields(new Type[] {typeof (CurrencyInfo.curyID)})]
  public virtual string DisplayCuryID
  {
    get => this.CuryID;
    set
    {
    }
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyRateType">Rate Type</see> associated with this object.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.Extensions.CurrencyRateType.CuryRateTypeID" /> field.
  /// </value>
  [PXDBString(6, IsUnicode = true)]
  [CurrencyInfo.CuryRateTypeIDSelector]
  [PXUIField(DisplayName = "Curr. Rate Type ID")]
  public virtual string CuryRateTypeID { get; set; }

  /// <summary>
  /// The date, starting from which the specified <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryRate">rate</see> is considered current.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Data.AccessInfo.BusinessDate">current business date</see>.
  /// </value>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Effective Date")]
  public virtual DateTime? CuryEffDate { get; set; }

  /// <summary>
  /// The operation required for currency conversion: Divide or Multiply.
  /// </summary>
  /// <value>
  /// Allowed values are:
  /// <c>"M"</c> - Multiply,
  /// <c>"D"</c> - Divide.
  /// Defaults to <c>"M"</c>.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("M")]
  [PXUIField(DisplayName = "Mult Div")]
  public virtual string CuryMultDiv { get; set; }

  /// <summary>
  /// The currency rate. For the purposes of conversion the value of this field is used
  /// together with the operation selected in the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryMultDiv" /> field.
  /// </summary>
  /// <value>
  /// Defaults to <c>1.0</c>.
  /// </value>
  [PXDBDecimal(8)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? CuryRate { get; set; }

  /// <summary>
  /// The inverse of the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryRate">exchange rate</see>, which is calculated automatically.
  /// </summary>
  [PXDBDecimal(8)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? RecipRate { get; set; }

  /// <summary>
  /// The exchange rate used for calculations and determined by the values of
  /// the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryMultDiv" />, <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryRate" /> and <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.RecipRate" /> fields.
  /// </summary>
  [PXDecimal(8)]
  [PXUIField(DisplayName = "Curr. Rate")]
  [PXDefault]
  public virtual Decimal? SampleCuryRate
  {
    [PXDependsOnFields(new Type[] {typeof (CurrencyInfo.curyMultDiv), typeof (CurrencyInfo.curyRate), typeof (CurrencyInfo.recipRate)})] get
    {
      return !(this.CuryMultDiv == "M") ? this.RecipRate : this.CuryRate;
    }
    set
    {
      if (this.CuryMultDiv == "M")
        this.CuryRate = value;
      else
        this.RecipRate = value;
    }
  }

  /// <summary>
  /// The inverse of the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.SampleCuryRate" />. This value is also determined by the values of
  /// the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryMultDiv" />, <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryRate" /> and <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.RecipRate" /> fields.
  /// </summary>
  [PXDecimal(8)]
  [PXUIField(DisplayName = "Reciprocal Rate")]
  public virtual Decimal? SampleRecipRate
  {
    [PXDependsOnFields(new Type[] {typeof (CurrencyInfo.curyMultDiv), typeof (CurrencyInfo.recipRate), typeof (CurrencyInfo.curyRate)})] get
    {
      return !(this.CuryMultDiv == "M") ? this.CuryRate : this.RecipRate;
    }
    set
    {
      if (this.CuryMultDiv == "M")
        this.RecipRate = value;
      else
        this.CuryRate = value;
    }
  }

  /// <summary>
  /// The number of digits after the decimal point used in operations with the amounts
  /// associated with this Currency Info object and expressed in its <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryID">currency</see>.
  /// </summary>
  /// <value>
  /// The value of this field is taken from the <see cref="P:PX.Objects.CM.Extensions.CurrencyList.DecimalPlaces" /> field of the record associated with the
  /// <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryID">currency</see> of this object.
  /// </value>
  [PXShort]
  public virtual short? CuryPrecision { get; set; }

  /// <summary>
  /// The number of digits after the decimal point used in operations with the amounts
  /// associated with this Currency Info object and expressed in its <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.BaseCuryID">base currency</see>.
  /// </summary>
  /// <value>
  /// The value of this field is taken from the <see cref="P:PX.Objects.CM.Extensions.CurrencyList.DecimalPlaces" /> field of the record associated with the
  /// <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.BaseCuryID">base currency</see> of this object.
  /// </value>
  [PXShort]
  public virtual short? BasePrecision { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public PX.Objects.CM.CurrencyInfo GetCM()
  {
    return new PX.Objects.CM.CurrencyInfo()
    {
      BaseCalc = this.BaseCalc,
      BaseCuryID = this.BaseCuryID,
      BasePrecision = this.BasePrecision,
      CuryEffDate = this.CuryEffDate,
      CuryID = this.CuryID,
      CuryInfoID = this.CuryInfoID,
      CuryMultDiv = this.CuryMultDiv,
      CuryPrecision = this.CuryPrecision,
      CuryRate = this.CuryRate,
      CuryRateTypeID = this.CuryRateTypeID,
      DisplayCuryID = this.DisplayCuryID,
      IsReadOnly = this.IsReadOnly,
      ModuleCode = this.ModuleCode,
      RecipRate = this.RecipRate,
      SampleCuryRate = this.SampleCuryRate,
      SampleRecipRate = this.SampleRecipRate,
      tstamp = this.tstamp
    };
  }

  public static CurrencyInfo GetEX(PX.Objects.CM.CurrencyInfo info)
  {
    return new CurrencyInfo()
    {
      BaseCalc = info.BaseCalc,
      BaseCuryID = info.BaseCuryID,
      BasePrecision = info.BasePrecision,
      CuryEffDate = info.CuryEffDate,
      CuryID = info.CuryID,
      CuryInfoID = info.CuryInfoID,
      CuryMultDiv = info.CuryMultDiv,
      CuryPrecision = info.CuryPrecision,
      CuryRate = info.CuryRate,
      CuryRateTypeID = info.CuryRateTypeID,
      DisplayCuryID = info.DisplayCuryID,
      IsReadOnly = info.IsReadOnly,
      ModuleCode = info.ModuleCode,
      RecipRate = info.RecipRate,
      SampleCuryRate = info.SampleCuryRate,
      SampleRecipRate = info.SampleRecipRate,
      tstamp = info.tstamp
    };
  }

  private Decimal GetRate()
  {
    Decimal? nullable = this.CuryRate.HasValue ? this.CuryRate : throw new PXRateNotFoundException();
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return 1M;
    nullable = this.CuryRate;
    return nullable.Value;
  }

  public Decimal RoundCury(Decimal val)
  {
    return Math.Round(val, (int) this.CuryPrecision.Value, MidpointRounding.AwayFromZero);
  }

  public Decimal RoundBase(Decimal val)
  {
    return Math.Round(val, (int) this.BasePrecision.Value, MidpointRounding.AwayFromZero);
  }

  public Decimal CuryConvBaseRaw(Decimal curyval)
  {
    if (curyval == 0M)
      return 0M;
    Decimal rate = this.GetRate();
    return !(this.CuryMultDiv != "D") ? curyval / rate : curyval * rate;
  }

  public Decimal CuryConvBase(Decimal curyval, int? precision)
  {
    Decimal d = this.CuryConvBaseRaw(curyval);
    return !precision.HasValue ? d : Math.Round(d, precision.Value, MidpointRounding.AwayFromZero);
  }

  public Decimal CuryConvBase(Decimal curyval)
  {
    Decimal curyval1 = curyval;
    short? basePrecision = this.BasePrecision;
    int? precision = basePrecision.HasValue ? new int?((int) basePrecision.GetValueOrDefault()) : new int?();
    return this.CuryConvBase(curyval1, precision);
  }

  public Decimal CuryConvCuryRaw(Decimal baseval)
  {
    Decimal rate = this.GetRate();
    return !(this.CuryMultDiv == "D") ? baseval / rate : baseval * rate;
  }

  public Decimal CuryConvCury(Decimal baseval, int? precision)
  {
    Decimal d = this.CuryConvCuryRaw(baseval);
    return !precision.HasValue ? d : Math.Round(d, precision.Value, MidpointRounding.AwayFromZero);
  }

  public Decimal CuryConvCury(Decimal baseval)
  {
    Decimal baseval1 = baseval;
    short? curyPrecision = this.CuryPrecision;
    int? precision = curyPrecision.HasValue ? new int?((int) curyPrecision.GetValueOrDefault()) : new int?();
    return this.CuryConvCury(baseval1, precision);
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CurrencyInfo.curyInfoID>
  {
  }

  public abstract class moduleCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyInfo.moduleCode>
  {
  }

  public abstract class baseCalc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CurrencyInfo.baseCalc>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyInfo.baseCuryID>
  {
  }

  protected class CuryIDSelectorAttribute : PXCustomSelectorAttribute
  {
    public CuryIDSelectorAttribute()
      : base(typeof (Currency.curyID))
    {
    }

    public virtual PXEventSubscriberAttribute Clone(PXAttributeLevel attributeLevel)
    {
      return attributeLevel == 2 ? (PXEventSubscriberAttribute) this : ((PXEventSubscriberAttribute) this).Clone(attributeLevel);
    }

    public virtual IEnumerable GetRecords()
    {
      CurrencyInfo.CuryIDSelectorAttribute selectorAttribute = this;
      foreach (IPXCurrency currency in ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()(selectorAttribute._Graph).Currencies())
      {
        if (currency is Currency)
          yield return (object) currency;
        else
          yield return (object) new Currency()
          {
            CuryID = currency.CuryID,
            Description = currency.Description
          };
      }
    }
  }

  public class CuryIDStringAttribute : PXDBStringAttribute
  {
    protected Dictionary<long, string> _Matches;

    public static Dictionary<long, string> GetMatchesDictionary(PXCache sender)
    {
      foreach (PXEventSubscriberAttribute subscriberAttribute in sender.Graph.Caches[typeof (CurrencyInfo)].GetAttributesReadonly<CurrencyInfo.curyID>())
      {
        if (subscriberAttribute is CurrencyInfo.CuryIDStringAttribute)
          return ((CurrencyInfo.CuryIDStringAttribute) subscriberAttribute)._Matches;
      }
      return (Dictionary<long, string>) null;
    }

    public CuryIDStringAttribute()
      : base(5)
    {
      this.IsUnicode = true;
    }

    public virtual PXEventSubscriberAttribute Clone(PXAttributeLevel attributeLevel)
    {
      return attributeLevel == 2 ? (PXEventSubscriberAttribute) this : ((PXEventSubscriberAttribute) this).Clone(attributeLevel);
    }

    public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
    {
      base.RowSelecting(sender, e);
      if (this._Matches == null)
        return;
      CurrencyInfo row = (CurrencyInfo) e.Row;
      if (row == null)
        return;
      long? curyInfoId = row.CuryInfoID;
      if (!curyInfoId.HasValue || string.IsNullOrEmpty(row.CuryID))
        return;
      Dictionary<long, string> matches = this._Matches;
      curyInfoId = row.CuryInfoID;
      long key = curyInfoId.Value;
      string curyId = row.CuryID;
      matches[key] = curyId;
    }

    public virtual void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      if (sender.Graph.FindImplementation<IPXCurrencyHelper>() != null)
        return;
      this._Matches = new Dictionary<long, string>();
    }
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyInfo.curyID>
  {
  }

  public abstract class displayCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyInfo.displayCuryID>
  {
  }

  protected class CuryRateTypeIDSelectorAttribute : PXCustomSelectorAttribute
  {
    public CuryRateTypeIDSelectorAttribute()
      : base(typeof (CurrencyRateType.curyRateTypeID))
    {
    }

    public virtual PXEventSubscriberAttribute Clone(PXAttributeLevel attributeLevel)
    {
      return attributeLevel == 2 ? (PXEventSubscriberAttribute) this : ((PXEventSubscriberAttribute) this).Clone(attributeLevel);
    }

    public virtual IEnumerable GetRecords()
    {
      CurrencyInfo.CuryRateTypeIDSelectorAttribute selectorAttribute = this;
      foreach (IPXCurrencyRateType currencyRateType in ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()(selectorAttribute._Graph).CurrencyRateTypes())
      {
        if (currencyRateType is CurrencyRateType)
          yield return (object) currencyRateType;
        else
          yield return (object) new CurrencyRateType()
          {
            CuryRateTypeID = currencyRateType.CuryRateTypeID,
            Descr = currencyRateType.Descr
          };
      }
    }
  }

  public abstract class curyRateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyInfo.curyRateTypeID>
  {
  }

  public abstract class curyEffDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CurrencyInfo.curyEffDate>
  {
  }

  public abstract class curyMultDiv : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyInfo.curyMultDiv>
  {
  }

  public abstract class curyRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CurrencyInfo.curyRate>
  {
  }

  public abstract class recipRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CurrencyInfo.recipRate>
  {
  }

  public abstract class sampleCuryRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CurrencyInfo.sampleCuryRate>
  {
  }

  public abstract class sampleRecipRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CurrencyInfo.sampleRecipRate>
  {
  }

  public abstract class curyPrecision : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CurrencyInfo.curyPrecision>
  {
  }

  public abstract class basePrecision : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CurrencyInfo.basePrecision>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CurrencyInfo.Tstamp>
  {
  }
}
