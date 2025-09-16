// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FAHistoryByPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable enable
namespace PX.Objects.FA;

/// <summary>
/// The DAC used to simplify selection and aggregation of proper <see cref="T:PX.Objects.FA.FABookHistory" /> records
/// on various inquiry and processing screens of the Fixed Assets module.
/// </summary>
[PXProjection(typeof (Select5<FABookHistory, InnerJoin<FixedAsset, On<FABookHistory.assetID, Equal<FixedAsset.assetID>>, InnerJoin<PX.Objects.GL.Branch, On<FixedAsset.branchID, Equal<PX.Objects.GL.Branch.branchID>>, InnerJoin<FABook, On<FABookHistory.bookID, Equal<FABook.bookID>>, InnerJoin<FABookPeriod, On<FABookPeriod.bookID, Equal<FABookHistory.bookID>, And<FABookPeriod.organizationID, Equal<IIf<Where<FABook.updateGL, Equal<True>>, PX.Objects.GL.Branch.organizationID, FinPeriod.organizationID.masterValue>>, And<FABookPeriod.finPeriodID, GreaterEqual<FABookHistory.finPeriodID>>>>>>>>, Aggregate<GroupBy<FABookHistory.assetID, GroupBy<FABookHistory.bookID, Max<FABookHistory.finPeriodID, GroupBy<FABookPeriod.finPeriodID>>>>>>))]
[PXCacheName("FA History by Period")]
[Serializable]
public class FAHistoryByPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AssetID;
  protected int? _BookID;
  protected 
  #nullable disable
  string _FinPeriodID;
  protected string _LastActivityPeriod;

  [PXDBInt(IsKey = true, BqlField = typeof (FABookHistory.assetID))]
  public virtual int? AssetID
  {
    get => this._AssetID;
    set => this._AssetID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (FABookHistory.bookID))]
  public virtual int? BookID
  {
    get => this._BookID;
    set => this._BookID = value;
  }

  [FABookPeriodID(typeof (FAHistoryByPeriod.bookID), null, true, typeof (FAHistoryByPeriod.assetID), null, null, null, null, null, IsKey = true, BqlField = typeof (FABookPeriod.finPeriodID))]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [FABookPeriodID(typeof (FAHistoryByPeriod.bookID), null, true, typeof (FAHistoryByPeriod.assetID), null, null, null, null, null, BqlField = typeof (FABookHistory.finPeriodID))]
  public virtual string LastActivityPeriod
  {
    get => this._LastActivityPeriod;
    set => this._LastActivityPeriod = value;
  }

  public class PK : 
    PrimaryKeyOf<FAHistoryByPeriod>.By<FAHistoryByPeriod.assetID, FAHistoryByPeriod.bookID, FAHistoryByPeriod.finPeriodID>
  {
    public static FAHistoryByPeriod Find(
      PXGraph graph,
      int? assetID,
      int? bookID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (FAHistoryByPeriod) PrimaryKeyOf<FAHistoryByPeriod>.By<FAHistoryByPeriod.assetID, FAHistoryByPeriod.bookID, FAHistoryByPeriod.finPeriodID>.FindBy(graph, (object) assetID, (object) bookID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class FixedAsset : 
      PrimaryKeyOf<FixedAsset>.By<FixedAsset.assetID>.ForeignKeyOf<FAHistoryByPeriod>.By<FAHistoryByPeriod.assetID>
    {
    }

    public class Book : 
      PrimaryKeyOf<FABook>.By<FABook.bookID>.ForeignKeyOf<FAHistoryByPeriod>.By<FAHistoryByPeriod.bookID>
    {
    }
  }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAHistoryByPeriod.assetID>
  {
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAHistoryByPeriod.bookID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAHistoryByPeriod.finPeriodID>
  {
  }

  public abstract class lastActivityPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAHistoryByPeriod.lastActivityPeriod>
  {
  }
}
