// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookBalance
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXProjection(typeof (SelectFrom<FABookBalance, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<FixedAsset>.On<BqlOperand<FABookBalance.assetID, IBqlInt>.IsEqual<FixedAsset.assetID>>>, FbqlJoins.Left<FABookHistory>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookHistory.assetID, Equal<FABookBalance.assetID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookHistory.bookID, Equal<FABookBalance.bookID>>>>>.And<BqlOperand<FABookHistory.finPeriodID, IBqlString>.IsEqual<BqlOperand<FABookBalance.maxHistoryPeriodID, IBqlString>.When<BqlOperand<FixedAsset.underConstruction, IBqlBool>.IsEqual<True>>.Else<BqlOperand<FABookBalance.currDeprPeriod, IBqlString>.IfNullThen<FABookBalance.lastPeriod>>>>>>>>.InnerJoin<FABook>.On<BqlOperand<FABook.bookID, IBqlInt>.IsEqual<FABookBalance.bookID>>), new Type[] {typeof (FABookBalance)})]
[PXCacheName("FA Book Balance")]
[Serializable]
public class FABookBalance : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected int? _AssetID;
  protected int? _ClassID;
  protected int? _BookID;
  protected 
  #nullable disable
  string _Status;
  protected bool? _UpdateGL;
  protected string _BookCD;
  protected bool? _Depreciate;
  protected string _AveragingConvention;
  protected Decimal? _UsefulLife;
  protected string _MidMonthType;
  protected short? _MidMonthDay;
  protected Decimal? _ADSLife;
  protected Decimal? _AcquisitionCost;
  protected Decimal? _SalvageAmount;
  protected Decimal? _BusinessUse;
  protected int? _BonusID;
  protected Decimal? _BonusRate;
  protected Decimal? _BonusAmount;
  protected Decimal? _Tax179Amount;
  protected DateTime? _DeprFromDate;
  protected string _DeprFromPeriod;
  protected string _DeprFromYear;
  protected int? _DepreciationMethodID;
  protected int? _RecoveryPeriod;
  protected DateTime? _DeprToDate;
  protected string _DeprToPeriod;
  protected string _DeprToYear;
  protected string _LastDeprPeriod;
  protected string _CurrDeprPeriod;
  protected string _InitPeriod;
  protected string _LastPeriod;
  protected string _HistPeriod;
  protected string _DisposalPeriodID;
  protected Decimal? _YtdDeprBase;
  protected Decimal? _YtdDepreciated;
  protected Decimal? _YtdBal;
  protected Decimal? _YtdAcquired;
  protected Decimal? _YtdTax179Recap;
  protected Decimal? _YtdBonusRecap;
  protected Decimal? _YtdRGOL;
  protected Decimal? _PtdDeprDisposed;
  protected int? _YtdSuspended;
  protected Decimal? _YtdReconciled;
  protected Decimal? _DisposalAmount;
  protected DateTime? _OrigDeprToDate;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (FixedAsset.assetID))]
  [PXParent(typeof (Select<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FABookBalance.assetID>>>>))]
  [PXSelector(typeof (Search<FixedAsset.assetID>), SubstituteKey = typeof (FixedAsset.assetCD), DirtyRead = true, DescriptionField = typeof (FixedAsset.description))]
  [PXUIField(DisplayName = "Fixed Asset", Enabled = false)]
  public virtual int? AssetID
  {
    get => this._AssetID;
    set => this._AssetID = value;
  }

  [PXInt]
  [PXDBScalar(typeof (Search<FixedAsset.classID, Where<FixedAsset.assetID, Equal<FABookBalance.assetID>>>))]
  [PXDefault(typeof (Search<FixedAsset.classID, Where<FixedAsset.assetID, Equal<Current<FABookBalance.assetID>>>>))]
  [PXSelector(typeof (Search<FixedAsset.assetID>), SubstituteKey = typeof (FixedAsset.assetCD), CacheGlobal = true, DescriptionField = typeof (FixedAsset.description))]
  [PXUIField(DisplayName = "Asset Class", Enabled = false)]
  public virtual int? ClassID
  {
    get => this._ClassID;
    set => this._ClassID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXSelector(typeof (Search2<FABook.bookID, InnerJoin<FABookSettings, On<FABookSettings.bookID, Equal<FABook.bookID>>>, Where<FABookSettings.assetID, Equal<Current<FixedAsset.classID>>>>), SubstituteKey = typeof (FABook.bookCode), DescriptionField = typeof (FABook.description), CacheGlobal = true)]
  [PXUIField(DisplayName = "Book")]
  public virtual int? BookID
  {
    get => this._BookID;
    set => this._BookID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  [FixedAssetStatus.List]
  [PXDefault("A")]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBBool(BqlField = typeof (FABook.updateGL))]
  [PXDefault(false, typeof (Search<FABook.updateGL, Where<FABook.bookID, Equal<Current<FABookBalance.bookID>>>>))]
  [PXUIField(DisplayName = "Posting Book", Enabled = false)]
  public virtual bool? UpdateGL
  {
    get => this._UpdateGL;
    set => this._UpdateGL = value;
  }

  /// <inheritdoc cref="P:PX.Objects.FA.FABook.BookCode" />
  [PXDBString(BqlField = typeof (FABook.bookCode))]
  [PXDefault("", typeof (Search<FABook.bookCode, Where<FABook.bookID, Equal<Current<FABookBalance.bookID>>>>))]
  [PXUIField(DisplayName = "Book CD", Enabled = false)]
  public virtual string BookCD
  {
    get => this._BookCD;
    set => this._BookCD = value;
  }

  [PXDBBool]
  [PXDefault(typeof (Search<FixedAsset.depreciable, Where<FixedAsset.assetID, Equal<Current<FABookBalance.assetID>>>>))]
  [PXUIField]
  public virtual bool? Depreciate
  {
    get => this._Depreciate;
    set => this._Depreciate = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Averaging Convention")]
  [FAAveragingConvention.List]
  [PXDefault(typeof (Search<FABookSettings.averagingConvention, Where<FABookSettings.bookID, Equal<Current<FABookBalance.bookID>>, And<FABookSettings.assetID, Equal<Current<FABookBalance.classID>>>>>))]
  [PXUIEnabled(typeof (Switch<Case2<Where<FABookBalance.depreciate, Equal<True>, And<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, NotEqual<FADepreciationMethod.depreciationMethod.australianPrimeCost>, And<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, NotEqual<FADepreciationMethod.depreciationMethod.australianDiminishingValue>, And<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, NotEqual<FADepreciationMethod.depreciationMethod.newZealandStraightLine>, And<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, NotEqual<FADepreciationMethod.depreciationMethod.newZealandDiminishingValue>, And<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, NotEqual<FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>, And<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, NotEqual<FADepreciationMethod.depreciationMethod.newZealandDiminishingValueEvenly>>>>>>>>, True>, False>))]
  [PXFormula(typeof (Switch<Case2<Where<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.australianPrimeCost>, Or<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.australianDiminishingValue>>>, FAAveragingConvention.fullDay, Case2<Where<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLine>, Or<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandDiminishingValue>, Or<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>, Or<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandDiminishingValueEvenly>>>>>, FAAveragingConvention.fullPeriod>>, FABookBalance.averagingConvention>))]
  public virtual string AveragingConvention
  {
    get => this._AveragingConvention;
    set => this._AveragingConvention = value;
  }

  [PXDBDecimal(4, MinValue = 0.0)]
  [PXDefault(typeof (Coalesce<Search<FABookSettings.percentPerYear, Where<FABookSettings.bookID, Equal<Current<FABookBalance.bookID>>, And<FABookSettings.assetID, Equal<Current<FABookBalance.classID>>, And<FABookSettings.depreciationMethodID, Equal<Current<FABookBalance.depreciationMethodID>>>>>>, SearchFor<FADepreciationMethod.percentPerYear>.In<SelectFromBase<FADepreciationMethod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FADepreciationMethod.methodID, IBqlInt>.IsEqual<BqlField<FABookBalance.depreciationMethodID, IBqlInt>.FromCurrent>>>>))]
  [PXUIField(DisplayName = "Percent per Year")]
  [PXUIEnabled(typeof (Switch<Case2<Where<FABookBalance.depreciate, Equal<True>, And<Where<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.australianPrimeCost>, Or<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.australianDiminishingValue>, Or<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLine>, Or<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandDiminishingValue>, Or<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>, Or<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandDiminishingValueEvenly>>>>>>>>>, True>, False>))]
  [UndefaultFormula(typeof (Switch<Case2<Where<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.australianPrimeCost>, Or<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.australianDiminishingValue>, Or<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLine>, Or<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandDiminishingValue>, Or<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>, Or<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandDiminishingValueEvenly>>>>>>>, DefaultValue<FABookBalance.percentPerYear>>>))]
  public virtual Decimal? PercentPerYear { get; set; }

  [PXDBDecimal(4, MinValue = 0.0)]
  [PXDefault(typeof (Search<FABookSettings.usefulLife, Where<FABookSettings.bookID, Equal<Current<FABookBalance.bookID>>, And<FABookSettings.assetID, Equal<Current<FABookBalance.classID>>>>>))]
  [PXUIField(DisplayName = "Useful Life, Years")]
  [PXUIRequired(typeof (FABookBalance.depreciate))]
  [PXUIEnabled(typeof (Switch<Case2<Where<FABookBalance.depreciate, Equal<True>, And<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, NotEqual<FADepreciationMethod.depreciationMethod.australianPrimeCost>, And<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, NotEqual<FADepreciationMethod.depreciationMethod.newZealandStraightLine>, And<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, NotEqual<FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>>>>>, True>, False>))]
  [PXFormula(typeof (Switch<Case2<Where<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.australianPrimeCost>, Or<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLine>, Or<Selector<FABookBalance.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>>>>, Div<decimal100, FABookBalance.percentPerYear>>, FABookBalance.usefulLife>))]
  public virtual Decimal? UsefulLife
  {
    get => this._UsefulLife;
    set => this._UsefulLife = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Mid-Period Type", Enabled = false)]
  [PXDefault("F", typeof (Search<FABookSettings.midMonthType, Where<FABookSettings.bookID, Equal<Current<FABookBalance.bookID>>, And<FABookSettings.assetID, Equal<Current<FABookBalance.classID>>>>>))]
  [FABook.midMonthType.List]
  [PXUIRequired(typeof (FABookSettings.midMonthType.IsRequired<FABookBalance.depreciate, FABookBalance.averagingConvention>))]
  [PXUIEnabled(typeof (FABookSettings.midMonthType.IsRequired<FABookBalance.depreciate, FABookBalance.averagingConvention>))]
  public virtual string MidMonthType
  {
    get => this._MidMonthType;
    set => this._MidMonthType = value;
  }

  [PXDBShort]
  [PXDefault(typeof (Search<FABookSettings.midMonthDay, Where<FABookSettings.bookID, Equal<Current<FABookBalance.bookID>>, And<FABookSettings.assetID, Equal<Current<FABookBalance.classID>>>>>))]
  [PXUIField(DisplayName = "Mid-Period Day")]
  [PXUIRequired(typeof (FABookSettings.midMonthType.IsRequired<FABookBalance.depreciate, FABookBalance.averagingConvention>))]
  [PXUIEnabled(typeof (FABookSettings.midMonthType.IsRequired<FABookBalance.depreciate, FABookBalance.averagingConvention>))]
  public virtual short? MidMonthDay
  {
    get => this._MidMonthDay;
    set => this._MidMonthDay = value;
  }

  [PXDBDecimal(2)]
  [PXFormula(typeof (FABookBalance.usefulLife))]
  [PXUIField(DisplayName = "ADS Life, Years")]
  public virtual Decimal? ADSLife
  {
    get => this._ADSLife;
    set => this._ADSLife = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(typeof (Search<FADetails.acquisitionCost, Where<FADetails.assetID, Equal<Current<FABookBalance.assetID>>>>))]
  [PXUIField(DisplayName = "Orig. Acquisition Cost")]
  public virtual Decimal? AcquisitionCost
  {
    get => this._AcquisitionCost;
    set => this._AcquisitionCost = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(typeof (Search<FADetails.salvageAmount, Where<FADetails.assetID, Equal<Current<FABookBalance.assetID>>>>))]
  [PXUIField(DisplayName = "Salvage Amount")]
  public virtual Decimal? SalvageAmount
  {
    get => this._SalvageAmount;
    set => this._SalvageAmount = value;
  }

  [PXDBDecimal(4, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "100.0")]
  [PXUIField(DisplayName = "Business Use, %")]
  public virtual Decimal? BusinessUse
  {
    get => this._BusinessUse;
    set => this._BusinessUse = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Bonus")]
  [PXSelector(typeof (Search2<FABonus.bonusID, CrossJoin<FABookSettings>, Where<FABookSettings.bookID, Equal<Current<FABookBalance.bookID>>, And<FABookSettings.assetID, Equal<Current<FABookBalance.classID>>, And<FABookSettings.bonus, Equal<True>>>>>), new Type[] {typeof (FABonus.bonusCD)}, SubstituteKey = typeof (FABonus.bonusCD), DescriptionField = typeof (FABonus.description))]
  [PXReferentialIntegrityCheck]
  [PXForeignReference(typeof (Field<FABookBalance.bonusID>.IsRelatedTo<FABonus.bonusID>))]
  public virtual int? BonusID
  {
    get => this._BonusID;
    set => this._BonusID = value;
  }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXUIField(DisplayName = "Bonus Rate")]
  [PXFormula(typeof (GetBonusRate<FABookBalance.deprFromDate, FABookBalance.bonusID>))]
  public virtual Decimal? BonusRate
  {
    get => this._BonusRate;
    set => this._BonusRate = value;
  }

  [PXDBBaseCury(null, null)]
  [PXFormula(typeof (Mult<Sub<Mult<FABookBalance.acquisitionCost, Div<FABookBalance.businessUse, decimal100>>, FABookBalance.tax179Amount>, Div<FABookBalance.bonusRate, decimal100>>))]
  [PXUIField(DisplayName = "Bonus Amount")]
  public virtual Decimal? BonusAmount
  {
    get => this._BonusAmount;
    set => this._BonusAmount = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tax 179 Amount")]
  public virtual Decimal? Tax179Amount
  {
    get => this._Tax179Amount;
    set => this._Tax179Amount = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Depr. From")]
  [PXDefault(typeof (Search<FADetails.depreciateFromDate, Where<FADetails.assetID, Equal<Current<FABookBalance.assetID>>>>))]
  public virtual DateTime? DeprFromDate
  {
    get => this._DeprFromDate;
    set => this._DeprFromDate = value;
  }

  [PXUIField(DisplayName = "Depr. From Period", Enabled = false)]
  [FABookPeriodSelector(null, null, null, typeof (FABookBalance.bookID), true, typeof (FABookBalance.assetID), null, null, null, null, null, ReportParametersFlag.None)]
  [PXFormula(typeof (RecoveryStartPeriod<FABookBalance.deprFromDate, FABookBalance.bookID, FABookBalance.depreciationMethodID, FABookBalance.averagingConvention, FABookBalance.midMonthType, FABookBalance.midMonthDay>))]
  public virtual string DeprFromPeriod
  {
    get => this._DeprFromPeriod;
    set => this._DeprFromPeriod = value;
  }

  [PXString(4, IsFixed = true)]
  [PXFormula(typeof (Substring<FABookBalance.deprFromPeriod, int0, int4>))]
  public virtual string DeprFromYear
  {
    get => this._DeprFromYear;
    set => this._DeprFromYear = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Search<FADepreciationMethod.methodID, Where2<Where<Current2<FABookBalance.usefulLife>, IsNull, Or<FADepreciationMethod.usefulLife, IsNull, Or<FADepreciationMethod.usefulLife, Equal<Current2<FABookBalance.usefulLife>>, Or<FADepreciationMethod.usefulLife, Equal<decimal0>>>>>, And<Where<FADepreciationMethod.recordType, NotEqual<FARecordType.classType>>>>>), new Type[] {typeof (FADepreciationMethod.methodCD), typeof (FADepreciationMethod.depreciationMethod), typeof (FADepreciationMethod.usefulLife), typeof (FADepreciationMethod.recoveryPeriod), typeof (FADepreciationMethod.averagingConvention), typeof (FADepreciationMethod.recordType), typeof (FADepreciationMethod.description)}, SubstituteKey = typeof (FADepreciationMethod.methodCD), DescriptionField = typeof (FADepreciationMethod.description))]
  [PXFormula(typeof (SelectDepreciationMethod<FABookBalance.deprFromDate, FABookBalance.classID, FABookBalance.bookID, FABookBalance.assetID>))]
  [PXUIField(DisplayName = "Depreciation Method")]
  [PXDefault]
  [PXUIEnabled(typeof (FABookBalance.depreciate))]
  public virtual int? DepreciationMethodID
  {
    get => this._DepreciationMethodID;
    set => this._DepreciationMethodID = value;
  }

  [PXDBInt(MinValue = 0)]
  [PXUIField(DisplayName = "Recovery Periods", Visible = false)]
  public virtual int? RecoveryPeriod
  {
    get => this._RecoveryPeriod;
    set => this._RecoveryPeriod = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Depr. to")]
  public virtual DateTime? DeprToDate
  {
    get => this._DeprToDate;
    set => this._DeprToDate = value;
  }

  [PXUIField(DisplayName = "Depr. to Period", Enabled = false)]
  [FABookPeriodSelector(null, null, null, typeof (FABookBalance.bookID), true, typeof (FABookBalance.assetID), null, null, null, null, null, ReportParametersFlag.None)]
  [PXFormula(typeof (Switch<Case<Where<FABookBalance.depreciate, Equal<True>>, OffsetBookDateToPeriod<FABookBalance.deprFromDate, FABookBalance.bookID, FABookBalance.assetID, FABookBalance.depreciationMethodID, FABookBalance.averagingConvention, FABookBalance.usefulLife>>>))]
  public virtual string DeprToPeriod
  {
    get => this._DeprToPeriod;
    set => this._DeprToPeriod = value;
  }

  [PXString(4, IsFixed = true)]
  [PXFormula(typeof (Substring<FABookBalance.deprToPeriod, int0, int4>))]
  public virtual string DeprToYear
  {
    get => this._DeprToYear;
    set => this._DeprToYear = value;
  }

  [PXUIField(DisplayName = "Last Depr. Period", Enabled = false)]
  [FABookPeriodSelector(null, null, null, typeof (FABookBalance.bookID), true, typeof (FABookBalance.assetID), null, null, null, null, null, ReportParametersFlag.None)]
  public virtual string LastDeprPeriod
  {
    get => this._LastDeprPeriod;
    set => this._LastDeprPeriod = value;
  }

  [PXDBString(6, IsFixed = true)]
  [PXUIField(DisplayName = "Current Period", Enabled = false)]
  [FinPeriodIDFormatting]
  public virtual string CurrDeprPeriod
  {
    get => this._CurrDeprPeriod;
    set => this._CurrDeprPeriod = value;
  }

  [PXDBString(6, IsFixed = true)]
  [FinPeriodIDFormatting]
  public virtual string InitPeriod
  {
    get => this._InitPeriod;
    set => this._InitPeriod = value;
  }

  [PXDBString(6, IsFixed = true)]
  [FinPeriodIDFormatting]
  public virtual string LastPeriod
  {
    get => this._LastPeriod;
    set => this._LastPeriod = value;
  }

  [PXString(6, IsFixed = true)]
  [PXDBCalced(typeof (Switch<Case<Where<FABookBalance.currDeprPeriod, IsNull, And<FABookBalance.deprToPeriod, IsNull>>, FABookBalance.deprFromPeriod, Case<Where<FABookBalance.currDeprPeriod, IsNull>, FABookBalance.deprToPeriod>>, FABookBalance.currDeprPeriod>), typeof (string))]
  public virtual string HistPeriod
  {
    get => this._HistPeriod;
    set => this._HistPeriod = value;
  }

  [PXDBString(6, IsFixed = true)]
  public virtual string MaxHistoryPeriodID { get; set; }

  [PXDBString(6, IsFixed = true)]
  [FinPeriodIDFormatting]
  public virtual string DisposalPeriodID
  {
    get => this._DisposalPeriodID;
    set => this._DisposalPeriodID = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (FABookHistory.ytdDeprBase))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Basis", Enabled = false)]
  public virtual Decimal? YtdDeprBase
  {
    get => this._YtdDeprBase;
    set => this._YtdDeprBase = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (FABookHistory.ytdDepreciated))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Accum. Depr.", Enabled = false)]
  public virtual Decimal? YtdDepreciated
  {
    get => this._YtdDepreciated;
    set => this._YtdDepreciated = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (FABookHistory.ytdBal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Net Value", Enabled = false)]
  public virtual Decimal? YtdBal
  {
    get => this._YtdBal;
    set => this._YtdBal = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (FABookHistory.ytdAcquired))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Current Cost", Enabled = false)]
  public virtual Decimal? YtdAcquired
  {
    get => this._YtdAcquired;
    set => this._YtdAcquired = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (FABookHistory.ytdTax179Recap))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tax 179 Recapture", Enabled = false)]
  public virtual Decimal? YtdTax179Recap
  {
    get => this._YtdTax179Recap;
    set => this._YtdTax179Recap = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (FABookHistory.ytdBonusRecap))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Bonus Recapture", Enabled = false)]
  public virtual Decimal? YtdBonusRecap
  {
    get => this._YtdBonusRecap;
    set => this._YtdBonusRecap = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (FABookHistory.ytdRGOL))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Gain/Loss Amount", Enabled = false)]
  public virtual Decimal? YtdRGOL
  {
    get => this._YtdRGOL;
    set => this._YtdRGOL = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (FABookHistory.ptdDeprDisposed))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdDeprDisposed
  {
    get => this._PtdDeprDisposed;
    set => this._PtdDeprDisposed = value;
  }

  [PXDBInt(BqlField = typeof (FABookHistory.ytdSuspended))]
  [PXDefault(0)]
  public virtual int? YtdSuspended
  {
    get => this._YtdSuspended;
    set => this._YtdSuspended = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (FABookHistory.ytdReconciled))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdReconciled
  {
    get => this._YtdReconciled;
    set => this._YtdReconciled = value;
  }

  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Disposal Amount")]
  public virtual Decimal? DisposalAmount
  {
    get => this._DisposalAmount;
    set => this._DisposalAmount = value;
  }

  [PXDBDate]
  public virtual DateTime? OrigDeprToDate
  {
    get => this._OrigDeprToDate;
    set => this._OrigDeprToDate = value;
  }

  [PXBool]
  [PXDefault(true)]
  public virtual bool? AllowChangeDeprFromPeriod { get; set; }

  /// <summary>
  /// A flag that indicates whether the fixed asset was acquired (at least one purchasing transaction is released for the book).
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsAcquired { get; set; }

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
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the book balance.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  public class PK : PrimaryKeyOf<FABookBalance>.By<FABookBalance.assetID, FABookBalance.bookID>
  {
    public static FABookBalance Find(
      PXGraph graph,
      int? assetID,
      int? bookID,
      PKFindOptions options = 0)
    {
      return (FABookBalance) PrimaryKeyOf<FABookBalance>.By<FABookBalance.assetID, FABookBalance.bookID>.FindBy(graph, (object) assetID, (object) bookID, options);
    }
  }

  public static class FK
  {
    public class FixedAsset : 
      PrimaryKeyOf<FixedAsset>.By<FixedAsset.assetID>.ForeignKeyOf<FABookBalance>.By<FABookBalance.assetID>
    {
    }

    public class AssetClass : 
      PrimaryKeyOf<FAClass>.By<FAClass.assetID>.ForeignKeyOf<FABookBalance>.By<FABookBalance.classID>
    {
    }

    public class Book : 
      PrimaryKeyOf<FABook>.By<FABook.bookID>.ForeignKeyOf<FABookBalance>.By<FABookBalance.bookID>
    {
    }

    public class Bonus : 
      PrimaryKeyOf<FABonus>.By<FABonus.bonusID>.ForeignKeyOf<FABookBalance>.By<FABookBalance.bonusID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookBalance.selected>
  {
  }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookBalance.assetID>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookBalance.classID>
  {
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookBalance.bookID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookBalance.status>
  {
  }

  public abstract class updateGL : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookBalance.updateGL>
  {
  }

  public abstract class bookCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookBalance.bookCD>
  {
  }

  public abstract class depreciate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookBalance.depreciate>
  {
  }

  public abstract class averagingConvention : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookBalance.averagingConvention>
  {
  }

  public abstract class percentPerYear : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookBalance.percentPerYear>
  {
  }

  public abstract class usefulLife : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookBalance.usefulLife>
  {
  }

  public abstract class midMonthType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookBalance.midMonthType>
  {
  }

  public abstract class midMonthDay : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FABookBalance.midMonthDay>
  {
  }

  public abstract class aDSLife : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookBalance.aDSLife>
  {
  }

  public abstract class acquisitionCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookBalance.acquisitionCost>
  {
  }

  public abstract class salvageAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookBalance.salvageAmount>
  {
  }

  public abstract class businessUse : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookBalance.businessUse>
  {
  }

  public abstract class bonusID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookBalance.bonusID>
  {
  }

  public abstract class bonusRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookBalance.bonusRate>
  {
  }

  public abstract class bonusAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookBalance.bonusAmount>
  {
  }

  public abstract class tax179Amount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookBalance.tax179Amount>
  {
  }

  public abstract class deprFromDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookBalance.deprFromDate>
  {
  }

  public abstract class deprFromPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookBalance.deprFromPeriod>
  {
  }

  public abstract class deprFromYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookBalance.deprFromYear>
  {
  }

  public abstract class depreciationMethodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FABookBalance.depreciationMethodID>
  {
  }

  public abstract class recoveryPeriod : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookBalance.recoveryPeriod>
  {
  }

  public abstract class deprToDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FABookBalance.deprToDate>
  {
  }

  public abstract class deprToPeriod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookBalance.deprToPeriod>
  {
  }

  public abstract class deprToYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookBalance.deprToYear>
  {
  }

  public abstract class lastDeprPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookBalance.lastDeprPeriod>
  {
  }

  public abstract class currDeprPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookBalance.currDeprPeriod>
  {
  }

  public abstract class initPeriod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookBalance.initPeriod>
  {
  }

  /// <summary>
  /// the period of the latest asset activity in the calendar. Can be greater than its usefull life
  /// </summary>
  public abstract class lastPeriod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookBalance.lastPeriod>
  {
  }

  public abstract class histPeriod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookBalance.histPeriod>
  {
  }

  public abstract class maxHistoryPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookBalance.maxHistoryPeriodID>
  {
  }

  public abstract class disposalPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookBalance.disposalPeriodID>
  {
  }

  public abstract class ytdDeprBase : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookBalance.ytdDeprBase>
  {
  }

  public abstract class ytdDepreciated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookBalance.ytdDepreciated>
  {
  }

  public abstract class ytdBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookBalance.ytdBal>
  {
  }

  public abstract class ytdAcquired : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookBalance.ytdAcquired>
  {
  }

  public abstract class ytdTax179Recap : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookBalance.ytdTax179Recap>
  {
  }

  public abstract class ytdBonusRecap : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookBalance.ytdBonusRecap>
  {
  }

  public abstract class ytdRGOL : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookBalance.ytdRGOL>
  {
  }

  public abstract class ptdDeprDisposed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookBalance.ptdDeprDisposed>
  {
  }

  public abstract class ytdSuspended : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookBalance.ytdSuspended>
  {
  }

  public abstract class ytdReconciled : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookBalance.ytdReconciled>
  {
  }

  public abstract class disposalAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookBalance.disposalAmount>
  {
  }

  public abstract class origDeprToDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookBalance.origDeprToDate>
  {
  }

  public abstract class allowChangeDeprFromPeriod : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FABookBalance.allowChangeDeprFromPeriod>
  {
  }

  public abstract class isAcquired : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookBalance.isAcquired>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FABookBalance.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FABookBalance.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookBalance.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookBalance.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FABookBalance.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookBalance.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookBalance.lastModifiedDateTime>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FABookBalance.noteID>
  {
  }
}
