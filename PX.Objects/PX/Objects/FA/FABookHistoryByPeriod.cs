// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookHistoryByPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXCacheName("FA Book History by Period")]
[PXProjection(typeof (Select5<FABookHistory, InnerJoin<FixedAsset, On<FABookHistory.assetID, Equal<FixedAsset.assetID>>, InnerJoin<PX.Objects.GL.Branch, On<FixedAsset.branchID, Equal<PX.Objects.GL.Branch.branchID>>, InnerJoin<FABook, On<FABookHistory.bookID, Equal<FABook.bookID>>, InnerJoin<FABookPeriod, On<FABookPeriod.bookID, Equal<FABookHistory.bookID>, And<FABookPeriod.organizationID, Equal<IIf<Where<FABook.updateGL, Equal<True>>, PX.Objects.GL.Branch.organizationID, FinPeriod.organizationID.masterValue>>, And<FABookPeriod.finPeriodID, GreaterEqual<FABookHistory.finPeriodID>>>>>>>>, Where<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>>, Aggregate<GroupBy<FABookHistory.assetID, GroupBy<FABookHistory.bookID, GroupBy<FABookPeriod.finPeriodID, Max<FABookHistory.finPeriodID>>>>>>))]
[Serializable]
public class FABookHistoryByPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (FABookHistory.assetID))]
  public virtual int? AssetID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (FABookHistory.bookID))]
  public virtual int? BookID { get; set; }

  [FABookPeriodID(typeof (FABookHistoryByPeriod.bookID), null, true, typeof (FABookHistoryByPeriod.assetID), null, null, null, null, null, IsKey = true, BqlField = typeof (FABookPeriod.finPeriodID))]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  [FABookPeriodID(typeof (FABookHistoryByPeriod.bookID), null, true, typeof (FABookHistoryByPeriod.assetID), null, null, null, null, null, BqlField = typeof (FABookHistory.finPeriodID))]
  [PXUIField]
  public virtual string LastActivityPeriod { get; set; }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookHistoryByPeriod.assetID>
  {
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookHistoryByPeriod.bookID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookHistoryByPeriod.finPeriodID>
  {
  }

  public abstract class lastActivityPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookHistoryByPeriod.lastActivityPeriod>
  {
  }
}
