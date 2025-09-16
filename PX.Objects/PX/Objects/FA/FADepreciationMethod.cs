// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FADepreciationMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.FA;

/// <exclude />
[PXPrimaryGraph(new Type[] {typeof (DepreciationMethodMaint), typeof (DepreciationTableMethodMaint)}, new Type[] {typeof (Where<FADepreciationMethod.isTableMethod, Equal<False>>), typeof (Where<FADepreciationMethod.isTableMethod, Equal<True>>)})]
[PXCacheName("Depreciation Method")]
[Serializable]
public class FADepreciationMethod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _MethodID;
  protected 
  #nullable disable
  string _MethodCD;
  protected string _Description;
  protected int? _ParentMethodID;
  protected string _DepreciationMethod;
  protected string _AveragingConvention;
  protected int? _RecoveryPeriod;
  protected Decimal? _TotalPercents;
  protected short? _AveragingConvPeriod;
  protected short? _DepreciationPeriodsInYear;
  protected DateTime? _DepreciationStartDate;
  protected DateTime? _DepreciationStopDate;
  protected int? _BookID;
  protected string _RecordType;
  protected Decimal? _UsefulLife;
  protected bool? _IsTableMethod;
  protected Decimal? _DBMultiPlier;
  protected bool? _SwitchToSL;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity]
  public virtual int? MethodID
  {
    get => this._MethodID;
    set => this._MethodID = value;
  }

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
  [PXSelector(typeof (Search<FADepreciationMethod.methodCD>), DescriptionField = typeof (FADepreciationMethod.description))]
  [PXUIField]
  [PXDefault]
  [PXFieldDescription]
  public virtual string MethodCD
  {
    get => this._MethodCD;
    set => this._MethodCD = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Search<FADepreciationMethod.methodID, Where<FADepreciationMethod.recordType, Equal<FARecordType.classType>>>), DescriptionField = typeof (FADepreciationMethod.description), SubstituteKey = typeof (FADepreciationMethod.methodCD))]
  [PXDefault]
  [PXUIField]
  public virtual int? ParentMethodID
  {
    get => this._ParentMethodID;
    set => this._ParentMethodID = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField]
  [PXDefault("SL")]
  [FADepreciationMethod.depreciationMethod.List]
  public virtual string DepreciationMethod
  {
    get => this._DepreciationMethod;
    set => this._DepreciationMethod = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("FP")]
  [PXUIField]
  [FAAveragingConvention.List]
  [PXFormula(typeof (Switch<Case2<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.australianPrimeCost>>>>>.Or<BqlOperand<FADepreciationMethod.depreciationMethod, IBqlString>.IsEqual<FADepreciationMethod.depreciationMethod.australianDiminishingValue>>>, FAAveragingConvention.fullDay, Case2<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLine>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.newZealandDiminishingValue>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>>>>>.Or<BqlOperand<FADepreciationMethod.depreciationMethod, IBqlString>.IsEqual<FADepreciationMethod.depreciationMethod.newZealandDiminishingValueEvenly>>>>>, FAAveragingConvention.fullPeriod>>, FADepreciationMethod.averagingConvention>))]
  [PXUIEnabled(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.australianPrimeCost>>>>, And<BqlOperand<FADepreciationMethod.depreciationMethod, IBqlString>.IsNotEqual<FADepreciationMethod.depreciationMethod.australianDiminishingValue>>>, And<BqlOperand<FADepreciationMethod.depreciationMethod, IBqlString>.IsNotEqual<FADepreciationMethod.depreciationMethod.newZealandStraightLine>>>, And<BqlOperand<FADepreciationMethod.depreciationMethod, IBqlString>.IsNotEqual<FADepreciationMethod.depreciationMethod.newZealandDiminishingValue>>>, And<BqlOperand<FADepreciationMethod.depreciationMethod, IBqlString>.IsNotEqual<FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>>>>.And<BqlOperand<FADepreciationMethod.depreciationMethod, IBqlString>.IsNotEqual<FADepreciationMethod.depreciationMethod.newZealandDiminishingValueEvenly>>>))]
  public virtual string AveragingConvention
  {
    get => this._AveragingConvention;
    set => this._AveragingConvention = value;
  }

  [PXDBInt]
  [PXUIField]
  public virtual int? RecoveryPeriod
  {
    get => this._RecoveryPeriod;
    set => this._RecoveryPeriod = value;
  }

  [PXDecimal(3)]
  [PXFormula(typeof (Mult<FADepreciationMethod.totalPercents, decimal100>))]
  [PXUIField(DisplayName = "Total Percent", Required = true, Enabled = false)]
  public virtual Decimal? DisplayTotalPercents { get; set; }

  [PXDBDecimal(5)]
  public virtual Decimal? TotalPercents
  {
    get => this._TotalPercents;
    set => this._TotalPercents = value;
  }

  [PXDBShort]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Convention Period", Required = true)]
  public virtual short? AveragingConvPeriod
  {
    get => this._AveragingConvPeriod;
    set => this._AveragingConvPeriod = value;
  }

  [PXUIField(DisplayName = "Depreciation periods in Year")]
  [PXShort(MinValue = 1, MaxValue = 366)]
  public virtual short? DepreciationPeriodsInYear
  {
    get => this._DepreciationPeriodsInYear;
    set => this._DepreciationPeriodsInYear = value;
  }

  [PXUIField(DisplayName = "Depreciation Start Date")]
  [PXDate]
  public virtual DateTime? DepreciationStartDate
  {
    get => this._DepreciationStartDate;
    set => this._DepreciationStartDate = value;
  }

  [PXUIField(DisplayName = "Depreciation Stop Date")]
  [PXDate]
  public virtual DateTime? DepreciationStopDate
  {
    get => this._DepreciationStopDate;
    set => this._DepreciationStopDate = value;
  }

  [PXInt]
  [PXSelector(typeof (FABook.bookID), SubstituteKey = typeof (FABook.bookCode), DescriptionField = typeof (FABook.description))]
  [PXUIField(DisplayName = "Book")]
  public virtual int? BookID
  {
    get => this._BookID;
    set => this._BookID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [PXDefault("A")]
  [FARecordType.MethodList]
  public virtual string RecordType
  {
    get => this._RecordType;
    set => this._RecordType = value;
  }

  [PXDBDecimal(2, MinValue = 0.0)]
  [PXUIField]
  public virtual Decimal? UsefulLife
  {
    get => this._UsefulLife;
    set => this._UsefulLife = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? IsTableMethod
  {
    get => this._IsTableMethod;
    set => this._IsTableMethod = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsPredefined { get; set; }

  [PXString(1, IsFixed = true)]
  [PXUIField]
  [FADepreciationMethod.source.List]
  [PXFormula(typeof (Switch<Case<Where<FADepreciationMethod.isPredefined, Equal<True>>, FADepreciationMethod.source.predefined>, FADepreciationMethod.source.custom>))]
  public virtual string Source { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXFormula(typeof (Switch<Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.sumOfTheYearsDigits>>, True, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.australianPrimeCost>>, False, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.australianDiminishingValue>>, False, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLine>>, False, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.newZealandDiminishingValue>>, False, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>>, False, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.newZealandDiminishingValueEvenly>>, False, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.dutch1>>, False, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.remainingValueByPeriodLength>>, False, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.remainingValue>>, False>>>>>>>>>>, FADepreciationMethod.yearlyAccountancy>))]
  [PXUIEnabled(typeof (Where<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.sumOfTheYearsDigits>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.dutch1>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.australianPrimeCost>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.australianDiminishingValue>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.newZealandStraightLine>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.newZealandDiminishingValue>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.newZealandDiminishingValueEvenly>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.remainingValueByPeriodLength>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.remainingValue>>>>>>>>>>>))]
  [PXUIField]
  public virtual bool? YearlyAccountancy { get; set; }

  [PercentDBDecimal]
  [PXUIField(DisplayName = "DB Multiplier")]
  [PXFormula(typeof (Switch<Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.australianPrimeCost>>, decimal0, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.australianDiminishingValue>>, decimal0, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLine>>, decimal0, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.newZealandDiminishingValue>>, decimal0, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>>, decimal0, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.newZealandDiminishingValueEvenly>>, decimal0, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.dutch2>>, decimal0, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.remainingValueByPeriodLength>>, decimal0, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.remainingValue>>, decimal0>>>>>>>>>, FADepreciationMethod.dBMultiPlier>))]
  [PXUIEnabled(typeof (Where<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.australianPrimeCost>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.australianDiminishingValue>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.newZealandStraightLine>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.newZealandDiminishingValue>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.newZealandDiminishingValueEvenly>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.dutch2>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.remainingValueByPeriodLength>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.remainingValue>>>>>>>>>>))]
  public virtual Decimal? DBMultiPlier
  {
    get => this._DBMultiPlier;
    set => this._DBMultiPlier = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  [PXFormula(typeof (Switch<Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.australianPrimeCost>>, False, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.australianDiminishingValue>>, False, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLine>>, False, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.newZealandDiminishingValue>>, False, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>>, False, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.newZealandDiminishingValueEvenly>>, False, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.dutch2>>, False, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.remainingValueByPeriodLength>>, False, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.remainingValue>>, False>>>>>>>>>, FADepreciationMethod.switchToSL>))]
  [PXUIEnabled(typeof (Where<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.australianPrimeCost>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.australianDiminishingValue>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.newZealandStraightLine>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.newZealandDiminishingValue>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.newZealandDiminishingValueEvenly>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.dutch2>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.remainingValueByPeriodLength>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.remainingValue>>>>>>>>>>))]
  public virtual bool? SwitchToSL
  {
    get => this._SwitchToSL;
    set => this._SwitchToSL = value;
  }

  [PXDBDecimal(4, MinValue = 0.0, MaxValue = 100.0)]
  [PXUIField(DisplayName = "Percent per Year")]
  [PXFormula(typeof (Switch<Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.remainingValueByPeriodLength>>, decimal0, Case<Where<FADepreciationMethod.depreciationMethod, Equal<FADepreciationMethod.depreciationMethod.remainingValue>>, decimal0>>, FADepreciationMethod.percentPerYear>))]
  [PXUIEnabled(typeof (Where<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.remainingValueByPeriodLength>, And<FADepreciationMethod.depreciationMethod, NotEqual<FADepreciationMethod.depreciationMethod.remainingValue>>>))]
  public virtual Decimal? PercentPerYear { get; set; }

  [PXNote(DescriptionField = typeof (FADepreciationMethod.methodCD))]
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

  public bool IsPureStraightLine
  {
    get => !this.IsTableMethod.GetValueOrDefault() && this.DepreciationMethod == "SL";
  }

  public bool IsYearlyAccountancyTableMethod
  {
    get => this.IsTableMethod.GetValueOrDefault() && this.YearlyAccountancy.GetValueOrDefault();
  }

  public bool IsNewZealandMethod
  {
    get
    {
      return this.DepreciationMethod == "ZL" || this.DepreciationMethod == "LE" || this.DepreciationMethod == "ZD" || this.DepreciationMethod == "DE";
    }
  }

  public class PK : PrimaryKeyOf<FADepreciationMethod>.By<FADepreciationMethod.methodID>
  {
    public static FADepreciationMethod Find(PXGraph graph, int? methodID, PKFindOptions options = 0)
    {
      return (FADepreciationMethod) PrimaryKeyOf<FADepreciationMethod>.By<FADepreciationMethod.methodID>.FindBy(graph, (object) methodID, options);
    }
  }

  public class UK : PrimaryKeyOf<FADepreciationMethod>.By<FADepreciationMethod.methodCD>
  {
    public static FADepreciationMethod Find(PXGraph graph, string methodCD, PKFindOptions options = 0)
    {
      return (FADepreciationMethod) PrimaryKeyOf<FADepreciationMethod>.By<FADepreciationMethod.methodCD>.FindBy(graph, (object) methodCD, options);
    }
  }

  public static class FK
  {
    public class Book : 
      PrimaryKeyOf<FABook>.By<FABook.bookID>.ForeignKeyOf<FADepreciationMethod>.By<FADepreciationMethod.bookID>
    {
    }

    public class ClassMethod : 
      PrimaryKeyOf<FADepreciationMethod>.By<FADepreciationMethod.methodID>.ForeignKeyOf<FADepreciationMethod>.By<FADepreciationMethod.parentMethodID>
    {
    }
  }

  public abstract class methodID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FADepreciationMethod.methodID>
  {
  }

  public abstract class methodCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADepreciationMethod.methodCD>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADepreciationMethod.description>
  {
  }

  public abstract class parentMethodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FADepreciationMethod.parentMethodID>
  {
  }

  public abstract class depreciationMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADepreciationMethod.depreciationMethod>
  {
    public const string StraightLine = "SL";
    public const string DecliningBalance = "DB";
    public const string SumOfTheYearsDigits = "YD";
    public const string RemainingValue = "RV";
    public const string Dutch1 = "N1";
    public const string Dutch2 = "N2";
    public const string RemainingValueByPeriodLength = "RD";
    public const string DecliningBalanceByPeriodLength = "DP";
    public const string AustralianPrimeCost = "PC";
    public const string AustralianDiminishingValue = "DV";
    public const string NewZealandStraightLine = "ZL";
    public const string NewZealandDiminishingValue = "ZD";
    public const string NewZealandStraightLineEvenly = "LE";
    public const string NewZealandDiminishingValueEvenly = "DE";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[14]
        {
          "SL",
          "DB",
          "YD",
          "RV",
          "N1",
          "N2",
          "RD",
          "DP",
          "PC",
          "DV",
          "ZL",
          "ZD",
          "LE",
          "DE"
        }, new string[14]
        {
          "Straight-Line",
          "Declining-Balance",
          "Sum-of-the-Years’-Digits",
          "Remaining Value",
          "Dutch Method 1",
          "Dutch Method 2",
          "Remaining Value by Days in Period",
          "Declining Balance by Days in Period",
          "Australian Prime Cost",
          "Australian Diminishing Value",
          "New Zealand Straight-Line",
          "New Zealand Diminishing Value",
          "New Zealand Straight-Line Evenly by Periods",
          "New Zealand Diminishing Value Evenly by Periods"
        })
      {
      }
    }

    public class straightLine : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADepreciationMethod.depreciationMethod.straightLine>
    {
      public straightLine()
        : base("SL")
      {
      }
    }

    public class decliningBalance : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADepreciationMethod.depreciationMethod.decliningBalance>
    {
      public decliningBalance()
        : base("DB")
      {
      }
    }

    public class sumOfTheYearsDigits : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADepreciationMethod.depreciationMethod.sumOfTheYearsDigits>
    {
      public sumOfTheYearsDigits()
        : base("YD")
      {
      }
    }

    public class remainingValue : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADepreciationMethod.depreciationMethod.remainingValue>
    {
      public remainingValue()
        : base("RV")
      {
      }
    }

    public class dutch1 : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADepreciationMethod.depreciationMethod.dutch1>
    {
      public dutch1()
        : base("N1")
      {
      }
    }

    public class dutch2 : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADepreciationMethod.depreciationMethod.dutch2>
    {
      public dutch2()
        : base("N2")
      {
      }
    }

    public class remainingValueByPeriodLength : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADepreciationMethod.depreciationMethod.remainingValueByPeriodLength>
    {
      public remainingValueByPeriodLength()
        : base("RD")
      {
      }
    }

    public class decliningBalanceByPeriodLength : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADepreciationMethod.depreciationMethod.decliningBalanceByPeriodLength>
    {
      public decliningBalanceByPeriodLength()
        : base("DP")
      {
      }
    }

    public class australianPrimeCost : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADepreciationMethod.depreciationMethod.australianPrimeCost>
    {
      public australianPrimeCost()
        : base("PC")
      {
      }
    }

    public class australianDiminishingValue : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADepreciationMethod.depreciationMethod.australianDiminishingValue>
    {
      public australianDiminishingValue()
        : base("DV")
      {
      }
    }

    public class newZealandStraightLine : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADepreciationMethod.depreciationMethod.newZealandStraightLine>
    {
      public newZealandStraightLine()
        : base("ZL")
      {
      }
    }

    public class newZealandDiminishingValue : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADepreciationMethod.depreciationMethod.newZealandDiminishingValue>
    {
      public newZealandDiminishingValue()
        : base("ZD")
      {
      }
    }

    public class newZealandStraightLineEvenly : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>
    {
      public newZealandStraightLineEvenly()
        : base("LE")
      {
      }
    }

    public class newZealandDiminishingValueEvenly : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADepreciationMethod.depreciationMethod.newZealandDiminishingValueEvenly>
    {
      public newZealandDiminishingValueEvenly()
        : base("DE")
      {
      }
    }
  }

  public abstract class averagingConvention : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADepreciationMethod.averagingConvention>
  {
  }

  public abstract class recoveryPeriod : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FADepreciationMethod.recoveryPeriod>
  {
  }

  public abstract class displayTotalPercents : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADepreciationMethod.displayTotalPercents>
  {
  }

  public abstract class totalPercents : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADepreciationMethod.totalPercents>
  {
  }

  public abstract class averagingConvPeriod : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    FADepreciationMethod.averagingConvPeriod>
  {
  }

  public abstract class depreciationPeriodsInYear : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    FADepreciationMethod.depreciationPeriodsInYear>
  {
  }

  public abstract class depreciationStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADepreciationMethod.depreciationStartDate>
  {
  }

  public abstract class depreciationStopDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADepreciationMethod.depreciationStopDate>
  {
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FADepreciationMethod.bookID>
  {
  }

  public abstract class recordType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADepreciationMethod.recordType>
  {
  }

  public abstract class usefulLife : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADepreciationMethod.usefulLife>
  {
  }

  public abstract class isTableMethod : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FADepreciationMethod.isTableMethod>
  {
  }

  public abstract class isPredefined : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FADepreciationMethod.isPredefined>
  {
  }

  public abstract class source : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FADepreciationMethod.source>
  {
    public const string Predefined = "P";
    public const string Custom = "C";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "P", "C" }, new string[2]
        {
          "Predefined",
          "Custom"
        })
      {
      }
    }

    public class predefined : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FADepreciationMethod.source.predefined>
    {
      public predefined()
        : base("P")
      {
      }
    }

    public class custom : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FADepreciationMethod.source.custom>
    {
      public custom()
        : base("C")
      {
      }
    }
  }

  public abstract class yearlyAccountancy : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FADepreciationMethod.yearlyAccountancy>
  {
  }

  public abstract class dBMultiPlier : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADepreciationMethod.dBMultiPlier>
  {
  }

  public abstract class switchToSL : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FADepreciationMethod.switchToSL>
  {
  }

  public abstract class percentPerYear : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADepreciationMethod.percentPerYear>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FADepreciationMethod.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FADepreciationMethod.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FADepreciationMethod.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADepreciationMethod.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADepreciationMethod.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FADepreciationMethod.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADepreciationMethod.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADepreciationMethod.lastModifiedDateTime>
  {
  }
}
