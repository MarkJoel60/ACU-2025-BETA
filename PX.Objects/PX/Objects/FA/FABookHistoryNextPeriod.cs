// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookHistoryNextPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXProjection(typeof (Select5<FABookHistory, InnerJoin<FABookHistory2, On<FABookHistory2.assetID, Equal<FABookHistory.assetID>, And<FABookHistory2.bookID, Equal<FABookHistory.bookID>, And<FABookHistory2.finPeriodID, GreaterEqual<FABookHistory.finPeriodID>, And<FABookHistory2.suspended, Equal<False>>>>>>, Aggregate<GroupBy<FABookHistory.assetID, GroupBy<FABookHistory.bookID, GroupBy<FABookHistory.finPeriodID, Min<FABookHistory2.finPeriodID, Max<FABookHistory.ptdDeprBase, Max<FABookHistory.ptdAdjusted>>>>>>>>))]
[PXHidden]
[Serializable]
public class FABookHistoryNextPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AssetID;
  protected int? _BookID;
  protected 
  #nullable disable
  string _FinPeriodID;
  protected string _NextPeriodID;
  protected Decimal? _PtdDeprBase;
  protected Decimal? _PtdAdjusted;

  [PXDBInt(IsKey = true, BqlField = typeof (FABookHistory.assetID))]
  [PXDefault]
  public virtual int? AssetID
  {
    get => this._AssetID;
    set => this._AssetID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (FABookHistory.bookID))]
  [PXDefault]
  public virtual int? BookID
  {
    get => this._BookID;
    set => this._BookID = value;
  }

  [PXDBString(6, IsKey = true, IsFixed = true, BqlField = typeof (FABookHistory.finPeriodID))]
  [PXDefault]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXDBString(6, IsFixed = true, BqlField = typeof (FABookHistory2.finPeriodID))]
  [PXDefault]
  public virtual string NextPeriodID
  {
    get => this._NextPeriodID;
    set => this._NextPeriodID = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (FABookHistory.ptdDeprBase))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdDeprBase
  {
    get => this._PtdDeprBase;
    set => this._PtdDeprBase = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (FABookHistory.ptdAdjusted))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdAdjusted
  {
    get => this._PtdAdjusted;
    set => this._PtdAdjusted = value;
  }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookHistoryNextPeriod.assetID>
  {
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookHistoryNextPeriod.bookID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookHistoryNextPeriod.finPeriodID>
  {
  }

  public abstract class nextPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookHistoryNextPeriod.nextPeriodID>
  {
  }

  public abstract class ptdDeprBase : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistoryNextPeriod.ptdDeprBase>
  {
  }

  public abstract class ptdAdjusted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistoryNextPeriod.ptdAdjusted>
  {
  }
}
