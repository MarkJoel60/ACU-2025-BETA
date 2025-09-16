// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinYearSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// The financial year setup record that is used to define the template for the actual financial years.
/// The system stores one record of this type per company. It is edited on the Financial Year (GL101000) form.
/// </summary>
[PXPrimaryGraph(typeof (FiscalYearSetupMaint))]
[PXCacheName("Financial Year")]
[Serializable]
public class FinYearSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IYearSetup
{
  protected 
  #nullable disable
  string _FirstFinYear;
  protected DateTime? _BegFinYear;
  protected short? _FinPeriods;
  protected short? _PeriodLength;
  protected string _PeriodType;
  protected bool? _UserDefined;
  protected DateTime? _PeriodsStartDate;
  protected bool _HasAdjustmentPeriod;
  protected string _EndYearCalcMethod;
  protected int? _EndYearDayOfWeek;
  protected int? _YearLastDayOfWeek;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected bool? _BelongsToNextYear;

  /// <summary>
  /// First financial year, for which data is stored in the system.
  /// </summary>
  [PXDBString(4, IsFixed = true)]
  [PXDefault("")]
  [PXUIField(DisplayName = "First Financial Year", Enabled = false)]
  public virtual string FirstFinYear
  {
    get => this._FirstFinYear;
    set => this._FirstFinYear = value;
  }

  /// <summary>The start date of the financial year.</summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Financial Year Starts On")]
  public virtual DateTime? BegFinYear
  {
    get => this._BegFinYear;
    set => this._BegFinYear = value;
  }

  /// <summary>The number of financial periods in the year.</summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Number of Financial Periods ")]
  public virtual short? FinPeriods
  {
    get => this._FinPeriods;
    set => this._FinPeriods = value;
  }

  /// <summary>
  /// Obsolete field.
  /// The length of <see cref="!:PX.Objects.GL.Obsolete.FinPeriod">periods</see> of the year in days.
  /// </summary>
  [PXDBShort(MinValue = 5, MaxValue = 366)]
  [PXDefault]
  [PXUIField(DisplayName = "Length of Financial Period (days)", Visible = true, Enabled = false)]
  public virtual short? PeriodLength
  {
    get => this._PeriodLength;
    set => this._PeriodLength = value;
  }

  /// <summary>The type of financial periods that make up the year.</summary>
  /// <value>
  /// Allowed values are:
  /// "MO" - Month,
  /// "BM" - Two Months,
  /// "QR" - Quarter,
  /// "WK" - Week,
  /// "BW" - Two Weeks,
  /// "FW" - Four Weeks,
  /// "DC" - Decade,
  /// "FF" - 4-4-5 Weeks,
  /// "FI" - 4-5-4 Weeks,
  /// "IF" - 5-4-4 Weeks,
  /// "CL" - Customer Periods Length,
  /// "CN" - Custom Number of Periods.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXDefault("MO")]
  [PXUIField(DisplayName = "Period Type")]
  [FinPeriodType.List]
  public virtual string PeriodType
  {
    get => this._PeriodType;
    set => this._PeriodType = value;
  }

  /// <summary>
  /// The read-only field indicating whether the <see cref="!:PX.Objects.GL.Obsolete.FinPeriod">periods</see> of the year can be modified by user.
  /// </summary>
  /// <value>
  /// Depends on the value of the <see cref="P:PX.Objects.GL.FinYearSetup.PeriodType" /> field - returns <c>true</c> if
  /// the <see cref="P:PX.Objects.GL.FinYearSetup.PeriodType" /> is <c>"CN"</c> (Custom number of periods).
  /// </value>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "User-Defined Periods")]
  public virtual bool? UserDefined
  {
    get => new bool?(this.PeriodType == "CN");
    set
    {
    }
  }

