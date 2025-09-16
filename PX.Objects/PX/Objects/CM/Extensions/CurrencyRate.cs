// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.CurrencyRate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CM.Extensions;

/// <summary>
/// Represents an exchange rate of a particular type for a pair of currencies on a particular date.
/// The records of this type are added and edited on the Currency Rates (CM301000) form
/// (corresponds to the <see cref="T:PX.Objects.CM.CuryRateMaint" /> graph).
/// </summary>
[PXCacheName("Currency Rate")]
[Serializable]
public class CurrencyRate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IPXCurrencyRate
{
  /// <summary>
  /// Key field. Database identity.
  /// The unique identifier of the currency rate record.
  /// </summary>
  [PXDBIdentity(IsKey = true)]
  [PXUIField]
  public virtual int? CuryRateID { get; set; }

  /// <summary>
  /// The identifier of the source <see cref="T:PX.Objects.CM.Extensions.Currency" />.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.Extensions.Currency.CuryID" /> field.
  /// </value>
  [PXDBString(5, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.curyID, NotEqual<Current<CuryRateFilter.toCurrency>>, And<CurrencyList.isActive, Equal<True>>>>))]
  public virtual 
  #nullable disable
  string FromCuryID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyRateType">type of the rate</see>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.Extensions.CurrencyRateType.CuryRateTypeID" /> field.
  /// </value>
  [PXDBString(6, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (CurrencyRateType.curyRateTypeID), DescriptionField = typeof (CurrencyRateType.descr))]
  public virtual string CuryRateType { get; set; }

  /// <summary>
  /// The date, starting from which the exchange rate is considered current.
  /// </summary>
  [PXDBDate]
  [PXDefault(typeof (CuryRateFilter.effDate))]
  [PXUIField]
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
  [PXUIField]
  [PXStringList("M;Multiply,D;Divide")]
  public virtual string CuryMultDiv { get; set; }

  /// <summary>
  /// The currency rate. For the purposes of conversion the value of this field is used
  /// together with the operation selected in the <see cref="P:PX.Objects.CM.Extensions.CurrencyRate.CuryMultDiv" /> field.
  /// </summary>
  /// <value>
  /// Defaults to <c>1.0</c>.
  /// </value>
  [PXDBDecimal(8, MinValue = 0.0)]
  [PXDefault]
  [PXUIField]
  public virtual Decimal? CuryRate { get; set; }

  /// <summary>
  /// The inverse of the <see cref="P:PX.Objects.CM.Extensions.CurrencyRate.CuryRate">exchange rate</see>, which is calculated automatically.
  /// </summary>
  [PXDBDecimal(8)]
  [PXUIField]
  public virtual Decimal? RateReciprocal { get; set; }

  /// <summary>
  /// The identifier of the destination <see cref="T:PX.Objects.CM.Extensions.Currency" />.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="!:Company.BaseCuryID">base currency of the company</see> through
  /// the <see cref="P:PX.Objects.CM.Extensions.CuryRateFilter.ToCurrency" /> field.
  /// Corresponds to the <see cref="P:PX.Objects.CM.Extensions.Currency.CuryID" /> field.
  /// </value>
  [PXDBString(5, IsUnicode = true)]
  [PXUIField]
  [PXDefault(typeof (CuryRateFilter.toCurrency))]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.isActive, Equal<True>>>))]
  public virtual string ToCuryID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public abstract class curyRateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CurrencyRate.curyRateID>
  {
  }

  public abstract class fromCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyRate.fromCuryID>
  {
  }

  public abstract class curyRateType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyRate.curyRateType>
  {
  }

  public abstract class curyEffDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CurrencyRate.curyEffDate>
  {
  }

  public abstract class curyMultDiv : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyRate.curyMultDiv>
  {
  }

  public abstract class curyRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CurrencyRate.curyRate>
  {
  }

  public abstract class rateReciprocal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CurrencyRate.rateReciprocal>
  {
  }

  public abstract class toCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyRate.toCuryID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CurrencyRate.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CurrencyRate.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyRate.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CurrencyRate.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CurrencyRate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyRate.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CurrencyRate.lastModifiedDateTime>
  {
  }
}
