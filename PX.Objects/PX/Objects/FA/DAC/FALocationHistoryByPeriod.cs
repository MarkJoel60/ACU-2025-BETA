// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DAC.FALocationHistoryByPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.FA.DAC;

[PXProjection(typeof (Select5<PX.Objects.FA.FixedAsset, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.FA.FixedAsset.branchID, Equal<PX.Objects.GL.Branch.branchID>>, InnerJoin<FALocationHistory, On<FALocationHistory.assetID, Equal<PX.Objects.FA.FixedAsset.assetID>, And<PX.Objects.FA.FixedAsset.recordType, Equal<FARecordType.assetType>>>, InnerJoin<FABook, On<FABook.updateGL, Equal<True>>, InnerJoin<FABookPeriod, On<FABookPeriod.bookID, Equal<FABook.bookID>, And<FABookPeriod.organizationID, Equal<PX.Objects.GL.Branch.organizationID>, And<FALocationHistory.periodID, LessEqual<FABookPeriod.finPeriodID>>>>>>>>, Aggregate<GroupBy<FALocationHistory.assetID, GroupBy<FABookPeriod.finPeriodID, Max<FALocationHistory.periodID, Max<FALocationHistory.revisionID>>>>>>))]
[PXCacheName("FA Location History by Period")]
[Serializable]
public class FALocationHistoryByPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AssetID;
  protected 
  #nullable disable
  string _PeriodID;
  protected string _LastPeriodID;
  protected int? _LastRevisionID;

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.FA.FixedAsset.assetID))]
  [PXDefault]
  public virtual int? AssetID
  {
    get => this._AssetID;
    set => this._AssetID = value;
  }

  [FABookPeriodID(null, null, true, typeof (FALocationHistoryByPeriod.assetID), null, null, null, null, null, BqlField = typeof (FABookPeriod.finPeriodID))]
  [PXDefault]
  public virtual string PeriodID
  {
    get => this._PeriodID;
    set => this._PeriodID = value;
  }

  [FABookPeriodID(null, null, true, typeof (FALocationHistoryByPeriod.assetID), null, null, null, null, null, BqlField = typeof (FALocationHistory.periodID))]
  [PXDefault]
  public virtual string LastPeriodID
  {
    get => this._LastPeriodID;
    set => this._LastPeriodID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (FALocationHistory.revisionID))]
  [PXDefault(0)]
  public virtual int? LastRevisionID
  {
    get => this._LastRevisionID;
    set => this._LastRevisionID = value;
  }

  public class PK : 
    PrimaryKeyOf<FALocationHistoryByPeriod>.By<FALocationHistoryByPeriod.assetID, FALocationHistoryByPeriod.periodID>
  {
    public static FALocationHistoryByPeriod Find(
      PXGraph graph,
      int? assetID,
      string periodID,
      PKFindOptions options = 0)
    {
      return (FALocationHistoryByPeriod) PrimaryKeyOf<FALocationHistoryByPeriod>.By<FALocationHistoryByPeriod.assetID, FALocationHistoryByPeriod.periodID>.FindBy(graph, (object) assetID, (object) periodID, options);
    }
  }

  public static class FK
  {
    public class FixedAsset : 
      PrimaryKeyOf<PX.Objects.FA.FixedAsset>.By<PX.Objects.FA.FixedAsset.assetID>.ForeignKeyOf<FALocationHistoryByPeriod>.By<FALocationHistoryByPeriod.assetID>
    {
    }
  }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FALocationHistoryByPeriod.assetID>
  {
  }

  public abstract class periodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FALocationHistoryByPeriod.periodID>
  {
  }

  public abstract class lastPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FALocationHistoryByPeriod.lastPeriodID>
  {
  }

  public abstract class lastRevisionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FALocationHistoryByPeriod.lastRevisionID>
  {
  }
}
