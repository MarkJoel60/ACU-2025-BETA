// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookHistoryMax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXProjection(typeof (SelectFrom<FABookBalance>))]
[PXHidden]
[Serializable]
public class FABookHistoryMax : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AssetID;
  protected int? _BookID;
  protected 
  #nullable disable
  string _FinPeriodID;

  [PXDBInt(IsKey = true, BqlField = typeof (FABookBalance.assetID))]
  [PXDefault]
  public virtual int? AssetID
  {
    get => this._AssetID;
    set => this._AssetID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (FABookBalance.bookID))]
  [PXDefault]
  public virtual int? BookID
  {
    get => this._BookID;
    set => this._BookID = value;
  }

  [FABookPeriodID(typeof (FABookHistoryMax.bookID), null, true, typeof (FABookHistoryMax.assetID), null, null, null, null, null, BqlField = typeof (FABookBalance.maxHistoryPeriodID))]
  [PXDefault]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookHistoryMax.assetID>
  {
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookHistoryMax.bookID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookHistoryMax.finPeriodID>
  {
  }
}
