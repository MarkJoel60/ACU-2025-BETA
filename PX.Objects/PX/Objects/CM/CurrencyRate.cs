// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CurrencyRate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CM;

/// <summary>
/// Represents an exchange rate of a particular type for a pair of currencies on a particular date.
/// The records of this type are added and edited on the Currency Rates (CM301000) form
/// (corresponds to the <see cref="T:PX.Objects.CM.CuryRateMaint" /> graph).
/// </summary>
[PXCacheName("Currency Rate")]
[Serializable]
public class CurrencyRate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _CuryRateID;
  protected 
  #nullable disable
  string _FromCuryID;
  protected string _CuryRateType;
  protected DateTime? _CuryEffDate;
  protected string _CuryMultDiv;
  protected Decimal? _CuryRate;
  protected Decimal? _RateReciprocal;
  protected string _ToCuryID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected bool? _Selected = new bool?(false);

  /// <summary>
  /// Key field. Database identity.
  /// The unique identifier of the currency rate record.
  /// </summary>
  [PXDBIdentity(IsKey = true)]
  [PXUIField]
  public virtual int? CuryRateID
  {
    get => this._CuryRateID;
    set => this._CuryRateID = value;
  }

  /// <summary>
  /// The identifier of the source <see cref="T:PX.Objects.CM.Currency" />.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.Currency.CuryID" /> field.
  /// </value>
  [PXDBString(5, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.curyID, NotEqual<Current<CuryRateFilter.toCurrency>>, And<CurrencyList.isActive, Equal<True>>>>))]
  public virtual string FromCuryID
  {
    get => this._FromCuryID;
    set => this._FromCuryID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyRateType">type of the rate</see>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.CurrencyRateType.CuryRateTypeID" /> field.
  /// </value>
  [PXDBString(6, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXForeignReference(typeof (Field<CurrencyRate.curyRateType>.IsRelatedTo<CurrencyRateType.curyRateTypeID>))]
  [PXSelector(typeof (CurrencyRateType.curyRateTypeID), DescriptionField = typeof (CurrencyRateType.descr))]
  public virtual string CuryRateType
  {
    get => this._CuryRateType;
    set => this._CuryRateType = value;
  }

  /// <summary>
  /// The date, starting from which the exchange rate is considered current.
  /// </summary>
  [PXDBDate]
  [PXDefault(typeof (CuryRateFilter.effDate))]
  [PXUIField]
  public virtual DateTime? CuryEffDate
  {
    get => this._CuryEffDate;
    set => this._CuryEffDate = value;
  }

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
  public virtual string CuryMultDiv
  {
    get => this._CuryMultDiv;
    set => this._CuryMultDiv = value;
  }

  /// <summary>
  /// The currency rate. For the purposes of conversion the value of this field is used
  /// together with the operation selected in the <see cref="P:PX.Objects.CM.CurrencyRate.CuryMultDiv" /> field.
  /// </summary>
  /// <value>
  /// Defaults to <c>1.0</c>.
  /// </value>
  [PXDBDecimal(8, MinValue = 0.0)]
  [PXDefault]
  [PXUIField]
  public virtual Decimal? CuryRate
  {
    get => this._CuryRate;
    set => this._CuryRate = value;
  }

  /// <summary>
  /// The inverse of the <see cref="P:PX.Objects.CM.CurrencyRate.CuryRate">exchange rate</see>, which is calculated automatically.
  /// </summary>
  [PXDBDecimal(8)]
  [PXUIField]
  public virtual Decimal? RateReciprocal
  {
    get => this._RateReciprocal;
    set => this._RateReciprocal = value;
  }

  /// <summary>
  /// The identifier of the destination <see cref="T:PX.Objects.CM.Currency" />.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="!:Company.BaseCuryID">base currency of the company</see> through
  /// the <see cref="P:PX.Objects.CM.CuryRateFilter.ToCurrency" /> field.
  /// Corresponds to the <see cref="P:PX.Objects.CM.Currency.CuryID" /> field.
  /// </value>
  [PXDBString(5, IsUnicode = true)]
  [PXUIField]
  [PXDefault(typeof (CuryRateFilter.toCurrency))]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.isActive, Equal<True>>>))]
  public virtual string ToCuryID
  {
    get => this._ToCuryID;
    set => this._ToCuryID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  /// <summary>Indicates weather the record is currently selected.</summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  public static implicit operator CurrencyInfo(CurrencyRate item)
  {
    return new CurrencyInfo()
    {
      BaseCuryID = item.ToCuryID,
      CuryRateTypeID = item.CuryRateType,
      CuryEffDate = !item.CuryEffDate.HasValue ? new DateTime?() : new DateTime?(item.CuryEffDate.Value.AddDays(-1.0)),
      CuryID = item.FromCuryID,
      CuryMultDiv = item.CuryMultDiv,
      CuryRate = item.CuryRate,
      RecipRate = item.RateReciprocal
    };
  }

  public class PK : PrimaryKeyOf<CurrencyRate>.By<CurrencyRate.curyRateID>
  {
    public static CurrencyRate Find(PXGraph graph, long? curyRateID, PKFindOptions options = 0)
    {
      return (CurrencyRate) PrimaryKeyOf<CurrencyRate>.By<CurrencyRate.curyRateID>.FindBy(graph, (object) curyRateID, options);
    }
  }

  public static class FK
  {
    public class FromCurrency : 
      PrimaryKeyOf<Currency>.By<Currency.curyID>.ForeignKeyOf<CurrencyRate>.By<CurrencyRate.fromCuryID>
    {
    }

    public class ToCurrency : 
      PrimaryKeyOf<Currency>.By<Currency.curyID>.ForeignKeyOf<CurrencyRate>.By<CurrencyRate.toCuryID>
    {
    }

    public class CurrencyRateType : 
      PrimaryKeyOf<CurrencyRateType>.By<CurrencyRateType.curyRateTypeID>.ForeignKeyOf<CurrencyRate>.By<CurrencyRate.curyRateType>
    {
    }
  }

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

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CurrencyRate.noteID>
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

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CurrencyRate.selected>
  {
  }
}
