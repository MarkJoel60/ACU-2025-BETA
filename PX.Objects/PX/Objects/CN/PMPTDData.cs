// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.PMPTDData
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.CN;

[PXHidden]
[PXProjection(typeof (Select5<PMForecastHistoryByPeriods, LeftJoin<PMForecastHistory, On<PMForecastHistory.projectID, Equal<PMForecastHistoryByPeriods.projectID>, And<PMForecastHistory.projectTaskID, Equal<PMForecastHistoryByPeriods.projectTaskID>, And<PMForecastHistory.costCodeID, Equal<PMForecastHistoryByPeriods.costCodeID>, And<PMForecastHistory.accountGroupID, Equal<PMForecastHistoryByPeriods.accountGroupID>, And<PMForecastHistory.periodID, LessEqual<PMForecastHistoryByPeriods.finPeriodID>>>>>>>, Aggregate<GroupBy<PMForecastHistoryByPeriods.projectID, GroupBy<PMForecastHistoryByPeriods.projectTaskID, GroupBy<PMForecastHistoryByPeriods.costCodeID, GroupBy<PMForecastHistoryByPeriods.accountGroupID, GroupBy<PMForecastHistoryByPeriods.finPeriodID, Sum<PMForecastHistory.curyActualAmount>>>>>>>>))]
[Serializable]
public class PMPTDData : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (PMForecastHistoryByPeriods.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMForecastHistoryByPeriods.projectTaskID))]
  public virtual int? ProjectTaskID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMForecastHistoryByPeriods.costCodeID))]
  public virtual int? CostCodeID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMForecastHistoryByPeriods.accountGroupID))]
  public virtual int? AccountGroupID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (PMForecastHistoryByPeriods.finPeriodID))]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  [PXDBBaseCury(BqlField = typeof (PMForecastHistory.curyActualAmount))]
  public virtual Decimal? FinPTDAmount { get; set; }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMPTDData.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMPTDData.projectTaskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMPTDData.costCodeID>
  {
  }

  public abstract class accountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMPTDData.accountGroupID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMPTDData.finPeriodID>
  {
  }

  public abstract class finPTDAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMPTDData.finPTDAmount>
  {
  }
}