  /// <summary>The start date of the first period of the year.</summary>
  [PXDBDate]
  [PXDefault(typeof (FinYearSetup.begFinYear))]
  [PXUIField(DisplayName = "First Period Start Date", Visible = true)]
  public virtual DateTime? PeriodsStartDate
  {
    get => this._PeriodsStartDate;
    set => this._PeriodsStartDate = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the system generates an additional period for posting adjustments.
  /// The adjustment period has the same start and end date and is the last period of the year.
  /// No date in the year corresponds to the adjustment period, so it can be selected for a particular document or batch only manually.
  /// See also the <see cref="!:PX.Objects.GL.Obsolete.FinPeriod.IsAdjustment" /> field.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Has Adjustment Period")]
  public bool? HasAdjustmentPeriod
  {
    get => new bool?(this._HasAdjustmentPeriod);
    set
    {
      if (!value.HasValue)
        return;
      this._HasAdjustmentPeriod = value.Value;
    }
  }

  /// <summary>
  /// The method used to determine the end date of a year with week-long periods.
  /// </summary>
  /// <value>
  /// Available values are:
  /// "CA" - Last day of the financial year,
  /// "LD" - Include last <see cref="P:PX.Objects.GL.FinYearSetup.YearLastDayOfWeek">selected day of week</see> of the year,
  /// "ND" - Include <see cref="P:PX.Objects.GL.FinYearSetup.YearLastDayOfWeek">selected day of week</see>, nearest tot the end of the year.
  /// For more information on these options see the documentation for the Financial Year (GL101000) form.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXDefault("CA")]
  [PXUIField(DisplayName = "Year End Calculation Method")]
  [EndYearMethod.List]
  public virtual string EndYearCalcMethod
  {
    get => this._EndYearCalcMethod;
    set => this._EndYearCalcMethod = value;
  }

  /// <summary>
  /// The day of the week when period starts.
  /// Relevant only for the <see cref="P:PX.Objects.GL.FinYearSetup.PeriodType">period types</see> based on weeks.
  /// </summary>
  /// <value>
  /// Allowed values are:
  /// 1 - "Sunday",
  /// 2 - "Monday",
  /// 3 - "Tuesday",
  /// 4 - "Wednesday",
  /// 5 - "Thursday",
  /// 6 - "Friday",
  /// 7 - "Saturday".
  /// </value>
  [PXDBInt]
  [PXDefault(7)]
  [PXUIField(DisplayName = "Periods Start Day of Week", Enabled = true)]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7}, new string[] {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"})]
  public virtual int? EndYearDayOfWeek
  {
    get => this._EndYearDayOfWeek;
    set => this._EndYearDayOfWeek = value;
  }

  /// <summary>
  /// The day of the week of the last day of the financial year.
  /// Relevant ony for the <see cref="P:PX.Objects.GL.FinYearSetup.PeriodType">period types</see> based on weeks and
  /// if the <see cref="P:PX.Objects.GL.FinYearSetup.EndYearCalcMethod" /> is <b>not</b> <c>"CA"</c> - Last day of the financial year.
  /// </summary>
  [PXInt]
  [PXUIField(DisplayName = "Day of Week", Enabled = false)]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7}, new string[] {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"})]
  public virtual int? YearLastDayOfWeek
  {
    get
    {
      int? endYearDayOfWeek = this._EndYearDayOfWeek;
      int? nullable = endYearDayOfWeek.HasValue ? new int?(endYearDayOfWeek.GetValueOrDefault() - 1) : new int?();
      int num = 0;
      if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
      {
        nullable = this._EndYearDayOfWeek;
        return !nullable.HasValue ? new int?() : new int?(nullable.GetValueOrDefault() - 1 + 7);
      }
      nullable = this._EndYearDayOfWeek;
      return !nullable.HasValue ? new int?() : new int?(nullable.GetValueOrDefault() - 1);
    }
    set => this._YearLastDayOfWeek = value;
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

  /// <summary>
  /// When set to <c>true</c>, indicates that the system must set the <see cref="P:PX.Objects.GL.FinYearSetup.FirstFinYear">financial year</see> to the one,
  /// following the year of the <see cref="P:PX.Objects.GL.FinYearSetup.BegFinYear">selected start date of the year</see>.
  /// </summary>
  /// <value>
  /// For example, if the <see cref="P:PX.Objects.GL.FinYearSetup.BegFinYear">selected start date</see> is 10/28/2014 and this option is set to <c>true</c>,
  /// the <see cref="P:PX.Objects.GL.FinYearSetup.FirstFinYear" /> will be <c>2015</c>.
  /// If this option is set to <c>false</c>, <see cref="P:PX.Objects.GL.FinYearSetup.FirstFinYear" /> for the same date will be <c>2014</c>.
  /// </value>
  [PXUIField(DisplayName = "Belongs to Next Year")]
  [PXBool]
  public bool? BelongsToNextYear
  {
    get => this._BelongsToNextYear;
    set
    {
      if (!value.HasValue)
        return;
      this._BelongsToNextYear = new bool?(value.Value);
    }
  }

  /// <summary>
  /// When <c>true</c>, indicates that the length of the <see cref="!:PX.Objects.GL.Obsolete.FinPeriod">periods</see> of the year in days is fixed.
  /// Read-only field.
  /// </summary>
  /// <value>
  /// The value of this field is determined by the <see cref="P:PX.Objects.GL.FinYearSetup.PeriodType" /> field.
  /// </value>
  public bool IsFixedLengthPeriod => FiscalPeriodSetupCreator.IsFixedLengthPeriod(this.FPType);

  /// <summary>
  /// Internal type of <see cref="!:PX.Objects.GL.Obsolete.FinPeriod">periods</see> of the year.
  /// Read-only field.
  /// </summary>
  /// <value>
  /// The value of this field is determined by the <see cref="P:PX.Objects.GL.FinYearSetup.PeriodType" /> field.
  /// </value>
  public FiscalPeriodSetupCreator.FPType FPType => FinPeriodType.GetFPType(this.PeriodType);

  public abstract class firstFinYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FinYearSetup.firstFinYear>
  {
  }

  public abstract class begFinYear : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FinYearSetup.begFinYear>
  {
  }

  public abstract class finPeriods : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FinYearSetup.finPeriods>
  {
  }

  public abstract class periodLength : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FinYearSetup.periodLength>
  {
  }

  public abstract class periodType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FinYearSetup.periodType>
  {
  }

  public abstract class userDefined : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinYearSetup.userDefined>
  {
  }

  public abstract class periodsStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FinYearSetup.periodsStartDate>
  {
  }

  public abstract class hasAdjustmentPeriod : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FinYearSetup.hasAdjustmentPeriod>
  {
  }

  public abstract class endYearCalcMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FinYearSetup.endYearCalcMethod>
  {
  }

  public abstract class endYearDayOfWeek : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FinYearSetup.endYearDayOfWeek>
  {
  }

  public abstract class yearLastDayOfWeek : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FinYearSetup.yearLastDayOfWeek>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FinYearSetup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FinYearSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FinYearSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FinYearSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FinYearSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FinYearSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FinYearSetup.lastModifiedDateTime>
  {
  }

  public abstract class belongsToNextYear : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FinYearSetup.belongsToNextYear>
  {
  }
}
