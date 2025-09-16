// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.PMForecastHistoryByPeriods
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.CN;

[PXHidden]
[PXProjection(typeof (Select5<PMForecastHistory, CrossJoin<FinPeriod>, Where<FinPeriod.finPeriodID, GreaterEqual<PMForecastHistoryByPeriods.minPeriod>>, Aggregate<GroupBy<PMForecastHistory.projectID, GroupBy<PMForecastHistory.projectTaskID, GroupBy<PMForecastHistory.costCodeID, GroupBy<PMForecastHistory.accountGroupID, GroupBy<FinPeriod.finPeriodID>>>>>>>))]
[Serializable]
public class PMForecastHistoryByPeriods : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (PMForecastHistory.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMForecastHistory.projectTaskID))]
  public virtual int? ProjectTaskID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMForecastHistory.costCodeID))]
  public virtual int? CostCodeID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMForecastHistory.accountGroupID))]
  public virtual int? AccountGroupID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (FinPeriod.finPeriodID))]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMForecastHistoryByPeriods.projectID>
  {
  }

  public abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMForecastHistoryByPeriods.projectTaskID>
  {
  }

  public abstract class costCodeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMForecastHistoryByPeriods.costCodeID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMForecastHistoryByPeriods.accountGroupID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMForecastHistoryByPeriods.finPeriodID>
  {
  }

  public sealed class minPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PMForecastHistoryByPeriods.minPeriod>
  {
    public minPeriod()
      : base("201201")
    {
    }
  }
}
