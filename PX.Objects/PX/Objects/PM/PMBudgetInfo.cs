// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMBudgetInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// The projection DAC that represents a <see cref="T:PX.Objects.PM.PMBudget">project budget line</see> connected with the <see cref="T:PX.Objects.PM.PMTask">project task</see> and the <see cref="T:PX.Objects.PM.PMAccountGroup">account group</see> associated with it.
/// </summary>
[PXHidden]
[PXProjection(typeof (Select2<PMBudget, InnerJoin<PMTask, On<PMBudget.projectID, Equal<PMTask.projectID>, And<PMBudget.projectTaskID, Equal<PMTask.taskID>>>, InnerJoin<PMAccountGroup, On<PMBudget.accountGroupID, Equal<PMAccountGroup.groupID>>>>>), new Type[] {typeof (PMBudget)})]
public class PMBudgetInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IProjectFilter
{
  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.ProjectID" />
  [PXDefault]
  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.projectID))]
  public virtual int? ProjectID { get; set; }

  /// <summary>Get or set Project TaskID</summary>
  public int? TaskID => this.ProjectTaskID;

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.ProjectTaskID" />
  [PXDimensionSelector("PROTASK", typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMBudgetInfo.projectID>>>>), typeof (PMTask.taskCD))]
  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.projectTaskID))]
  public virtual int? ProjectTaskID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.AccountGroupID" />
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new Type[] {typeof (PMAccountGroup.groupCD)})]
  [PXDimensionSelector("ACCGROUP", typeof (Search<PMAccountGroup.groupID>), typeof (PMAccountGroup.groupCD))]
  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.accountGroupID))]
  public virtual int? AccountGroupID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.InventoryID" />
  [PXDimensionSelector("INVENTORY", typeof (Search<PX.Objects.IN.InventoryItem.inventoryID>), typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.inventoryID))]
  public virtual int? InventoryID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.CostCodeID" />
  [PXDimensionSelector("COSTCODE", typeof (Search<PMCostCode.costCodeID>), typeof (PMCostCode.costCodeCD))]
  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.costCodeID))]
  public virtual int? CostCodeID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.CuryInfoID" />
  [PXDBLong(BqlField = typeof (PMBudget.curyInfoID))]
  [CurrencyInfo(typeof (PMProject.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.Description" />
  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (PMBudget.description))]
  [PXUIField(DisplayName = "Description")]
  public virtual 
  #nullable disable
  string Description { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.Qty" />
  [PXDBQuantity(BqlField = typeof (PMBudget.qty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Quantity")]
  public virtual Decimal? Qty { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.CuryAmount" />
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.amount), BqlField = typeof (PMBudget.curyAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Amount")]
  public virtual Decimal? CuryAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.Amount" />
  [PXDBBaseCury(BqlField = typeof (PMBudget.amount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Amount in Base Currency")]
  public virtual Decimal? Amount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.RevisedQty" />
  [PXDBQuantity(BqlField = typeof (PMBudget.revisedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Quantity")]
  public virtual Decimal? RevisedQty { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.CuryRevisedAmount" />
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.revisedAmount), BqlField = typeof (PMBudget.curyRevisedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Amount")]
  public virtual Decimal? CuryRevisedAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.RevisedAmount" />
  [PXDBBaseCury(BqlField = typeof (PMBudget.revisedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Amount in Base Currency")]
  public virtual Decimal? RevisedAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.ActualQty" />
  [PXDBQuantity(BqlField = typeof (PMBudget.actualQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Quantity", Enabled = false)]
  public virtual Decimal? ActualQty { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.CuryActualAmount" />
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.baseActualAmount), BqlField = typeof (PMBudget.curyActualAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Amount", Enabled = false)]
  public virtual Decimal? CuryActualAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.ActualAmount" />
  [PXDBBaseCury(BqlField = typeof (PMBudget.actualAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Amount in Base Currency", Enabled = false)]
  public virtual Decimal? ActualAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.DraftChangeOrderQty" />
  [PXDBQuantity(BqlField = typeof (PMBudget.draftChangeOrderQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Potential CO Quantity", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? DraftChangeOrderQty { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.CuryDraftChangeOrderAmount" />
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.draftChangeOrderAmount), BqlField = typeof (PMBudget.curyDraftChangeOrderAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Potential CO Amount", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CuryDraftChangeOrderAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.DraftChangeOrderAmount" />
  [PXDBBaseCury(BqlField = typeof (PMBudget.draftChangeOrderAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Potential CO Amount in Base Currency", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? DraftChangeOrderAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.ChangeOrderQty" />
  [PXDBQuantity(BqlField = typeof (PMBudget.changeOrderQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted CO Quantity", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? ChangeOrderQty { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.CuryChangeOrderAmount" />
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.changeOrderAmount), BqlField = typeof (PMBudget.curyChangeOrderAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted CO Amount", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CuryChangeOrderAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBudget.ChangeOrderAmount" />
  [PXDBBaseCury(BqlField = typeof (PMBudget.changeOrderAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted CO Amount in Base Currency", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? ChangeOrderAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAccountGroup.Type" />
  [PXDBString(1, BqlField = typeof (PMAccountGroup.type))]
  public virtual string AccountGroupType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAccountGroup.IsExpense" />
  [PXDBBool(BqlField = typeof (PMAccountGroup.isExpense))]
  public virtual bool? IsExpense { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMTask.PlannedStartDate" />
  [PXDBDate(BqlField = typeof (PMTask.plannedStartDate))]
  [PXUIField(DisplayName = "Planned Start Date", Enabled = false)]
  public virtual DateTime? PlannedStartDate { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMTask.PlannedEndDate" />
  [PXDBDate(BqlField = typeof (PMTask.plannedEndDate))]
  [PXUIField(DisplayName = "Planned End Date", Enabled = false)]
  public virtual DateTime? PlannedEndDate { get; set; }

  [PXDBTimestamp(BqlField = typeof (PMBudget.Tstamp))]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID(BqlField = typeof (PMBudget.createdByID))]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID(BqlField = typeof (PMBudget.createdByScreenID))]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  [PXDBCreatedDateTime(BqlField = typeof (PMBudget.createdDateTime))]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (PMBudget.lastModifiedByID))]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (PMBudget.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime(BqlField = typeof (PMBudget.lastModifiedDateTime))]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetInfo.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetInfo.projectTaskID>
  {
  }

  public abstract class accountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetInfo.accountGroupID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetInfo.inventoryID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudgetInfo.costCodeID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMBudgetInfo.curyInfoID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBudgetInfo.description>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudgetInfo.qty>
  {
  }

  public abstract class curyAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudgetInfo.curyAmount>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudgetInfo.amount>
  {
  }

  public abstract class revisedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudgetInfo.revisedQty>
  {
  }

  public abstract class curyRevisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetInfo.curyRevisedAmount>
  {
  }

  public abstract class revisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetInfo.revisedAmount>
  {
  }

  public abstract class actualQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudgetInfo.actualQty>
  {
  }

  public abstract class curyActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetInfo.curyActualAmount>
  {
  }

  public abstract class actualAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudgetInfo.actualAmount>
  {
  }

  public abstract class draftChangeOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetInfo.draftChangeOrderQty>
  {
  }

  public abstract class curyDraftChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetInfo.curyDraftChangeOrderAmount>
  {
  }

  public abstract class draftChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetInfo.draftChangeOrderAmount>
  {
  }

  public abstract class changeOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetInfo.changeOrderQty>
  {
  }

  public abstract class curyChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetInfo.curyChangeOrderAmount>
  {
  }

  public abstract class changeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudgetInfo.changeOrderAmount>
  {
  }

  public abstract class accountGroupType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBudgetInfo.accountGroupType>
  {
  }

  public abstract class isExpense : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMBudgetInfo.isExpense>
  {
  }

  public abstract class plannedStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMBudgetInfo.plannedStartDate>
  {
  }

  public abstract class plannedEndDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMBudgetInfo.plannedEndDate>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMBudgetInfo.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMBudgetInfo.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBudgetInfo.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMBudgetInfo.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMBudgetInfo.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBudgetInfo.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMBudgetInfo.lastModifiedDateTime>
  {
  }
}
