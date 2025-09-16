// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMForecastProject
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.PM;

[PXCacheName("Project")]
[PXProjection(typeof (Select2<PMProject, LeftJoin<PMProjectRevenueTotal, On<PMProjectRevenueTotal.projectID, Equal<PMProject.contractID>>, LeftJoin<PMProjectCostForecastTotal, On<PMProjectCostForecastTotal.projectID, Equal<PMProject.contractID>>>>>))]
[Serializable]
public class PMForecastProject : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="P:PX.Objects.PM.PMProject.ContractID" />
  [PXDBInt(IsKey = true, BqlField = typeof (PMProject.contractID))]
  public virtual int? ContractID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProjectRevenueTotal.CuryRevisedAmount" />
  [PXDBBaseCury(BqlField = typeof (PMProjectRevenueTotal.curyRevisedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted Revenue", Enabled = false)]
  public virtual Decimal? TotalBudgetedRevenueAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProjectCostForecastTotal.CuryCommittedAmount" />
  [PXDBBaseCury(BqlField = typeof (PMProjectCostForecastTotal.curyCommittedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Cost")]
  public virtual Decimal? CuryCommittedCostAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProjectCostForecastTotal.CuryActualAmount" />
  [PXDBBaseCury(BqlField = typeof (PMProjectCostForecastTotal.curyActualAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Cost", Enabled = false)]
  public virtual Decimal? CuryActualCostAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProjectCostForecastTotal.CuryCommittedInvoicedAmount" />
  [PXDBBaseCury(BqlField = typeof (PMProjectCostForecastTotal.curyCommittedInvoicedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Invoiced Cost")]
  public virtual Decimal? CuryCommittedInvoicedCostAmount { get; set; }

  [PXDBBaseCury(BqlField = typeof (PMProjectCostForecastTotal.curyRevisedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost at Completion", Enabled = false)]
  public virtual Decimal? TotalBudgetedCostAmount { get; set; }

  [PXFormula(typeof (Add<IsNull<PMForecastProject.curyActualCostAmount, decimal0>, Sub<IsNull<PMForecastProject.curyCommittedCostAmount, decimal0>, IsNull<PMForecastProject.curyCommittedInvoicedCostAmount, decimal0>>>))]
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual + Committed Costs", Enabled = false)]
  public virtual Decimal? TotalBudgetedCompletedAmount { get; set; }

  [PXFormula(typeof (Sub<IsNull<PMForecastProject.totalBudgetedCostAmount, decimal0>, IsNull<PMForecastProject.totalBudgetedCompletedAmount, decimal0>>))]
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost to Complete")]
  public virtual Decimal? TotalBudgetedAmountToComplete { get; set; }

  [PXFormula(typeof (Sub<IsNull<PMForecastProject.totalBudgetedRevenueAmount, decimal0>, IsNull<PMForecastProject.totalBudgetedCostAmount, decimal0>>))]
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Gross Profit")]
  public virtual Decimal? TotalBudgetedGrossProfit { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost Variance")]
  public virtual Decimal? TotalBudgetedVarianceAmount { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Gross Profit")]
  public virtual Decimal? TotalProjectedGrossProfit { get; set; }

  public abstract class contractID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  PMForecastProject.contractID>
  {
  }

  public abstract class totalBudgetedRevenueAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastProject.totalBudgetedRevenueAmount>
  {
  }

  public abstract class curyCommittedCostAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastProject.curyCommittedCostAmount>
  {
  }

  public abstract class curyActualCostAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastProject.curyActualCostAmount>
  {
  }

  public abstract class curyCommittedInvoicedCostAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastProject.curyCommittedInvoicedCostAmount>
  {
  }

  public abstract class totalBudgetedCostAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastProject.totalBudgetedCostAmount>
  {
  }

  public abstract class totalBudgetedCompletedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastProject.totalBudgetedCompletedAmount>
  {
  }

  public abstract class totalBudgetedAmountToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastProject.totalBudgetedAmountToComplete>
  {
  }

  public abstract class totalBudgetedGrossProfit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastProject.totalBudgetedGrossProfit>
  {
  }

  public abstract class totalBudgetedVarianceAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastProject.totalBudgetedVarianceAmount>
  {
  }

  public abstract class totalProjectedGrossProfit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastProject.totalProjectedGrossProfit>
  {
  }
}
