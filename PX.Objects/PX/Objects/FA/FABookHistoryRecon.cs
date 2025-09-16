// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookHistoryRecon
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

[PXProjection(typeof (Select2<FABookHistoryMax, InnerJoin<FABookHistory, On<FABookHistoryMax.assetID, Equal<FABookHistory.assetID>, And<FABookHistoryMax.bookID, Equal<FABookHistory.bookID>, And<FABookHistoryMax.finPeriodID, Equal<FABookHistory.finPeriodID>>>>, InnerJoin<FABook, On<FABook.bookID, Equal<FABookHistory.bookID>>>>>))]
[PXCacheName("FA Book History for Reconciliation")]
[Serializable]
public class FABookHistoryRecon : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AssetID;
  protected int? _BookID;
  protected bool? _UpdateGL;
  protected Decimal? _YtdAcquired;
  protected Decimal? _YtdReconciled;

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

  [PXDBBool(BqlField = typeof (FABook.updateGL))]
  public virtual bool? UpdateGL
  {
    get => this._UpdateGL;
    set => this._UpdateGL = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (FABookHistory.ytdAcquired))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdAcquired
  {
    get => this._YtdAcquired;
    set => this._YtdAcquired = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (FABookHistory.ytdReconciled))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? YtdReconciled
  {
    get => this._YtdReconciled;
    set => this._YtdReconciled = value;
  }

  public abstract class assetID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  FABookHistoryRecon.assetID>
  {
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookHistoryRecon.bookID>
  {
  }

  public abstract class updateGL : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookHistoryRecon.updateGL>
  {
  }

  public abstract class ytdAcquired : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistoryRecon.ytdAcquired>
  {
  }

  public abstract class ytdReconciled : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookHistoryRecon.ytdReconciled>
  {
  }
}
