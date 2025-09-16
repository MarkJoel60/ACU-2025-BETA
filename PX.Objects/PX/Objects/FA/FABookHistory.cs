// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXCacheName("FA Book History")]
[Serializable]
public class FABookHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AssetID;
  protected int? _BookID;
  protected 
  #nullable disable
  string _FinPeriodID;
  protected bool? _Closed;
  protected bool? _Suspended;
  protected Decimal? _BegBal;
  protected Decimal? _YtdBal;
  protected Decimal? _BegDeprBase;
  protected Decimal? _PtdDeprBase;
  protected Decimal? _YtdDeprBase;
  protected Decimal? _YtdAcquired;
  protected Decimal? _PtdAcquired;
  protected Decimal? _YtdReconciled;
  protected Decimal? _PtdReconciled;
  protected Decimal? _YtdDepreciated;
  protected Decimal? _PtdDepreciated;
  protected Decimal? _PtdAdjusted;
  protected Decimal? _PtdDeprDisposed;
  protected Decimal? _PtdCalculated;
  protected Decimal? _YtdCalculated;
  protected Decimal? _YtdBonus;
  protected Decimal? _PtdBonus;
  protected Decimal? _YtdBonusTaken;
  protected Decimal? _PtdBonusTaken;
  protected Decimal? _YtdBonusCalculated;
  protected Decimal? _PtdBonusCalculated;
  protected Decimal? _YtdBonusRecap;
  protected Decimal? _PtdBonusRecap;
  protected Decimal? _YtdTax179;
  protected Decimal? _PtdTax179;
  protected Decimal? _YtdTax179Taken;
  protected Decimal? _PtdTax179Taken;
  protected Decimal? _YtdTax179Calculated;
  protected Decimal? _PtdTax179Calculated;
  protected Decimal? _YtdTax179Recap;
  protected Decimal? _PtdTax179Recap;
  protected Decimal? _YtdRevalueAmount;
  protected Decimal? _PtdRevalueAmount;
  protected Decimal? _YtdDisposalAmount;
  protected Decimal? _PtdDisposalAmount;
  protected Decimal? _YtdRGOL;
  protected Decimal? _PtdRGOL;
  protected int? _YtdSuspended;
  protected int? _YtdReversed;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// A reference to <see cref="T:PX.Objects.FA.FixedAsset" />.
  /// The field is a part of the primary key.
  /// The full primary key contains <see cref="P:PX.Objects.FA.FABookHistory.AssetID" />, <see cref="P:PX.Objects.FA.FABookHistory.BookID" />, and <see cref="P:PX.Objects.FA.FABookHistory.FinPeriodID" /> fields.
  /// </summary>
  /// <value>
  /// An integer identifier of the fixed asset.
  /// This is a required field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? AssetID
  {
    get => this._AssetID;
    set => this._AssetID = value;
  }

  /// <summary>
  /// A reference to <see cref="T:PX.Objects.FA.FABook" />.
  /// The field is a part of the primary key.
  /// The full primary key contains <see cref="P:PX.Objects.FA.FABookHistory.AssetID" />, <see cref="P:PX.Objects.FA.FABookHistory.BookID" />, and <see cref="P:PX.Objects.FA.FABookHistory.FinPeriodID" /> fields.
  /// </summary>
  /// <value>
  /// An integer identifier of the book.
  /// This is a required field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? BookID
  {
    get => this._BookID;
    set => this._BookID = value;
  }

  /// <summary>
  /// The financial period.
  /// The field is a part of the primary key.
  /// The full primary key contains <see cref="P:PX.Objects.FA.FABookHistory.AssetID" />, <see cref="P:PX.Objects.FA.FABookHistory.BookID" />, and <see cref="P:PX.Objects.FA.FABookHistory.FinPeriodID" /> fields.
  /// </summary>
  /// <value>This is a required field.</value>
  [FABookPeriodID(typeof (FABookHistory.bookID), null, true, typeof (FABookHistory.assetID), null, null, null, null, null, IsKey = true)]
  [PXDefault]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  /// <summary>
  /// A flag that indicates (if set to <c>true</c>) that the financial period is closed for the depreciation.
  /// </summary>
  /// <value>
  /// The financial period is closed when the depreciation amount of the fixed asset in the financial period is calculated and successfully posted to the General Ledger module.
  /// By default, the value is set to false.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Closed
  {
    get => this._Closed;
    set => this._Closed = value;
  }

  /// <summary>
  /// A flag that indicates (if set to <c>true</c>) that the financial period is reopened.
  /// </summary>
  /// <value>
  /// The financial period becomes reopen, if, in the financial period, negative depreciation adjustment to zero amount has been performed manually.
  /// By default, the value is set to false.
  /// </value>
  [PXBool]
  [PXDefault(false)]
  public virtual bool? Reopen { get; set; }

  /// <summary>
  /// A flag that indicates (if set to <c>true</c>) that the fixed asset is suspended in the financial period.
  /// </summary>
  /// <value>By default, the value is set to false.</value>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Suspended
  {
    get => this._Suspended;
    set => this._Suspended = value;
  }

  /// <summary>
  /// The beginning balance (net value) of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BegBal
  {
    get => this._BegBal;
    set => this._BegBal = value;
  }

  /// <summary>
  /// The ending balance (net value) of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdBal
  {
    get => this._YtdBal;
    set => this._YtdBal = value;
  }

  /// <summary>
  /// The beginning value of the depreciation basis of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BegDeprBase
  {
    get => this._BegDeprBase;
    set => this._BegDeprBase = value;
  }

  /// <summary>
  /// The change of the depreciation basis of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdDeprBase
  {
    get => this._PtdDeprBase;
    set => this._PtdDeprBase = value;
  }

  /// <summary>
  /// The ending value of the depreciation basis of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdDeprBase
  {
    get => this._YtdDeprBase;
    set => this._YtdDeprBase = value;
  }

  /// <summary>
  /// The ending value of the acquisition amount of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdAcquired
  {
    get => this._YtdAcquired;
    set => this._YtdAcquired = value;
  }

  /// <summary>
  /// The change of the acquisition amount of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdAcquired
  {
    get => this._PtdAcquired;
    set => this._PtdAcquired = value;
  }

  /// <summary>
  /// The ending value of the GL reconciliation amount of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdReconciled
  {
    get => this._YtdReconciled;
    set => this._YtdReconciled = value;
  }

  /// <summary>
  /// The change of the GL reconciliation amount of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdReconciled
  {
    get => this._PtdReconciled;
    set => this._PtdReconciled = value;
  }

  /// <summary>
  /// The accumulated depreciation amount of the fixed asset at the end of the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdDepreciated
  {
    get => this._YtdDepreciated;
    set => this._YtdDepreciated = value;
  }

  /// <summary>
  /// The change of the depreciation amount of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdDepreciated
  {
    get => this._PtdDepreciated;
    set => this._PtdDepreciated = value;
  }

  /// <summary>
  /// The change of the depreciation amount of the fixed asset because of split of the assets in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdAdjusted
  {
    get => this._PtdAdjusted;
    set => this._PtdAdjusted = value;
  }

  /// <summary>
  /// The change of the depreciation amount of the fixed asset because of disposal of the assets in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdDeprDisposed
  {
    get => this._PtdDeprDisposed;
    set => this._PtdDeprDisposed = value;
  }

  /// <summary>
  /// The calculated depreciation amount of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdCalculated
  {
    get => this._PtdCalculated;
    set => this._PtdCalculated = value;
  }

  /// <summary>
  /// The calculated depreciation amount of the fixed asset at the end of the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdCalculated
  {
    get => this._YtdCalculated;
    set => this._YtdCalculated = value;
  }

  /// <summary>
  /// The bonus amount of the fixed asset at the end of the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdBonus
  {
    get => this._YtdBonus;
    set => this._YtdBonus = value;
  }

  /// <summary>
  /// The change of the bonus amount of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdBonus
  {
    get => this._PtdBonus;
    set => this._PtdBonus = value;
  }

  /// <summary>
  /// The taken bonus amount of the fixed asset at the end of the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdBonusTaken
  {
    get => this._YtdBonusTaken;
    set => this._YtdBonusTaken = value;
  }

  /// <summary>
  /// The taken bonus amount of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdBonusTaken
  {
    get => this._PtdBonusTaken;
    set => this._PtdBonusTaken = value;
  }

  /// <summary>
  /// The calculated bonus amount of the fixed asset at the end of the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdBonusCalculated
  {
    get => this._YtdBonusCalculated;
    set => this._YtdBonusCalculated = value;
  }

  /// <summary>
  /// The calculated bonus amount of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdBonusCalculated
  {
    get => this._PtdBonusCalculated;
    set => this._PtdBonusCalculated = value;
  }

  /// <summary>
  /// The recaptured bonus amount of the fixed asset at the end of the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdBonusRecap
  {
    get => this._YtdBonusRecap;
    set => this._YtdBonusRecap = value;
  }

  /// <summary>
  /// The recaptured bonus amount of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdBonusRecap
  {
    get => this._PtdBonusRecap;
    set => this._PtdBonusRecap = value;
  }

  /// <summary>
  /// The Tax 179 amount of the fixed asset at the end of the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdTax179
  {
    get => this._YtdTax179;
    set => this._YtdTax179 = value;
  }

  /// <summary>
  /// The change of the Tax 179 amount of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdTax179
  {
    get => this._PtdTax179;
    set => this._PtdTax179 = value;
  }

  /// <summary>
  /// The taken Tax 179 amount of the fixed asset at the end of the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdTax179Taken
  {
    get => this._YtdTax179Taken;
    set => this._YtdTax179Taken = value;
  }

  /// <summary>
  /// The taken Tax 179 amount of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdTax179Taken
  {
    get => this._PtdTax179Taken;
    set => this._PtdTax179Taken = value;
  }

  /// <summary>
  /// The calculated Tax 179 amount of the fixed asset at the end of the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdTax179Calculated
  {
    get => this._YtdTax179Calculated;
    set => this._YtdTax179Calculated = value;
  }

  /// <summary>
  /// The calculated Tax 179 amount of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdTax179Calculated
  {
    get => this._PtdTax179Calculated;
    set => this._PtdTax179Calculated = value;
  }

  /// <summary>
  /// The recaptured Tax 179 amount of the fixed asset at the end of the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdTax179Recap
  {
    get => this._YtdTax179Recap;
    set => this._YtdTax179Recap = value;
  }

  /// <summary>
  /// The recaptured Tax 179 amount of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdTax179Recap
  {
    get => this._PtdTax179Recap;
    set => this._PtdTax179Recap = value;
  }

  /// <summary>A reserved field, which is currently not used.</summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdRevalueAmount
  {
    get => this._YtdRevalueAmount;
    set => this._YtdRevalueAmount = value;
  }

  /// <summary>A reserved field, which is currently not used.</summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdRevalueAmount
  {
    get => this._PtdRevalueAmount;
    set => this._PtdRevalueAmount = value;
  }

  /// <summary>
  /// The sale amount of the fixed asset at the end of the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdDisposalAmount
  {
    get => this._YtdDisposalAmount;
    set => this._YtdDisposalAmount = value;
  }

  /// <summary>
  /// The sale amount of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdDisposalAmount
  {
    get => this._PtdDisposalAmount;
    set => this._PtdDisposalAmount = value;
  }

  /// <summary>
  /// The positive gain or negative loss amount of the fixed asset at the end of the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdRGOL
  {
    get => this._YtdRGOL;
    set => this._YtdRGOL = value;
  }

  /// <summary>
  /// The positive gain or negative loss amount of the fixed asset in the financial period.
  /// </summary>
  /// <value>By default, the value is 0.0.</value>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdRGOL
  {
    get => this._PtdRGOL;
    set => this._PtdRGOL = value;
  }

  /// <summary>The service field.</summary>
  /// <value>
  /// The number of financial periods at the end of which the fixed asset has been suspended.
  /// </value>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? YtdSuspended
  {
    get => this._YtdSuspended;
    set => this._YtdSuspended = value;
  }

  /// <summary>The service field.</summary>
  /// <value>
  /// The number of financial periods at the end of which the fixed asset has been reversed from suspend.
  /// </value>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? YtdReversed
  {
    get => this._YtdReversed;
    set => this._YtdReversed = value;
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

  public class PK : 
    PrimaryKeyOf<FABookHistory>.By<FABookHistory.assetID, FABookHistory.bookID, FABookHistory.finPeriodID>
  {
    public static FABookHistory Find(
      PXGraph graph,
      int? assetID,
      int? bookID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (FABookHistory) PrimaryKeyOf<FABookHistory>.By<FABookHistory.assetID, FABookHistory.bookID, FABookHistory.finPeriodID>.FindBy(graph, (object) assetID, (object) bookID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class FixedAsset : 
      PrimaryKeyOf<FixedAsset>.By<FixedAsset.assetID>.ForeignKeyOf<FABookHistory>.By<FABookHistory.assetID>
    {
    }

    public class Book : 
      PrimaryKeyOf<FABook>.By<FABook.bookID>.ForeignKeyOf<FABookHistory>.By<FABookHistory.bookID>
    {
    }
  }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookHistory.assetID>
  {
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookHistory.bookID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookHistory.finPeriodID>
  {
  }

  public abstract class closed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookHistory.closed>
  {
  }

  public abstract class reopen : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookHistory.reopen>
  {
  }

  public abstract class suspended : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookHistory.suspended>
  {
  }

  public abstract class begBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookHistory.begBal>
  {
  }

  public abstract class ytdBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookHistory.ytdBal>
  {
  }

  public abstract class begDeprBase : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookHistory.begDeprBase>
  {
  }

  public abstract class ptdDeprBase : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookHistory.ptdDeprBase>
  {
  }

  public abstract class ytdDeprBase : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookHistory.ytdDeprBase>
  {
  }

  public abstract class ytdAcquired : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookHistory.ytdAcquired>
  {
  }

  public abstract class ptdAcquired : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookHistory.ptdAcquired>
  {
  }

  public abstract class ytdReconciled : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ytdReconciled>
  {
  }

  public abstract class ptdReconciled : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ptdReconciled>
  {
  }

  public abstract class ytdDepreciated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ytdDepreciated>
  {
  }

  public abstract class ptdDepreciated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ptdDepreciated>
  {
  }

  public abstract class ptdAdjusted : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookHistory.ptdAdjusted>
  {
  }

  public abstract class ptdDeprDisposed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ptdDeprDisposed>
  {
  }

  public abstract class ptdCalculated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ptdCalculated>
  {
  }

  public abstract class ytdCalculated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ytdCalculated>
  {
  }

  public abstract class ytdBonus : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookHistory.ytdBonus>
  {
  }

  public abstract class ptdBonus : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookHistory.ptdBonus>
  {
  }

  public abstract class ytdBonusTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ytdBonusTaken>
  {
  }

  public abstract class ptdBonusTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ptdBonusTaken>
  {
  }

  public abstract class ytdBonusCalculated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ytdBonusCalculated>
  {
  }

  public abstract class ptdBonusCalculated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ptdBonusCalculated>
  {
  }

  public abstract class ytdBonusRecap : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ytdBonusRecap>
  {
  }

  public abstract class ptdBonusRecap : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ptdBonusRecap>
  {
  }

  public abstract class ytdTax179 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookHistory.ytdTax179>
  {
  }

  public abstract class ptdTax179 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookHistory.ptdTax179>
  {
  }

  public abstract class ytdTax179Taken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ytdTax179Taken>
  {
  }

  public abstract class ptdTax179Taken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ptdTax179Taken>
  {
  }

  public abstract class ytdTax179Calculated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ytdTax179Calculated>
  {
  }

  public abstract class ptdTax179Calculated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ptdTax179Calculated>
  {
  }

  public abstract class ytdTax179Recap : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ytdTax179Recap>
  {
  }

  public abstract class ptdTax179Recap : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ptdTax179Recap>
  {
  }

  public abstract class ytdRevalueAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ytdRevalueAmount>
  {
  }

  public abstract class ptdRevalueAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ptdRevalueAmount>
  {
  }

  public abstract class ytdDisposalAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ytdDisposalAmount>
  {
  }

  public abstract class ptdDisposalAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistory.ptdDisposalAmount>
  {
  }

  public abstract class ytdRGOL : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookHistory.ytdRGOL>
  {
  }

  public abstract class ptdRGOL : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookHistory.ptdRGOL>
  {
  }

  public abstract class ytdSuspended : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookHistory.ytdSuspended>
  {
  }

  public abstract class ytdReversed : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookHistory.ytdReversed>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FABookHistory.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FABookHistory.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookHistory.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookHistory.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FABookHistory.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookHistory.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookHistory.lastModifiedDateTime>
  {
  }
}
