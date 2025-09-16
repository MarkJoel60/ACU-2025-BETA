// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookYearSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXCacheName("Book Calendar")]
[Serializable]
public class FABookYearSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IYearSetup
{
  protected int? _BookID;
  protected 
  #nullable disable
  string _FirstFinYear;
  protected DateTime? _BegFinYear;
  protected short? _FinPeriods;
  protected bool? _UserDefined;
  protected string _PeriodType;
  protected short? _PeriodLength;
  protected DateTime? _PeriodsStartDate;
  protected bool _HasAdjustmentPeriod;
  protected string _EndYearCalcMethod;
  protected int? _EndYearDayOfWeek;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected bool? _BelongsToNextYear;

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXSelector(typeof (Search<FABook.bookID, Where<FABook.updateGL, Equal<False>>>), SubstituteKey = typeof (FABook.bookCode), DescriptionField = typeof (FABook.description))]
  [PXUIField(DisplayName = "Book")]
  [PXFieldDescription]
  [PXParent(typeof (Select<FABook, Where<FABook.bookID, Equal<Current<FABookYearSetup.bookID>>>>))]
  public virtual int? BookID
  {
    get => this._BookID;
    set => this._BookID = value;
  }

  [PXDBString(4, IsFixed = true)]
  [PXDefault("")]
  [PXUIField(DisplayName = "First Year", Enabled = false)]
  [PXFieldDescription]
  public virtual string FirstFinYear
  {
    get => this._FirstFinYear;
    set => this._FirstFinYear = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Year Starts On")]
  public virtual DateTime? BegFinYear
  {
    get => this._BegFinYear;
    set => this._BegFinYear = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Number of Periods ")]
  public virtual short? FinPeriods
  {
    get => this._FinPeriods;
    set => this._FinPeriods = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "User-Defined Periods")]
  public virtual bool? UserDefined
  {
    [PXDependsOnFields(new Type[] {typeof (FABookYearSetup.periodType)})] get
    {
      return new bool?(this.PeriodType == "CN");
    }
    set
    {
    }
  }

  [PXNote(DescriptionField = typeof (FABookYearSetup.bookID))]
  public virtual Guid? NoteID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("MO")]
  [PXUIField(DisplayName = "Period Type")]
  [FinPeriodType.List]
  public virtual string PeriodType
  {
    get => this._PeriodType;
    set => this._PeriodType = value;
  }

  [PXDBShort(MinValue = 5, MaxValue = 366)]
  [PXDefault]
  [PXUIField(DisplayName = "Length of Period(days)")]
  public virtual short? PeriodLength
  {
    get => this._PeriodLength;
    set => this._PeriodLength = value;
  }

  [PXDBDate]
  [PXDefault(typeof (FABookYearSetup.begFinYear))]
  [PXUIField(DisplayName = "First Period Starts On")]
  public virtual DateTime? PeriodsStartDate
  {
    get => this._PeriodsStartDate;
    set => this._PeriodsStartDate = value;
  }

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

  [PXDBString(2, IsFixed = true)]
  [PXDefault("CA")]
  [PXUIField(DisplayName = "Year End Calculation Method")]
  [EndYearMethod.List]
  public virtual string EndYearCalcMethod
  {
    get => this._EndYearCalcMethod;
    set => this._EndYearCalcMethod = value;
  }

  [PXDBInt]
  [PXDefault(7)]
  [PXUIField(DisplayName = "Day Of Week", Enabled = true)]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7}, new string[] {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"})]
  public virtual int? EndYearDayOfWeek
  {
    get => this._EndYearDayOfWeek;
    set => this._EndYearDayOfWeek = value;
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

  [PXUIField(DisplayName = "Belongs To Next Year")]
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

  public bool IsFixedLengthPeriod
  {
    [PXDependsOnFields(new Type[] {typeof (FABookYearSetup.fPType)})] get
    {
      return FiscalPeriodSetupCreator.IsFixedLengthPeriod(this.FPType);
    }
  }

  public FiscalPeriodSetupCreator.FPType FPType
  {
    [PXDependsOnFields(new Type[] {typeof (FABookYearSetup.periodType)})] get
    {
      return FinPeriodType.GetFPType(this.PeriodType);
    }
  }

  public class PK : PrimaryKeyOf<FABookYearSetup>.By<FABookYearSetup.bookID>
  {
    public static FABookYearSetup Find(PXGraph graph, int? bookID, PKFindOptions options = 0)
    {
      return (FABookYearSetup) PrimaryKeyOf<FABookYearSetup>.By<FABookYearSetup.bookID>.FindBy(graph, (object) bookID, options);
    }
  }

  public static class FK
  {
    public class Book : 
      PrimaryKeyOf<FABook>.By<FABook.bookID>.ForeignKeyOf<FABookYearSetup>.By<FABookYearSetup.bookID>
    {
    }
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookYearSetup.bookID>
  {
  }

  public abstract class firstFinYear : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookYearSetup.firstFinYear>
  {
  }

  public abstract class begFinYear : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookYearSetup.begFinYear>
  {
  }

  public abstract class finPeriods : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FABookYearSetup.finPeriods>
  {
  }

  public abstract class userDefined : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookYearSetup.userDefined>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FABookYearSetup.noteID>
  {
  }

  public abstract class periodType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookYearSetup.periodType>
  {
  }

  public abstract class periodLength : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FABookYearSetup.periodLength>
  {
  }

  public abstract class periodsStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookYearSetup.periodsStartDate>
  {
  }

  public abstract class hasAdjustmentPeriod : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FABookYearSetup.hasAdjustmentPeriod>
  {
  }

  public abstract class endYearCalcMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookYearSetup.endYearCalcMethod>
  {
  }

  public abstract class endYearDayOfWeek : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FABookYearSetup.endYearDayOfWeek>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FABookYearSetup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FABookYearSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookYearSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookYearSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FABookYearSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookYearSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookYearSetup.lastModifiedDateTime>
  {
  }

  public abstract class belongsToNextYear : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FABookYearSetup.belongsToNextYear>
  {
  }

  public abstract class fPType : IBqlField, IBqlOperand
  {
  }
}
