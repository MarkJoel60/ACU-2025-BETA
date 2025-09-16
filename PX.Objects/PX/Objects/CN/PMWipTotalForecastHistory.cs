// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.PMWipTotalForecastHistory
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
[PXCacheName("PM WIP Total Forecast History")]
[PXProjection(typeof (Select4<PMWipForecastHistory, Aggregate<GroupBy<PMWipForecastHistory.projectID, GroupBy<PMWipForecastHistory.periodID, Sum<PMWipForecastHistory.actualCostAmount, Sum<PMWipForecastHistory.actualRevenueAmount, Sum<PMWipForecastHistory.arRevenueAmount, Sum<PMWipForecastHistory.arRevenueInclTaxAmount, Sum<PMWipForecastHistory.curyARAllAmount>>>>>>>>>))]
[Serializable]
public class PMWipTotalForecastHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (PMWipForecastHistory.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBString(IsKey = true, BqlField = typeof (PMWipForecastHistory.periodID))]
  public virtual 
  #nullable disable
  string PeriodID { get; set; }

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

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipTotalForecastHistory.projectID>
  {
  }

  public abstract class periodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWipTotalForecastHistory.periodID>
  {
  }

  public abstract class actualCostAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipTotalForecastHistory.actualCostAmount>
  {
  }

  public abstract class actualRevenueAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipTotalForecastHistory.actualRevenueAmount>
  {
  }

  public abstract class arRevenueAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipTotalForecastHistory.arRevenueAmount>
  {
  }

  public abstract class arRevenueInclTaxAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipTotalForecastHistory.arRevenueInclTaxAmount>
  {
  }

  public abstract class curyARAllAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipTotalForecastHistory.curyARAllAmount>
  {
  }
}
