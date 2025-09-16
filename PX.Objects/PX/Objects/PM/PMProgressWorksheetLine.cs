// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProgressWorksheetLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.TM;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Contains the main properties of a progress worksheet line. The records of this type are created and edited through the <b>Details</b>
/// tab of the Progress Worksheets (PM303000) form (which corresponds to the <see cref="T:PX.Objects.PM.ProgressWorksheetEntry" /> graph).
/// </summary>
[PXCacheName("Progress Worksheet Line")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMProgressWorksheetLine : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IProjectFilter,
  IQuantify
{
  protected Guid? _NoteID;
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The original sequence number of the line among all the progress worksheet lines.
  /// </summary>
  /// <remarks>The sequence of line numbers of the progress worksheet lines that belong to a single document can include gaps.</remarks>
  [PXUIField(DisplayName = "Line Number", Visible = false)]
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (PMProgressWorksheet.lineCntr))]
  public virtual int? LineNbr { get; set; }

  /// <summary>
  /// The reference number of the parent <see cref="T:PX.Objects.PM.PMProgressWorksheet">progress worksheet</see>.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXSelector(typeof (Search<PMProgressWorksheet.refNbr>))]
  [PXUIField(DisplayName = "Worksheet Nbr.", Enabled = false)]
  [PXDBDefault(typeof (PMProgressWorksheet.refNbr))]
  [PXParent(typeof (Select<PMProgressWorksheet, Where<PMProgressWorksheet.refNbr, Equal<Current<PMProgressWorksheetLine.refNbr>>>>))]
  public virtual string RefNbr { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the progress worksheet line.
  /// </summary>
  /// <value>
  /// By default, the value of this field is set to the <see cref="P:PX.Objects.PM.PMProgressWorksheet.ProjectID">project ID</see> of the parent progress worksheet.
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXDBInt]
  [PXDefault(typeof (PMProgressWorksheet.projectID))]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">task</see> associated with the progress worksheet line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMTask.TaskID" /> field.
  /// </value>
  [PXRestrictor(typeof (Where<PMTask.status, Equal<ProjectTaskStatus.active>, Or<PMTask.status, Equal<ProjectTaskStatus.planned>>>), "The {0} project task has the {1} status and cannot be selected. Select another project task.", new Type[] {typeof (PMTask.taskCD), typeof (PMTask.status)})]
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMProgressWorksheetLine.projectID>>, And<PMTask.isDefault, Equal<True>, And<PMTask.status, NotEqual<ProjectTaskStatus.canceled>, And<PMTask.status, NotEqual<ProjectTaskStatus.completed>>>>>>))]
  [ActiveOrInPlanningProjectTask(typeof (PMProgressWorksheetLine.projectID), DisplayName = "Project Task")]
  [PXForeignReference(typeof (Field<PMProgressWorksheetLine.taskID>.IsRelatedTo<PMTask.taskID>))]
  public virtual int? TaskID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> associated with the progress worksheet line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [PXDefault]
  [PXUIField(DisplayName = "Inventory ID")]
  [PXDBInt]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMCostBudget>.On<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<PMCostBudget.inventoryID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostBudget.projectID, Equal<BqlField<PMProject.contractID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<PMCostBudget.type, IBqlString>.IsEqual<AccountType.expense>>>>.And<BqlOperand<PMCostBudget.inventoryID, IBqlInt>.IsNotNull>>.Aggregate<To<GroupBy<PMCostBudget.inventoryID>>>, PX.Objects.IN.InventoryItem>.SearchFor<PX.Objects.IN.InventoryItem.inventoryID>), SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXForeignReference(typeof (Field<PMProgressWorksheetLine.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? InventoryID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">cost code</see> associated with the cost part of the estimation line.
  /// </summary>
  [PXDefault]
  [PXUIField(DisplayName = "Cost Code", FieldClass = "COSTCODE")]
  [PXDBInt]
  [PXRestrictor(typeof (Where<PMCostCode.isActive, Equal<True>>), "The {0} cost code is inactive.", new Type[] {typeof (PMCostCode.costCodeCD)})]
  [PXDimensionSelector("COSTCODE", typeof (FbqlSelect<SelectFromBase<PMCostCode, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMCostBudget>.On<BqlOperand<PMCostCode.costCodeID, IBqlInt>.IsEqual<PMCostBudget.costCodeID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostBudget.projectID, Equal<BqlField<PMProject.contractID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<PMCostBudget.type, IBqlString>.IsEqual<AccountType.expense>>>>.And<BqlOperand<PMCostBudget.costCodeID, IBqlInt>.IsNotNull>>.Aggregate<To<GroupBy<PMCostBudget.costCodeID>>>, PMCostCode>.SearchFor<PMCostCode.costCodeID>), typeof (PMCostCode.costCodeCD))]
  [PXForeignReference(typeof (Field<PMProgressWorksheetLine.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  public virtual int? CostCodeID { get; set; }

  /// <summary>The identifier of the <see cref="T:PX.Objects.PM.PMAccountGroup">account group</see> associated with the progress worksheet line.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
  /// </value>
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new Type[] {typeof (PMAccountGroup.groupCD)})]
  [PXDefault]
  [PXUIField(DisplayName = "Account Group")]
  [PXDBInt]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PMAccountGroup, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMCostBudget>.On<BqlOperand<PMAccountGroup.groupID, IBqlInt>.IsEqual<PMCostBudget.accountGroupID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostBudget.projectID, Equal<BqlField<PMProject.contractID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<PMCostBudget.type, IBqlString>.IsEqual<AccountType.expense>>>, And<BqlOperand<PMCostBudget.accountGroupID, IBqlInt>.IsNotNull>>>.And<BqlOperand<PMAccountGroup.isExpense, IBqlBool>.IsEqual<True>>>.Aggregate<To<GroupBy<PMCostBudget.accountGroupID>>>, PMAccountGroup>.SearchFor<PMAccountGroup.groupID>), SubstituteKey = typeof (PMAccountGroup.groupCD))]
  [PXForeignReference(typeof (Field<PMProgressWorksheetLine.accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  public virtual int? AccountGroupID { get; set; }

  /// <summary>The description of the progress worksheet line.</summary>
  [PXString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public virtual string Description { get; set; }

  /// <summary>The UOM of the progress worksheet line.</summary>
  [PXString(6, IsUnicode = true)]
  [PXUIField(DisplayName = "UOM", Enabled = false)]
  public virtual string UOM { get; set; }

  /// <summary>
  /// The completed quantity for the units that are produced or installed from the previous progress worksheet date.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Completed Quantity")]
  public virtual Decimal? Qty { get; set; }

  /// <summary>
  /// /// The sum of the values of the <see cref="P:PX.Objects.PM.PMProgressWorksheetLine.Qty">Completed Quantity</see> fields (for the lines with the same project key) of the progress worksheets that are released up to and including the current progress worksheet date. (The values of the current document are not included in the sum.)
  /// </summary>
  [PXQuantity]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Previously Completed Quantity", Enabled = false)]
  public virtual Decimal? PreviouslyCompletedQuantity { get; set; }

  /// <summary>
  /// The sum of the values of the <see cref="P:PX.Objects.PM.PMProgressWorksheetLine.Qty">Completed Quantity</see> fields for the released progress worksheets with dates within the previous financial period.
  /// </summary>
  [PXQuantity]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Prior Period Quantity", Enabled = false)]
  public virtual Decimal? PriorPeriodQuantity { get; set; }

  /// <summary>
  /// The sum of the values of the <see cref="P:PX.Objects.PM.PMProgressWorksheetLine.Qty">Completed Quantity</see> fields for the released progress worksheets with the dates that start from the first day of the current financial period and that end with the current date of the document. (The date that is specified in a progress worksheet should be a part of the current period.)
  /// </summary>
  [PXQuantity]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Current Period Quantity", Enabled = false)]
  public virtual Decimal? CurrentPeriodQuantity { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMProgressWorksheetLine.PreviouslyCompletedQuantity" /> and <see cref="P:PX.Objects.PM.PMProgressWorksheetLine.Qty" />.
  /// </summary>
  [PXQuantity]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Completed Quantity", Enabled = false)]
  public virtual Decimal? TotalCompletedQuantity { get; set; }

  /// <summary>
  /// The total dompleted quantity devided by the total budgeted quantity.
  /// </summary>
  [PXDecimal(2)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Completed (%), Total", Enabled = false)]
  public virtual Decimal? CompletedPercentTotalQuantity { get; set; }

  /// /// <summary>The total budgeted quantity.</summary>
  /// <value>
  /// The value of this field is equal to the revised budgeted quantity, which is specified on the Cost Budget tab of the Projects (PM301000) form for the project specified for this progress worksheet.
  /// </value>
  [PXQuantity]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Budgeted Quantity", Enabled = false)]
  public virtual Decimal? TotalBudgetedQuantity { get; set; }

  /// <summary>
  /// The employee associated with the progress worksheet line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds the value of the <see cref="T:PX.Objects.EP.EPEmployee.PK" /> field.
  /// </value>
  [PXUIField(DisplayName = "Employee", Visible = false)]
  [PXChildUpdatable(AutoRefresh = true)]
  [SubordinateOwnerEmployee(DisplayName = "Employee", Visible = false)]
  public virtual int? OwnerID { get; set; }

  /// <summary>
  /// The workgroup associated with the progress worksheet line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.TM.EPCompanyTree.WorkGroupID">EPCompanyTree.WorkGroupID</see> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Workgroup", Visible = false)]
  [PXWorkgroupSelector]
  public virtual int? WorkgroupID { get; set; }

  [PXNote(DescriptionField = typeof (PMProgressWorksheetLine.refNbr))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProgressWorksheetLine.lineNbr>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProgressWorksheetLine.refNbr>
  {
    public const int Length = 15;
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProgressWorksheetLine.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProgressWorksheetLine.taskID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProgressWorksheetLine.inventoryID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProgressWorksheetLine.costCodeID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProgressWorksheetLine.accountGroupID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProgressWorksheetLine.description>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProgressWorksheetLine.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProgressWorksheetLine.qty>
  {
  }

  public abstract class previouslyCompletedQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProgressWorksheetLine.previouslyCompletedQuantity>
  {
  }

  public abstract class priorPeriodQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProgressWorksheetLine.priorPeriodQuantity>
  {
  }

  public abstract class currentPeriodQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProgressWorksheetLine.currentPeriodQuantity>
  {
  }

  public abstract class totalCompletedQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProgressWorksheetLine.totalCompletedQuantity>
  {
  }

  public abstract class completedPercentTotalQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProgressWorksheetLine.completedPercentTotalQuantity>
  {
  }

  public abstract class totalBudgetedQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProgressWorksheetLine.totalBudgetedQuantity>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProgressWorksheetLine.ownerID>
  {
  }

  public abstract class workgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProgressWorksheetLine.workgroupID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMProgressWorksheetLine.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMProgressWorksheetLine.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMProgressWorksheetLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProgressWorksheetLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMProgressWorksheetLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMProgressWorksheetLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProgressWorksheetLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMProgressWorksheetLine.lastModifiedDateTime>
  {
  }
}
