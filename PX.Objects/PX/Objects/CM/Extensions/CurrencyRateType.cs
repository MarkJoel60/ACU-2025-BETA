// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.CurrencyRateType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CM.Extensions;

/// <summary>
/// Represents the types of currency rates, which can be used to distinguish rates by their sources and by the accounting routines the rates are used for.
/// The records of this type are edited on the Currency Rate Types (CM201000) form (corresponds to the <see cref="T:PX.Objects.CM.CurrencyRateTypeMaint" /> graph).
/// </summary>
[PXPrimaryGraph(new Type[] {typeof (CurrencyRateTypeMaint)}, new Type[] {typeof (Select<CurrencyRateType, Where<CurrencyRateType.curyRateTypeID, Equal<Current<CurrencyRateType.curyRateTypeID>>>>)})]
[PXCacheName("Currency Rate Type")]
[Serializable]
public class CurrencyRateType : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IPXCurrencyRateType
{
  /// <summary>
  /// Key field.
  /// The unique identifier of the rate type.
  /// </summary>
  [PXDBString(6, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXReferentialIntegrityCheck]
  [PXUIField]
  public virtual 
  #nullable disable
  string CuryRateTypeID { get; set; }

  /// <summary>The description of the rate type.</summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr { get; set; }

  /// <summary>
  /// The number of days, during which the rate of this type is considered current.
  /// </summary>
  /// <value>
  /// Defaults to <c>0</c>, meaning that the rate remains effective until the date when a new rate is specified.
  /// </value>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Days Effective")]
  public virtual short? RateEffDays { get; set; }

  /// <summary>
  /// Identifies that this currency rate should be automatically refreshed online.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Refresh Online")]
  public virtual bool? RefreshOnline { get; set; }

  /// <summary>
  /// Adjustment percentage to apply to the rate retrieved online.
  /// </summary>
  /// <value>
  /// Defaults to <c>0.00</c>, meaning that the rate actual exchange rate will be used without any adjustment.
  /// </value>
  [PXDBDecimal(2)]
  [PXUIField(DisplayName = "Online Rate Adjustment (%)")]
  public virtual Decimal? OnlineRateAdjustment { get; set; }

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

  public abstract class curyRateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyRateType.curyRateTypeID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyRateType.descr>
  {
  }

  public abstract class rateEffDays : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CurrencyRateType.rateEffDays>
  {
  }

  public abstract class refreshOnline : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CurrencyRateType.refreshOnline>
  {
  }

  public abstract class onlineRateAdjustment : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CurrencyRateType.onlineRateAdjustment>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CurrencyRateType.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CurrencyRateType.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyRateType.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CurrencyRateType.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CurrencyRateType.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyRateType.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CurrencyRateType.lastModifiedDateTime>
  {
  }
}
