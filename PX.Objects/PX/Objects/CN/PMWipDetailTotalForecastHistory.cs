// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.PMWipDetailTotalForecastHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CN;

/// <exclude />
[PXCacheName("PM WIP Detail Total Forecast History")]
[PXProjection(typeof (Select4<PMWipForecastHistory, Aggregate<GroupBy<PMWipForecastHistory.projectID, GroupBy<PMWipForecastHistory.projectTaskID, GroupBy<PMWipForecastHistory.costCodeID, GroupBy<PMWipForecastHistory.accountGroupID, GroupBy<PMWipForecastHistory.periodID, Sum<PMWipForecastHistory.actualCostAmount, Sum<PMWipForecastHistory.actualRevenueAmount, Sum<PMWipForecastHistory.arRevenueAmount, Sum<PMWipForecastHistory.arRevenueInclTaxAmount, Sum<PMWipForecastHistory.curyARAllAmount>>>>>>>>>>>>))]
[Serializable]
public class PMWipDetailTotalForecastHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (PMWipForecastHistory.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMWipForecastHistory.projectTaskID))]
  public virtual int? ProjectTaskID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMWipForecastHistory.costCodeID))]
  public virtual int? CostCodeID { get; set; }

  [PXDBString(IsKey = true, BqlField = typeof (PMWipForecastHistory.periodID))]
  public virtual 
  #nullable disable
  string PeriodID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMWipForecastHistory.accountGroupID))]
  public virtual int? AccountGroupID { get; set; }

  [PXDBDecimal(BqlField = typeof (PMWipForecastHistory.actualCostAmount))]
  public virtual Decimal? ActualCostAmount { get; set; }

  [PXDBDecimal(BqlField = typeof (PMWipForecastHistory.actualRevenueAmount))]
  public virtual Decimal? ActualRevenueAmount { get; set; }

  [PXDBDecimal(BqlField = typeof (PMWipForecastHistory.arRevenueAmount))]
  public virtual Decimal? ArRevenueAmount { get; set; }

  /// <summary>The inclusive tax amount.</summary>
  [PXDBDecimal(BqlField = typeof (PMWipForecastHistory.arRevenueInclTaxAmount))]
  public virtual Decimal? ArRevenueInclTaxAmount { get; set; }

  [PXDBDecimal(BqlField = typeof (PMWipForecastHistory.curyARAllAmount))]
  public virtual Decimal? CuryARAllAmount { get; set; }

  public abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipDetailTotalForecastHistory.projectID>
  {
  }

  public abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipDetailTotalForecastHistory.projectTaskID>
  {
  }

  public abstract class costCodeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipDetailTotalForecastHistory.costCodeID>
  {
  }

  public abstract class periodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWipDetailTotalForecastHistory.periodID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipDetailTotalForecastHistory.accountGroupID>
  {
  }

  public abstract class actualCostAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipDetailTotalForecastHistory.actualCostAmount>
  {
  }

  public abstract class actualRevenueAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipDetailTotalForecastHistory.actualRevenueAmount>
  {
  }

  public abstract class arRevenueAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipDetailTotalForecastHistory.arRevenueAmount>
  {
  }

  public abstract class arRevenueInclTaxAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipDetailTotalForecastHistory.arRevenueInclTaxAmount>
  {
  }

  public abstract class curyARAllAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipDetailTotalForecastHistory.curyARAllAmount>
  {
  }
}
