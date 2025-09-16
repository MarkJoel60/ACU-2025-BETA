// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMChangeOrderBudget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>The base class for <see cref="T:PX.Objects.PM.PMChangeOrderRevenueBudget" /> and <see cref="T:PX.Objects.PM.PMChangeOrderCostBudget" /> types.
/// The DAC provides fields common to these types.</summary>
[ExcludeFromCodeCoverage]
[PXCacheName("Budget")]
[Serializable]
public class PMChangeOrderBudget : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IProjectFilter,
  IQuantify
{
  protected int? _ProjectID;
  protected int? _CostCodeID;
  protected int? _AccountGroupID;
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _Type;
  protected Decimal? _Rate;
  protected string _Description;
  protected Decimal? _Qty;
  protected string _UOM;
  protected bool? _Released;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The reference number of the parent <see cref="T:PX.Objects.PM.PMChangeOrder">change order</see>.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXParent(typeof (Select<PMChangeOrder, Where<PMChangeOrder.refNbr, Equal<Current<PMChangeOrderBudget.refNbr>>>>))]
  [PXDBDefault(typeof (PMChangeOrder.refNbr))]
  [PXUIField(DisplayName = "Ref. Nbr.", Enabled = false)]
  public virtual string RefNbr { get; set; }

  /// <summary>The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the change order line.</summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.PM.PMChangeOrder.ProjectID">project</see> of the parent change order.
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXDefault(typeof (PMChangeOrder.projectID))]
  [PXForeignReference(typeof (Field<PMChangeOrderBudget.projectID>.IsRelatedTo<PMProject.contractID>))]
  [PXDBInt]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>The original sequence number of the line.</summary>
  /// <remarks>The sequence of line numbers that belongs to a single document can include gaps.</remarks>
  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (PMChangeOrder.budgetLineCntr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public virtual int? LineNbr { get; set; }

  /// <summary>The identifier of the <see cref="T:PX.Objects.PM.PMTask">task</see> associated with the change order line.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMTask.TaskID" /> field.
  /// </value>
  public int? TaskID => this.ProjectTaskID;

  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMChangeOrderBudget.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [PXDBInt]
  [PXForeignReference(typeof (CompositeKey<Field<PMChangeOrderBudget.projectID>.IsRelatedTo<PMTask.projectID>, Field<PMChangeOrderBudget.projectTaskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? ProjectTaskID { get; set; }

  /// <summary>The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">cost code</see> associated with the change order line.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMCostCode.costCodeID" /> field.
  /// </value>
  [CostCode(ReleasedField = typeof (PMChangeOrderBudget.released))]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.PM.PMAccountGroup">account group</see> associated with the change order line.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
  /// </value>
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new System.Type[] {typeof (PMAccountGroup.groupCD)})]
  [PXDefault]
  [AccountGroup]
  [PXForeignReference(typeof (Field<PMChangeOrderBudget.accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  public virtual int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> associated with the change order line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField]
  [PMInventorySelector]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  /// <summary>The type of the change order line.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"A"</c>: Asset,
  /// <c>"L"</c>: Liability,
  /// <c>"I"</c>: Income,
  /// <c>"E"</c>: Expense,
  /// <c>"O"</c>: Off-Balance
  /// </value>
  [PXDBString(1)]
  [PXDefault]
  [PMAccountType.List]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  /// <summary>The rate of the specified unit of the change order line. The value can be manually modified.</summary>
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Rate")]
  public virtual Decimal? Rate
  {
    get => this._Rate;
    set => this._Rate = value;
  }

  /// <summary>The description of the change order line.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>The quantity of the change order line.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  /// <summary>The unit of measure of the change order line.</summary>
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.baseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMBudget.inventoryID>>>>))]
  [PMUnit(typeof (PMChangeOrderBudget.inventoryID))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  /// <summary>The amount of the change order line in the base currency. The value can be manually modified.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? Amount { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.PM.PMBudget.Qty">Original Budgeted Quantity</see>,
  /// <see cref="P:PX.Objects.PM.PMChangeOrderBudget.PreviouslyApprovedQty">Previously Approved CO Quantity</see>,
  /// and <see cref="P:PX.Objects.PM.PMChangeOrderBudget.Qty">Quantity</see> values.
  /// </summary>
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Quantity", Enabled = false)]
  public virtual Decimal? RevisedQty { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.PM.PMBudget.CuryAmount">Original Budgeted Amount</see>,
  /// <see cref="P:PX.Objects.PM.PMChangeOrderBudget.PreviouslyApprovedAmount">Previously Approved CO Amount</see>,
  /// and <see cref="P:PX.Objects.PM.PMChangeOrderBudget.Amount">Amount</see> values. The amount is displayed in the base currency.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Amount", Enabled = false)]
  public virtual Decimal? RevisedAmount { get; set; }

  /// <summary>
  /// The total quantity of all the estimation lines of linked change requests
  /// with the same project task, account group, and cost code or inventory item.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Change Request Total Quantity", Enabled = false, FieldClass = "ChangeRequest")]
  public virtual Decimal? ChangeRequestQty { get; set; }

  /// <summary>
  /// The total amount of all the estimation lines of linked change requests
  /// with the same project task, account group, and cost code or inventory item.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Change Request Total Amount", Enabled = false, FieldClass = "ChangeRequest")]
  public virtual Decimal? ChangeRequestAmount { get; set; }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the following change order line fields are not available for editing: <see cref="P:PX.Objects.PM.PMChangeOrderBudget.ProjectTaskID" />, <see cref="P:PX.Objects.PM.PMChangeOrderBudget.AccountGroupID" />,
  /// <see cref="P:PX.Objects.PM.PMChangeOrderBudget.CostCodeID" />, <see cref="P:PX.Objects.PM.PMChangeOrderBudget.InventoryID" />, <see cref="P:PX.Objects.PM.PMChangeOrderBudget.UOM" />.</summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsDisabled { get; set; }

  /// <summary>
  /// The total quantity of the released change orders that were created before the current one
  /// and that are associated with the same project, project task, account group, and cost code or inventory item.
  /// </summary>
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PreviouslyApprovedQty { get; set; }

  /// <summary>
  /// The total amount of the released change orders that were created before the current one
  /// and that are associated with the same project, project task, account group, and cost code or inventory item.
  /// The amount is displayed in the base currency.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PreviouslyApprovedAmount { get; set; }

  /// <summary>
  /// The total quantity of the commitment lines of the currently selected change order
  /// that are associated with the same project, project task, account group, and cost code or inventory item.
  /// </summary>
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CommittedCOQty { get; set; }

  /// <summary>
  /// The total amount of the commitment lines of the currently selected change order
  /// that are associated with the same project, project task, account group, and cost code or inventory item.
  /// The amount is displayed in the base currency.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CommittedCOAmount { get; set; }

  /// <summary>The total amount of lines of the unreleased change orders (except for the current one) that refer to the cost budget line with the same project, project task,
  /// account group, and cost code or inventory item. The amount is displayed in the base currency.</summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OtherDraftRevisedAmount { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.PM.PMChangeOrderBudget.RevisedAmount">Revised Budgeted Amount</see> and <see cref="P:PX.Objects.PM.PMChangeOrderBudget.OtherDraftRevisedAmount">Other Draft CO Amount</see> values.
  /// The amount is displayed in the base currency.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalPotentialRevisedAmount { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the parent <see cref="T:PX.Objects.PM.PMChangeOrder">change order</see> has been released.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Released", Enabled = false)]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXNote]
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

  /// <summary>Primary Key</summary>
  /// <exclude />
  public class PK : 
    PrimaryKeyOf<PMChangeOrderBudget>.By<PMChangeOrderBudget.refNbr, PMChangeOrderBudget.lineNbr>
  {
    public static PMChangeOrderBudget Find(
      PXGraph graph,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (PMChangeOrderBudget) PrimaryKeyOf<PMChangeOrderBudget>.By<PMChangeOrderBudget.refNbr, PMChangeOrderBudget.lineNbr>.FindBy(graph, (object) refNbr, (object) lineNbr, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  /// <exclude />
  public static class FK
  {
    /// <summary>Change Order</summary>
    /// <exclude />
    public class ChangeOrder : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<PMChangeOrderBudget>.By<PMChangeOrderBudget.refNbr>
    {
    }

    /// <summary>Project</summary>
    /// <exclude />
    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<PMChangeOrderBudget>.By<PMChangeOrderBudget.projectID>
    {
    }

    /// <summary>Project Task</summary>
    /// <exclude />
    public class ProjectTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<PMChangeOrderBudget>.By<PMChangeOrderBudget.projectID, PMChangeOrderBudget.projectTaskID>
    {
    }

    /// <summary>Account Group</summary>
    /// <exclude />
    public class AccountGroup : 
      PrimaryKeyOf<PMAccountGroup>.By<PMAccountGroup.groupID>.ForeignKeyOf<PMChangeOrderBudget>.By<PMChangeOrderBudget.accountGroupID>
    {
    }

    /// <summary>Cost Code</summary>
    /// <exclude />
    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<PMChangeOrderBudget>.By<PMChangeOrderBudget.costCodeID>
    {
    }

    /// <summary>Inventory Item</summary>
    /// <exclude />
    public class Item : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<PMChangeOrderBudget>.By<PMChangeOrderBudget.inventoryID>
    {
    }
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeOrderBudget.refNbr>
  {
    public const int Length = 15;
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrderBudget.projectID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrderBudget.lineNbr>
  {
  }

  public abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMChangeOrderBudget.projectTaskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrderBudget.costCodeID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMChangeOrderBudget.accountGroupID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrderBudget.inventoryID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeOrderBudget.type>
  {
  }

  public abstract class rate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMChangeOrderBudget.rate>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrderBudget.description>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMChangeOrderBudget.qty>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeOrderBudget.uOM>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMChangeOrderBudget.amount>
  {
  }

  public abstract class revisedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderBudget.revisedQty>
  {
  }

  public abstract class revisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderBudget.revisedAmount>
  {
  }

  public abstract class changeRequestQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderBudget.changeRequestQty>
  {
  }

  public abstract class changeRequestAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderBudget.changeRequestAmount>
  {
  }

  public abstract class isDisabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMChangeOrderBudget.isDisabled>
  {
  }

  public abstract class previouslyApprovedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderBudget.previouslyApprovedQty>
  {
  }

  public abstract class previouslyApprovedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderBudget.previouslyApprovedAmount>
  {
  }

  public abstract class committedCOQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderBudget.committedCOQty>
  {
  }

  public abstract class committedCOAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderBudget.committedCOAmount>
  {
  }

  public abstract class otherDraftRevisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderBudget.otherDraftRevisedAmount>
  {
  }

  public abstract class totalPotentialRevisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderBudget.totalPotentialRevisedAmount>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMChangeOrderBudget.released>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMChangeOrderBudget.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMChangeOrderBudget.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMChangeOrderBudget.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrderBudget.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMChangeOrderBudget.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMChangeOrderBudget.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrderBudget.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMChangeOrderBudget.lastModifiedDateTime>
  {
  }
}
