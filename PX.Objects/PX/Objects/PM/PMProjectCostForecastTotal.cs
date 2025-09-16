// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProjectCostForecastTotal
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>Aggregate Total Sum of a Project for Cost Forecast.</summary>
[PXCacheName("Contract Total")]
[PXProjection(typeof (Select4<PMBudget, Where<PMBudget.type, Equal<AccountType.expense>>, Aggregate<GroupBy<PMBudget.projectID, Sum<PMBudget.curyRevisedAmount, Sum<PMBudget.curyCommittedAmount, Sum<PMBudget.curyActualAmount, Sum<PMBudget.curyCommittedInvoicedAmount, Sum<PMBudget.curyCostProjectionCostAtCompletion, Sum<PMBudget.curyChangeOrderAmount, Sum<PMBudget.curyAmount, Sum<PMBudget.curyCommittedOrigAmount>>>>>>>>>>>))]
public class PMProjectCostForecastTotal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>Project</summary>
  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.projectID))]
  public virtual int? ProjectID { get; set; }

  /// <summary>Revised Contract Total</summary>
  [PXDBBaseCury(BqlField = typeof (PMBudget.curyRevisedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost at Completion", Enabled = false)]
  public virtual Decimal? CuryRevisedAmount { get; set; }

  /// <summary>Total Revised Commited Amount for Project</summary>
  [PXDBBaseCury(BqlField = typeof (PMBudget.curyCommittedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Cost")]
  public virtual Decimal? CuryCommittedAmount { get; set; }

  /// <summary>Total Actual Amount for Project</summary>
  [PXDBBaseCury(BqlField = typeof (PMBudget.curyActualAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Cost", Enabled = false)]
  public virtual Decimal? CuryActualAmount { get; set; }

  /// <summary>Total Committed Invoiced Amount for Project</summary>
  [PXDBBaseCury(BqlField = typeof (PMBudget.curyCommittedInvoicedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Invoiced Cost")]
  public virtual Decimal? CuryCommittedInvoicedAmount { get; set; }

  /// <summary>Total Projected Cost at Completion</summary>
  [PXDBBaseCury(BqlField = typeof (PMBudget.curyCostProjectionCostAtCompletion))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Cost at Completion")]
  public virtual Decimal? CuryCostProjectionCostAtCompletion { get; set; }

  [PXFormula(typeof (Add<PMProjectCostForecastTotal.curyActualAmount, Sub<PMProjectCostForecastTotal.curyCommittedAmount, PMProjectCostForecastTotal.curyCommittedInvoicedAmount>>))]
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual + Committed Costs", Enabled = false)]
  public virtual Decimal? CompletedAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.CuryChangeOrderAmount" />
  [PXDBBaseCury(BqlField = typeof (PMBudget.curyChangeOrderAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted CO Amount", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CuryChangeOrderAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.CuryAmount" />
  [PXDBBaseCury(BqlField = typeof (PMBudget.curyAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Amount")]
  public virtual Decimal? CuryAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.CuryCommittedOrigAmount" />
  [PXDBBaseCury(BqlField = typeof (PMBudget.curyCommittedOrigAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Committed Amount", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CuryCommittedOrigAmount { get; set; }

  /// <exclude />
  public abstract class projectID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  PMProjectCostForecastTotal.projectID>
  {
  }

  /// <exclude />
  public abstract class curyRevisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectCostForecastTotal.curyRevisedAmount>
  {
  }

  /// <exclude />
  public abstract class curyCommittedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectCostForecastTotal.curyCommittedAmount>
  {
  }

  /// <exclude />
  public abstract class curyActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectCostForecastTotal.curyActualAmount>
  {
  }

  /// <exclude />
  public abstract class curyCommittedInvoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectCostForecastTotal.curyCommittedInvoicedAmount>
  {
  }

  /// <exclude />
  public abstract class curyCostProjectionCostAtCompletion : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectCostForecastTotal.curyCostProjectionCostAtCompletion>
  {
  }

  public abstract class completedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectCostForecastTotal.completedAmount>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.CuryChangeOrderAmount" />
  public abstract class curyChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectCostForecastTotal.curyChangeOrderAmount>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.CuryAmount" />
  public abstract class curyAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectCostForecastTotal.curyAmount>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.CuryCommittedOrigAmount" />
  public abstract class curyCommittedOrigAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectCostForecastTotal.curyCommittedOrigAmount>
  {
  }
}
