// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.PMWipForecastHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.CN;

/// <exclude />
[PXCacheName("PM WIP Forecast History")]
[PXProjection(typeof (Select2<PMForecastHistory, InnerJoin<PMAccountGroup, On<PMForecastHistory.accountGroupID, Equal<PMAccountGroup.groupID>>>>))]
[Serializable]
public class PMWipForecastHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (PMForecastHistory.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMForecastHistory.projectTaskID))]
  public virtual int? ProjectTaskID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMForecastHistory.accountGroupID))]
  public virtual int? AccountGroupID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMForecastHistory.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMForecastHistory.costCodeID))]
  public virtual int? CostCodeID { get; set; }

  [PXDBString(IsKey = true, BqlField = typeof (PMForecastHistory.periodID))]
  public virtual 
  #nullable disable
  string PeriodID { get; set; }

  [PXDBString(1, BqlField = typeof (PMAccountGroup.type))]
  public virtual string Type { get; set; }

  [PXDBBool(BqlField = typeof (PMAccountGroup.isExpense))]
  public virtual bool? IsExpense { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<PMWipForecastHistory.type, Equal<AccountType.expense>, Or<PMWipForecastHistory.isExpense, Equal<boolTrue>>>, PMForecastHistory.curyActualAmount>, decimal0>), typeof (Decimal))]
  public virtual Decimal? ActualCostAmount { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<PMWipForecastHistory.type, Equal<AccountType.income>>, PMForecastHistory.curyActualAmount>, decimal0>), typeof (Decimal))]
  public virtual Decimal? ActualRevenueAmount { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<PMWipForecastHistory.type, Equal<AccountType.income>>, PMForecastHistory.curyArAmount>, decimal0>), typeof (Decimal))]
  public virtual Decimal? ArRevenueAmount { get; set; }

  /// <summary>The inclusive tax amount.</summary>
  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<PMWipForecastHistory.type, Equal<AccountType.income>>, PMForecastHistory.curyInclTaxAmount>, decimal0>), typeof (Decimal))]
  public virtual Decimal? ArRevenueInclTaxAmount { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Mult<Switch<Case<Where<PMWipForecastHistory.type, Equal<AccountType.income>>, decimal1, Case<Where<PMWipForecastHistory.type, Equal<AccountType.liability>>, decimal1, Case<Where<PMWipForecastHistory.type, Equal<AccountType.asset>>, decimal_1, Case<Where<PMWipForecastHistory.type, Equal<AccountType.expense>>, decimal_1>>>>, decimal0>, PMForecastHistory.curyArAmount>), typeof (Decimal))]
  public virtual Decimal? CuryARAllAmount { get; set; }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipForecastHistory.projectID>
  {
  }

  public abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipForecastHistory.projectTaskID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipForecastHistory.accountGroupID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipForecastHistory.inventoryID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipForecastHistory.costCodeID>
  {
  }

  public abstract class periodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWipForecastHistory.periodID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWipForecastHistory.type>
  {
  }

  public abstract class isExpense : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMWipForecastHistory.isExpense>
  {
  }

  public abstract class actualCostAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipForecastHistory.actualCostAmount>
  {
  }

  public abstract class actualRevenueAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipForecastHistory.actualRevenueAmount>
  {
  }

  public abstract class arRevenueAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipForecastHistory.arRevenueAmount>
  {
  }

  public abstract class arRevenueInclTaxAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipForecastHistory.arRevenueInclTaxAmount>
  {
  }

  public abstract class curyARAllAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipForecastHistory.curyARAllAmount>
  {
  }
}
