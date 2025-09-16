// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMRate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// A billing rate.
/// Billing rates are used in the formulas of the billing and allocation rules for calculating
/// the amount and quantity during the billing of a project or transaction allocation.
/// The records of this type are created and edited through the Rate Tables (PM206000) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.RateMaint" /> graph).
/// </summary>
[PXCacheName("Rate")]
[PXPrimaryGraph(typeof (RateMaint))]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMRate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _RateDefinitionID;
  protected 
  #nullable disable
  string _RateCodeID;
  protected int? _LineNbr;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected Decimal? _Rate;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>The identifier of the rate definition.</summary>
  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (PMRateSequence.rateDefinitionID))]
  [PXParent(typeof (Select<PMRateSequence, Where<PMRateSequence.rateDefinitionID, Equal<Current<PMRate.rateDefinitionID>>, And<PMRateSequence.rateCodeID, Equal<Current<PMRate.rateCodeID>>>>>))]
  public virtual int? RateDefinitionID
  {
    get => this._RateDefinitionID;
    set => this._RateDefinitionID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMRateSequence">rate code</see> to which rate belongs.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMRateSequence.rateCodeID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (PMRateSequence.rateCodeID))]
  public virtual string RateCodeID
  {
    get => this._RateCodeID;
    set => this._RateCodeID = value;
  }

  /// <summary>The line number of the rate.</summary>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (PMRateSequence.lineCntr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  /// <summary>The start date of the range when the rate is valid.</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Start Date")]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  /// <summary>The end date of the range when the rate is valid.</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "End Date")]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  /// <summary>The value of the rate.</summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Rate")]
  public virtual Decimal? Rate
  {
    get => this._Rate;
    set => this._Rate = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

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

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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

  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRate.RateDefinitionID" />
  public abstract class rateDefinitionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRate.rateDefinitionID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRate.RateCodeID" />
  public abstract class rateCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRate.rateCodeID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRate.LineNbr" />
  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRate.lineNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRate.StartDate" />
  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMRate.startDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRate.EndDate" />
  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMRate.endDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRate.Rate" />
  public abstract class rate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMRate.rate>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMRate.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMRate.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMRate.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMRate.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMRate.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMRate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMRate.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMRate.lastModifiedDateTime>
  {
  }
}
