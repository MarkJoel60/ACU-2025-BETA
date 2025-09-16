// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMChangeOrderLine
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
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PO;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>A commitment line of a <see cref="T:PX.Objects.PM.PMChangeOrder">change order</see>. The records of this type are created and edited through the <strong>Commitments</strong> tab of the
/// Change Orders (PM308000) form (which corresponds to the <see cref="T:PX.Objects.PM.ChangeOrderEntry" /> graph).</summary>
[ExcludeFromCodeCoverage]
[PXCacheName("Change Order Line")]
[PXPrimaryGraph(typeof (ChangeOrderEntry))]
[Serializable]
public class PMChangeOrderLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IQuantify
{
  protected int? _LineNbr;
  protected int? _ProjectID;
  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">Cost Code</see> associated with the commitment.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMCostCode.costCodeID" /> field.
  /// </value>
  protected int? _CostCodeID;
  protected int? _InventoryID;
  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.INSubItem">Subitem</see> associated with the commitment.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.INSubItem.SubItemID" /> field.
  /// </value>
  protected int? _SubItemID;
  /// <summary>
  /// The identifier of the <see cref="!:Vendor" /> associated with the commitment.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  protected int? _VendorID;
  /// <summary>
  /// The number of the <see cref="T:PX.Objects.PO.POLine">purchase order line</see> associated with the commitment.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PO.POLine.LineNbr" /> field.
  /// </value>
  protected int? _POLineNbr;
  protected 
  #nullable disable
  string _CuryID;
  protected string _UOM;
  protected int? _AccountID;
  protected Decimal? _Qty;
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
  [PXDBDefault(typeof (PMChangeOrder.refNbr))]
  [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
  [PXParent(typeof (Select<PMChangeOrder, Where<PMChangeOrder.refNbr, Equal<Current<PMChangeOrderLine.refNbr>>>>))]
  public virtual string RefNbr { get; set; }

  /// <summary>The original sequence number of the line.</summary>
  /// <remarks>The sequence of line numbers that belongs to a single document can include gaps.</remarks>
  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (PMChangeOrder.lineCntr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the commitment.</summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.PM.PMChangeOrder.ProjectID">project</see> of the parent change order.
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXDBDefault(typeof (PMChangeOrder.projectID))]
  [PXDBInt]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">Task</see> associated with the commitment.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMTask.TaskID" /> field.
  /// </value>
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMChangeOrderLine.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [ProjectTask(typeof (PMChangeOrderLine.projectID), AlwaysEnabled = true)]
  [PXForeignReference(typeof (CompositeKey<Field<PMChangeOrderLine.projectID>.IsRelatedTo<PMTask.projectID>, Field<PMChangeOrderLine.taskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? TaskID { get; set; }

  [CostCode(typeof (PMChangeOrderLine.accountID), typeof (PMChangeOrderLine.taskID), "E", ReleasedField = typeof (PMChangeOrderLine.released), ProjectField = typeof (PMChangeOrderLine.projectID), InventoryField = typeof (PMChangeOrderLine.inventoryID), VendorField = typeof (PMChangeOrderLine.vendorID), UseNewDefaulting = true)]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> associated with the commitment.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [Inventory(Filterable = true)]
  [PXParent(typeof (Select<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMChangeOrderLine.inventoryID>>>>))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.defaultSubItemID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMChangeOrderLine.inventoryID>>, And<PX.Objects.IN.InventoryItem.defaultSubItemOnEntry, Equal<True>>>>))]
  [SubItem(typeof (PMChangeOrderLine.inventoryID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  /// <summary>The description of the commitment.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Description { get; set; }

  [PXDefault]
  [POVendor]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  /// <summary>
  /// The type of the <see cref="T:PX.Objects.PO.POOrder">purchase order</see> associated with the commitment.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PO.POOrderType.RBDSListAttribute" />.
  /// </value>
  [PXDefault("RO")]
  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "PO Type", Enabled = true)]
  [PX.Objects.PO.POOrderType.RPList]
  public virtual string POOrderType { get; set; }

  /// <summary>
  /// The reference number of the <see cref="T:PX.Objects.PO.POOrder">purchase order</see> associated with the commitment.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PO.POLine.OrderNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "PO Nbr.")]
  [PXRestrictor(typeof (Where<PX.Objects.PO.POOrder.hold, Equal<False>>), "The purchase order is on hold. You need to first clear the On Hold check box for the purchase order.", new Type[] {})]
  [PXRestrictor(typeof (Where<PX.Objects.PO.POOrder.approved, Equal<True>>), "The purchase order is pending approval. Please, approve the order first.", new Type[] {})]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PX.Objects.PO.POLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POOrder>.On<PX.Objects.PO.POLine.FK.Order>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POLine.orderType, Equal<BqlField<PMChangeOrderLine.pOOrderType, IBqlString>.FromCurrent>>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<PMChangeOrderLine.vendorID>, IsNull>>>>.Or<BqlOperand<PX.Objects.PO.POLine.vendorID, IBqlInt>.IsEqual<BqlField<PMChangeOrderLine.vendorID, IBqlInt>.FromCurrent>>>>>.Aggregate<To<GroupBy<PX.Objects.PO.POLine.orderType>, GroupBy<PX.Objects.PO.POLine.orderNbr>, GroupBy<PX.Objects.PO.POLine.vendorID>>>, PX.Objects.PO.POLine>.SearchFor<PX.Objects.PO.POLine.orderNbr>), new Type[] {typeof (PX.Objects.PO.POLine.orderType), typeof (PX.Objects.PO.POLine.orderNbr), typeof (PX.Objects.PO.POLine.vendorID)}, DescriptionField = typeof (PX.Objects.PO.POOrder.vendorID))]
  public virtual string POOrderNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "PO Line Nbr.")]
  [PXSelector(typeof (Search<PX.Objects.PO.POLine.lineNbr, Where<PX.Objects.PO.POLine.orderType, Equal<Current<PMChangeOrderLine.pOOrderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Current<PMChangeOrderLine.pOOrderNbr>>, And<PX.Objects.PO.POLine.projectID, Equal<Current<PMChangeOrder.projectID>>>>>>), new Type[] {typeof (PX.Objects.PO.POLine.lineNbr), typeof (PX.Objects.PO.POLine.lineType), typeof (PX.Objects.PO.POLine.inventoryID), typeof (PX.Objects.PO.POLine.tranDesc), typeof (PX.Objects.PO.POLine.uOM), typeof (PX.Objects.PO.POLine.orderQty), typeof (PX.Objects.PO.POLine.curyExtCost)})]
  public virtual int? POLineNbr
  {
    get => this._POLineNbr;
    set => this._POLineNbr = value;
  }

  /// <summary>
  /// The identifier of the commitment <see cref="T:PX.Objects.CM.Extensions.Currency">currency</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CM.Currency.CuryID" /> field.
  /// </value>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  /// <summary>The unit of measure of the commitment.</summary>
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.purchaseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMChangeOrderLine.inventoryID>>>>))]
  [PMUnit(typeof (PMChangeOrderLine.inventoryID))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.Account">expense account</see> associated with the commitment.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(DisplayName = "Account", DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  /// <summary>The quantity of the commitment.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  /// <summary>The cost of the specified unit of the commitment. The value can be manually modified.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Cost")]
  public virtual Decimal? UnitCost { get; set; }

  /// <summary>The amount of the commitment. The value can be manually modified.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  [PXFormula(typeof (Mult<PMChangeOrderLine.qty, PMChangeOrderLine.unitCost>))]
  public virtual Decimal? Amount { get; set; }

  /// <summary>The <see cref="P:PX.Objects.PM.PMChangeOrderLine.Amount">amount</see> of the commitment in the project currency.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount in Project Currency", Enabled = false)]
  [PXFormula(null, typeof (SumCalc<PMChangeOrder.commitmentTotal>))]
  public virtual Decimal? AmountInProjectCury { get; set; }

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

  /// <summary>
  /// The status of the commitment line of the change order.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"U"</c>: Update,
  /// <c>"L"</c>: New Document,
  /// <c>"D"</c>: New Line,
  /// <c>"R"</c>: Reopen
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [ChangeOrderLineType.List]
  [PXDefault("L")]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  public virtual string LineType { get; set; }

  /// <summary>
  /// The reference number of the corresponding change request if the commitment line has been created based on this change request.
  /// </summary>
  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Change Request Ref. Nbr.", Enabled = false, FieldClass = "ChangeRequest")]
  public virtual string ChangeRequestRefNbr { get; set; }

  /// <summary>The number of the corresponding change request line.</summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Change Request Line Nbr.", Enabled = false, FieldClass = "ChangeRequest")]
  public virtual int? ChangeRequestLineNbr { get; set; }

  /// <summary>The sum of the <see cref="P:PX.Objects.PM.PMChangeOrderLine.Qty">quantity</see> and <see cref="P:PX.Objects.PM.POLinePM.OrderQty">original quantity of the purchase order line</see> associated with the commitment.</summary>
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PotentialRevisedQty { get; set; }

  /// <summary>The sum of the <see cref="T:PX.Objects.PM.PMChangeOrderLine.amount" /> and the <see cref="P:PX.Objects.PM.POLinePM.CuryLineAmt">original amount of the purchase order line</see> associated with the commitment.</summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PotentialRevisedAmount { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Category")]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  public virtual string TaxCategoryID { get; set; }

  /// <summary>The line retainage percentage</summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXUIField(DisplayName = "Retainage Percent", FieldClass = "Retainage")]
  public virtual Decimal? RetainagePct { get; set; }

  /// <summary>The line retainage amount in base currency</summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBBaseCury]
  [PXUIField(DisplayName = "Retainage Amount", FieldClass = "Retainage")]
  public virtual Decimal? RetainageAmt { get; set; }

  /// <summary>The line retainage amount in project currency</summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBBaseCury]
  [PXUIField(DisplayName = "Retainage Amount in Project Currency", Enabled = false, FieldClass = "Retainage")]
  public virtual Decimal? RetainageAmtInProjectCury { get; set; }

  [PXNote(DescriptionField = typeof (PMChangeOrderLine.description))]
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
    PrimaryKeyOf<PMChangeOrderLine>.By<PMChangeOrderLine.refNbr, PMChangeOrderLine.lineNbr>
  {
    public static PMChangeOrderLine Find(
      PXGraph graph,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (PMChangeOrderLine) PrimaryKeyOf<PMChangeOrderLine>.By<PMChangeOrderLine.refNbr, PMChangeOrderLine.lineNbr>.FindBy(graph, (object) refNbr, (object) lineNbr, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  /// <exclude />
  public static class FK
  {
    /// <summary>Change Order</summary>
    public class ChangeOrder : 
      PrimaryKeyOf<PMChangeOrder>.By<PMChangeOrder.refNbr>.ForeignKeyOf<PMChangeOrderLine>.By<PMChangeOrderLine.refNbr>
    {
    }

    /// <summary>Project</summary>
    /// <exclude />
    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<PMChangeOrderLine>.By<PMChangeOrderLine.projectID>
    {
    }

    /// <summary>Project Task</summary>
    /// <exclude />
    public class ProjectTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<PMChangeOrderLine>.By<PMChangeOrderLine.projectID, PMChangeOrderLine.taskID>
    {
    }

    /// <summary>Cost Code</summary>
    /// <exclude />
    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<PMChangeOrderLine>.By<PMChangeOrderLine.costCodeID>
    {
    }

    /// <summary>Inventory Item</summary>
    /// <exclude />
    public class Item : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<PMChangeOrderLine>.By<PMChangeOrderLine.inventoryID>
    {
    }
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeOrderLine.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrderLine.lineNbr>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrderLine.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrderLine.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrderLine.costCodeID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrderLine.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrderLine.subItemID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrderLine.description>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrderLine.vendorID>
  {
  }

  public abstract class pOOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrderLine.pOOrderType>
  {
  }

  public abstract class pOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeOrderLine.pOOrderNbr>
  {
  }

  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrderLine.pOLineNbr>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeOrderLine.curyID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeOrderLine.uOM>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeOrderLine.accountID>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMChangeOrderLine.qty>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMChangeOrderLine.unitCost>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMChangeOrderLine.amount>
  {
  }

  public abstract class amountInProjectCury : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderLine.amountInProjectCury>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMChangeOrderLine.released>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeOrderLine.lineType>
  {
  }

  public abstract class changeRequestRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrderLine.changeRequestRefNbr>
  {
  }

  public abstract class changeRequestLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMChangeOrderLine.changeRequestLineNbr>
  {
  }

  public abstract class potentialRevisedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderLine.potentialRevisedQty>
  {
  }

  public abstract class potentialRevisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderLine.potentialRevisedAmount>
  {
  }

  public abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrderLine.taxCategoryID>
  {
  }

  public abstract class retainagePct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderLine.retainagePct>
  {
    public const int Precision = 6;
  }

  public abstract class retainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderLine.retainageAmt>
  {
  }

  public abstract class retainageAmtInProjectCury : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeOrderLine.retainageAmtInProjectCury>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMChangeOrderLine.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMChangeOrderLine.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMChangeOrderLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrderLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMChangeOrderLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMChangeOrderLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeOrderLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMChangeOrderLine.lastModifiedDateTime>
  {
  }
}
