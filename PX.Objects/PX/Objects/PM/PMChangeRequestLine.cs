// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMChangeRequestLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents an estimation line of a <see cref="T:PX.Objects.PM.PMChangeRequest">change request</see>.
/// The records of this type are created and edited through the <strong>Estimation</strong> tab of the Change Requests (PM308500) form
/// (which corresponds to the <see cref="!:ChangeRequestEntry" /> graph).
/// </summary>
[ExcludeFromCodeCoverage]
[PXCacheName("Change Request")]
[Serializable]
public class PMChangeRequestLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IQuantify
{
  protected int? _SubItemID;
  protected int? _VendorID;
  protected Decimal? _Qty;
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
  /// The reference number of the parent <see cref="T:PX.Objects.PM.PMChangeRequest">change request</see>.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (PMChangeRequest.refNbr))]
  [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
  [PXParent(typeof (Select<PMChangeRequest, Where<PMChangeRequest.refNbr, Equal<Current<PMChangeRequestLine.refNbr>>>>))]
  public virtual string RefNbr { get; set; }

  /// <summary>The original sequence number of the line.</summary>
  /// <remarks>
  /// The sequence of line numbers that belongs to a single document can include gaps.
  /// </remarks>
  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (PMChangeRequest.lineCntr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public virtual int? LineNbr { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the estimation line.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.PM.PMChangeRequest.ProjectID">project</see> of the parent change request.
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXDBDefault(typeof (PMChangeRequest.projectID))]
  [PXDBInt]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">task</see> associated with the cost part of the estimation line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMTask.TaskID" /> field.
  /// </value>
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMChangeRequestLine.projectID>>, And<PMTask.isDefault, Equal<True>, And<PMTask.type, NotEqual<ProjectTaskType.revenue>>>>>))]
  [ProjectTask(typeof (PMChangeRequestLine.projectID), typeof (Where<PMTask.type, NotEqual<ProjectTaskType.revenue>>), AlwaysEnabled = true)]
  [PXForeignReference(typeof (CompositeKey<Field<PMChangeRequestLine.projectID>.IsRelatedTo<PMTask.projectID>, Field<PMChangeRequestLine.costTaskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? CostTaskID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMAccountGroup">expense account group</see> associated with the cost part of the estimation line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
  /// </value>
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new Type[] {typeof (PMAccountGroup.groupCD)})]
  [AccountGroup(typeof (Where<PMAccountGroup.isExpense, Equal<True>>))]
  [PXForeignReference(typeof (Field<PMChangeRequestLine.costAccountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  public virtual int? CostAccountGroupID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">cost code</see> associated with the cost part of the estimation line.
  /// </summary>
  [CostCode(null, typeof (PMChangeRequestLine.costTaskID), "E", typeof (PMChangeRequestLine.costAccountGroupID))]
  public virtual int? CostCodeID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> associated with the cost part of the estimation line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Inventory ID")]
  [PMInventorySelector]
  public virtual int? InventoryID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.INSubItem">subitem</see> associated with the estimation line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.INSubItem.SubItemID" /> field.
  /// </value>
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.defaultSubItemID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMChangeRequestLine.inventoryID>>, And<PX.Objects.IN.InventoryItem.defaultSubItemOnEntry, Equal<True>>>>))]
  [SubItem(typeof (PMChangeRequestLine.inventoryID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">task</see> associated with the revenue part of the estimation line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMTask.TaskID" /> field.
  /// </value>
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMChangeRequestLine.projectID>>, And<PMTask.isDefault, Equal<True>, And<PMTask.type, NotEqual<ProjectTaskType.cost>>>>>))]
  [ProjectTask(typeof (PMChangeRequestLine.projectID), typeof (Where<PMTask.type, NotEqual<ProjectTaskType.cost>>), AlwaysEnabled = true, AllowNull = true, DisplayName = "Revenue Task")]
  [PXForeignReference(typeof (CompositeKey<Field<PMChangeRequestLine.projectID>.IsRelatedTo<PMTask.projectID>, Field<PMChangeRequestLine.revenueTaskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? RevenueTaskID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMAccountGroup">income account group</see> associated with the revenue part of the estimation line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
  /// </value>
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new Type[] {typeof (PMAccountGroup.groupCD)})]
  [AccountGroup(typeof (Where<PMAccountGroup.type, Equal<AccountType.income>>), DisplayName = "Revenue Account Group")]
  [PXForeignReference(typeof (Field<PMChangeRequestLine.revenueAccountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  public virtual int? RevenueAccountGroupID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">cost code</see> associated with the revenue part of the estimation line.
  /// </summary>
  [CostCode(null, typeof (PMChangeRequestLine.revenueTaskID), "I", typeof (PMChangeRequestLine.revenueAccountGroupID), DisplayName = "Revenue Code", AllowNullValue = true)]
  public virtual int? RevenueCodeID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> associated with the revenue part of the estimation line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Revenue Inventory ID")]
  [PMInventorySelector]
  public virtual int? RevenueInventoryID { get; set; }

  /// <summary>The description of the estimation line.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  [PXFieldDescription]
  public virtual string Description { get; set; }

  /// <summary>
  /// The identifier of the vendor associated with the estimation line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [Vendor(typeof (Search<PX.Objects.AP.Vendor.bAccountID, Where<PX.Objects.AP.Vendor.vStatus, NotEqual<VendorStatus.inactive>>>))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the system produces a commitment line
  /// based on this estimation line when the change request is linked to a change order.
  /// </summary>
  [PXDBBool]
  [PXDefault(false, typeof (Search<PMAccountGroup.createsCommitment, Where<PMAccountGroup.groupID, Equal<Current<PMChangeRequestLine.costAccountGroupID>>>>))]
  [PXUIField(DisplayName = "Create Commitment")]
  public bool? IsCommitment { get; set; }

  /// <summary>
  /// The unit of measure associated with the cost part of the estimation line.
  /// </summary>
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.salesUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMChangeRequestLine.inventoryID>>>>))]
  [PMUnit(typeof (PMChangeRequestLine.inventoryID))]
  public virtual string UOM { get; set; }

  /// <summary>The quantity of the estimation line.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  /// <summary>The cost of the estimation line.</summary>
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Cost")]
  public virtual Decimal? UnitCost { get; set; }

  /// <summary>
  /// The extended cost of the estimation line, which the system calculates by multiplying
  /// the <see cref="P:PX.Objects.PM.PMChangeRequestLine.Qty">Quantity</see> and <see cref="P:PX.Objects.PM.PMChangeRequestLine.UnitCost">Unit Cost</see> values of the line.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ext. Cost")]
  [PXFormula(typeof (Mult<PMChangeRequestLine.qty, PMChangeRequestLine.unitCost>), typeof (SumCalc<PMChangeRequest.costTotal>))]
  public virtual Decimal? ExtCost { get; set; }

  /// <summary>
  /// The markup percentage that the system uses to calculate <see cref="P:PX.Objects.PM.PMChangeRequestLine.UnitPrice">Unit Price</see>
  /// based on <see cref="P:PX.Objects.PM.PMChangeRequestLine.ExtCost">Ext. Cost</see>.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<PX.Objects.IN.InventoryItem.markupPct, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMChangeRequestLine.inventoryID>>>>))]
  [PXUIField(DisplayName = "Price Markup (%)")]
  public virtual Decimal? PriceMarkupPct { get; set; }

  /// <summary>
  /// The price of the estimation line, which the system calculates by multiplying
  /// the <see cref="P:PX.Objects.PM.PMChangeRequestLine.UnitCost">Unit Cost</see> and <see cref="P:PX.Objects.PM.PMChangeRequestLine.PriceMarkupPct">Price Markup (%)</see> values for the line.
  /// </summary>
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Price")]
  [PXFormula(typeof (Mult<Div<Add<decimal100, PMChangeRequestLine.priceMarkupPct>, decimal100>, PMChangeRequestLine.unitCost>))]
  public virtual Decimal? UnitPrice { get; set; }

  /// <summary>
  /// The extended price of the estimation line, which the system calculates by multiplying
  /// the <see cref="P:PX.Objects.PM.PMChangeRequestLine.Qty">Quantity</see> and <see cref="P:PX.Objects.PM.PMChangeRequestLine.UnitPrice">Unit Price</see> values of the line.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ext. Price")]
  [PXFormula(typeof (Mult<PMChangeRequestLine.qty, PMChangeRequestLine.unitPrice>))]
  public virtual Decimal? ExtPrice { get; set; }

  /// <summary>The line markup percentage of the estimation line.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<PMAccountGroup.defaultLineMarkupPct, Where<PMAccountGroup.groupID, Equal<Current<PMChangeRequestLine.costAccountGroupID>>>>))]
  [PXUIField(DisplayName = "Line Markup (%)")]
  public virtual Decimal? LineMarkupPct { get; set; }

  /// <summary>
  /// The total amount of the revenue part of the estimation line.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Line Amount")]
  [PXFormula(typeof (Div<Mult<PMChangeRequestLine.extPrice, Add<decimal100, PMChangeRequestLine.lineMarkupPct>>, decimal100>), typeof (SumCalc<PMChangeRequest.lineTotal>))]
  public virtual Decimal? LineAmount { get; set; }

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

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeRequestLine.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeRequestLine.lineNbr>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeRequestLine.projectID>
  {
  }

  public abstract class costTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeRequestLine.costTaskID>
  {
  }

  public abstract class costAccountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMChangeRequestLine.costAccountGroupID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeRequestLine.costCodeID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeRequestLine.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeRequestLine.subItemID>
  {
  }

  public abstract class revenueTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMChangeRequestLine.revenueTaskID>
  {
  }

  public abstract class revenueAccountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMChangeRequestLine.revenueAccountGroupID>
  {
  }

  public abstract class revenueCodeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMChangeRequestLine.revenueCodeID>
  {
  }

  public abstract class revenueInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMChangeRequestLine.revenueInventoryID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeRequestLine.description>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeRequestLine.vendorID>
  {
  }

  public abstract class isCommitment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMChangeRequestLine.isCommitment>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeRequestLine.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMChangeRequestLine.qty>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMChangeRequestLine.unitCost>
  {
  }

  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMChangeRequestLine.extCost>
  {
  }

  public abstract class priceMarkupPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeRequestLine.priceMarkupPct>
  {
  }

  public abstract class unitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeRequestLine.unitPrice>
  {
  }

  public abstract class extPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMChangeRequestLine.extPrice>
  {
  }

  public abstract class lineMarkupPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeRequestLine.priceMarkupPct>
  {
  }

  public abstract class lineAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeRequestLine.lineAmount>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMChangeRequestLine.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMChangeRequestLine.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMChangeRequestLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeRequestLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMChangeRequestLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMChangeRequestLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeRequestLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMChangeRequestLine.lastModifiedDateTime>
  {
  }
}
