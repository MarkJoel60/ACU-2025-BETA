// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMForecastRecord
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// The virtual DAC that represents a detail line of a project budget forecast.
/// The records of this type are created and edited through the Project Budget Forecast (PM209600) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.ForecastMaint" /> graph).
/// </summary>
[PXHidden]
[PXPrimaryGraph(typeof (ForecastMaint))]
public class PMForecastRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IProjectFilter
{
  public const 
  #nullable disable
  string SummaryFinPeriod = "000000";
  public const string TotalFinPeriod = "999998";
  public const string DifferenceFinPeriod = "999999";

  /// <inheritdoc cref="P:PX.Objects.PM.PMForecastDetail.ProjectID" />
  [PXUnboundDefault(typeof (PMForecast.projectID))]
  [PXInt(IsKey = true)]
  public virtual int? ProjectID { get; set; }

  /// <summary>Get or set Project TaskID</summary>
  public int? TaskID => this.ProjectTaskID;

  /// <inheritdoc cref="P:PX.Objects.PM.PMForecastDetail.ProjectTaskID" />
  [PXInt(IsKey = true)]
  [PXDimensionSelector("PROTASK", typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMForecastRecord.projectID>>>>), typeof (PMTask.taskCD))]
  public virtual int? ProjectTaskID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMForecastDetail.AccountGroupID" />
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new Type[] {typeof (PMAccountGroup.groupCD)})]
  [PXInt(IsKey = true)]
  [PXDimensionSelector("ACCGROUP", typeof (Search<PMAccountGroup.groupID>), typeof (PMAccountGroup.groupCD))]
  public virtual int? AccountGroupID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMForecastDetail.InventoryID" />
  [PXInt(IsKey = true)]
  [PXDimensionSelector("INVENTORY", typeof (Search<PX.Objects.IN.InventoryItem.inventoryID>), typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  public virtual int? InventoryID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMForecastDetail.CostCodeID" />
  [PXInt(IsKey = true)]
  [PXDimensionSelector("COSTCODE", typeof (Search<PMCostCode.costCodeID>), typeof (PMCostCode.costCodeCD))]
  public virtual int? CostCodeID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMForecastDetail.PeriodID" />
  [PXString(6, IsKey = true, IsFixed = true)]
  public virtual string FinPeriodID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMForecastDetail.ProjectTaskID" />
  [PXInt]
  [PXUIField(DisplayName = "Project Task")]
  [PXDimensionSelector("PROTASK", typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMForecastRecord.projectID>>>>), typeof (PMTask.taskCD))]
  public virtual int? ProjectTask { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMForecastDetail.AccountGroupID" />
  [PXInt]
  [PXUIField(DisplayName = "Account Group")]
  [PXDimensionSelector("ACCGROUP", typeof (Search<PMAccountGroup.groupID>), typeof (PMAccountGroup.groupCD))]
  public virtual int? AccountGroup { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMForecastDetail.InventoryID" />
  [PXInt]
  [PXUIField(DisplayName = "Inventory ID")]
  [PXDimensionSelector("INVENTORY", typeof (Search<PX.Objects.IN.InventoryItem.inventoryID>), typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  public virtual int? Inventory { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMForecastDetail.CostCodeID" />
  [PXInt]
  [PXUIField(DisplayName = "Cost Code", FieldClass = "COSTCODE")]
  [PXDimensionSelector("COSTCODE", typeof (Search<PMCostCode.costCodeID>), typeof (PMCostCode.costCodeCD))]
  public virtual int? CostCode { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMForecastDetail.PeriodID" />
  [PXString]
  [PXUIField(DisplayName = "Financial Period")]
  public virtual string Period { get; set; }

  /// <summary>The type of the associated account group.</summary>
  [PXString(1)]
  public virtual string AccountGroupType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMForecastDetail.Description" />
  [PXString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMForecastDetail.Qty" />
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Quantity")]
  public virtual Decimal? Qty { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMForecastDetail.CuryAmount" />
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Amount")]
  public virtual Decimal? CuryAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMForecastDetail.RevisedQty" />
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Quantity")]
  public virtual Decimal? RevisedQty { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMForecastDetail.CuryRevisedAmount" />
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Amount")]
  public virtual Decimal? CuryRevisedAmount { get; set; }

  /// <summary>
  /// The total quantity of the lines of the released AR invoices that correspond to the project budget line.
  /// </summary>
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Quantity", Enabled = false)]
  public virtual Decimal? ActualQty { get; set; }

  /// <summary>
  /// The total amount of the lines of the released AR invoices that correspond to the project budget line.
  /// </summary>
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMForecastRecord.actualAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Amount", Enabled = false)]
  public virtual Decimal? CuryActualAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMForecastRecord.CuryActualAmount">actual amount</see> in the base currency.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Amount in Base Currency", Enabled = false)]
  public virtual Decimal? ActualAmount { get; set; }

  /// <summary>
  /// The total quantity of the potential changes to the project budget.
  /// </summary>
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Potential CO Quantity", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? DraftChangeOrderQty { get; set; }

  /// <summary>
  /// The total amount of the potential changes to the project budget.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Potential CO Amount", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CuryDraftChangeOrderAmount { get; set; }

  /// <summary>
  /// The total quantity of the lines of released change orders that are associated with the project budget line.
  /// </summary>
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted CO Quantity", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? ChangeOrderQty { get; set; }

  /// <summary>
  /// The total amount of the lines of released change orders that are associated with the project budget line.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted CO Amount", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CuryChangeOrderAmount { get; set; }

  /// <summary>
  /// The difference between the <see cref="P:PX.Objects.PM.PMForecastRecord.CuryRevisedAmount">revised budgeted amount</see> and the <see cref="P:PX.Objects.PM.PMForecastRecord.CuryActualAmount">actual amount</see>.
  /// </summary>
  [PXBaseCury]
  [PXUIField(DisplayName = "Revised Amount - Actual Amount", Enabled = false)]
  public virtual Decimal? CuryVarianceAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMForecastRecord.curyRevisedAmount), typeof (PMForecastRecord.curyActualAmount)})] get
    {
      if (!this.CuryActualAmount.HasValue)
        return new Decimal?();
      Decimal? curyRevisedAmount = this.CuryRevisedAmount;
      Decimal? curyActualAmount = this.CuryActualAmount;
      return !(curyRevisedAmount.HasValue & curyActualAmount.HasValue) ? new Decimal?() : new Decimal?(curyRevisedAmount.GetValueOrDefault() - curyActualAmount.GetValueOrDefault());
    }
  }

  /// <summary>
  /// The difference between the <see cref="P:PX.Objects.PM.PMForecastRecord.RevisedQty">revised budgeted quantity</see> and the <see cref="P:PX.Objects.PM.PMForecastRecord.ActualQty">actual quantity</see>.
  /// </summary>
  [PXQuantity]
  [PXUIField(DisplayName = "Revised Quantity - Actual Quantity", Enabled = false)]
  public virtual Decimal? VarianceQuantity
  {
    [PXDependsOnFields(new Type[] {typeof (PMForecastRecord.revisedQty), typeof (PMForecastRecord.actualQty)})] get
    {
      if (!this.ActualQty.HasValue)
        return new Decimal?();
      Decimal? revisedQty = this.RevisedQty;
      Decimal? actualQty = this.ActualQty;
      return !(revisedQty.HasValue & actualQty.HasValue) ? new Decimal?() : new Decimal?(revisedQty.GetValueOrDefault() - actualQty.GetValueOrDefault());
    }
  }

  public bool IsSummary => this.FinPeriodID == "000000";

  public bool IsTotal => this.FinPeriodID == "999998";

  public bool IsDifference => this.FinPeriodID == "999999";

  /// <summary>The date when the project task is expected to start.</summary>
  [PXDate]
  [PXUIField(DisplayName = "Planned Start Date", Enabled = false)]
  public virtual DateTime? PlannedStartDate { get; set; }

  /// <summary>The date when the project task is expected to end.</summary>
  [PXDate]
  [PXUIField(DisplayName = "Planned End Date", Enabled = false)]
  public virtual DateTime? PlannedEndDate { get; set; }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMForecastRecord.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMForecastRecord.projectTaskID>
  {
  }

  public abstract class accountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMForecastRecord.accountGroupID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMForecastRecord.inventoryID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMForecastRecord.costCodeID>
  {
  }

  public abstract class periodID : IBqlField, IBqlOperand
  {
  }

  public abstract class projectTask : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMForecastRecord.projectTask>
  {
  }

  public abstract class accountGroup : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMForecastRecord.accountGroup>
  {
  }

  public abstract class inventory : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMForecastRecord.inventory>
  {
  }

  public abstract class costCode : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMForecastRecord.costCode>
  {
  }

  public abstract class period : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMForecastRecord.period>
  {
  }

  public abstract class accountGroupType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMForecastRecord.accountGroupType>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMForecastRecord.description>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMForecastRecord.qty>
  {
  }

  public abstract class curyAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMForecastRecord.curyAmount>
  {
  }

  public abstract class revisedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMForecastRecord.revisedQty>
  {
  }

  public abstract class curyRevisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastRecord.curyRevisedAmount>
  {
  }

  public abstract class actualQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMForecastRecord.actualQty>
  {
  }

  public abstract class curyActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastRecord.curyActualAmount>
  {
  }

  public abstract class actualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastRecord.actualAmount>
  {
  }

  public abstract class draftChangeOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastRecord.draftChangeOrderQty>
  {
  }

  public abstract class curyDraftChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastRecord.curyDraftChangeOrderAmount>
  {
  }

  public abstract class changeOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastRecord.changeOrderQty>
  {
  }

  public abstract class curyChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastRecord.curyChangeOrderAmount>
  {
  }

  public abstract class curyVarianceAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastRecord.curyVarianceAmount>
  {
  }

  public abstract class varianceQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastRecord.varianceQuantity>
  {
  }

  public abstract class plannedStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMForecastRecord.plannedStartDate>
  {
  }

  public abstract class plannedEndDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMForecastRecord.plannedEndDate>
  {
  }
}
