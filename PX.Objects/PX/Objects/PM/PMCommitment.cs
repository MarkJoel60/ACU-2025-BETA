// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMCommitment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// A commitment.
/// Commitments are created and updated during processing of <see cref="T:PX.Objects.PO.POOrder">purchase orders</see>
/// or subcontracts with detail lines related to a project. Also the records of this type are created through the
/// External Commitments (PM209000) form (which corresponds to the <see cref="T:PX.Objects.PM.ExternalCommitmentEntry" /> graph).
/// The records of this type are displayed on the Commitments (PM306000) form (which corresponds to the
/// <see cref="T:PX.Objects.PM.CommitmentInquiry" /> graph).
/// </summary>
[PXCacheName("Commitment Record")]
[PXPrimaryGraph(typeof (CommitmentInquiry))]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMCommitment : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IProjectFilter,
  IQuantify
{
  protected Guid? _CommitmentID;
  protected 
  #nullable disable
  string _Type;
  protected int? _AccountGroupID;
  protected int? _ProjectID;
  protected int? _ProjectTaskID;
  protected int? _InventoryID;
  protected int? _CostCodeID;
  protected string _ExtRefNbr;
  protected string _UOM;
  protected Decimal? _Qty;
  protected Decimal? _Amount;
  protected Decimal? _ReceivedQty;
  protected Decimal? _InvoicedQty;
  protected Decimal? _InvoicedAmount;
  protected Decimal? _OpenQty;
  protected Decimal? _OpenAmount;
  protected Guid? _RefNoteID;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>The identifier of the commitment.</summary>
  [PXDefault]
  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? CommitmentID
  {
    get => this._CommitmentID;
    set => this._CommitmentID = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.Branch">branch</see> of the commitment.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.GL.Branch.branchID" /> field.
  /// </value>
  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  /// <summary>The type of the commitment.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMCommitmentType.ListAttribute" />.
  /// </value>
  [PXDBString(1)]
  [PXDefault("I")]
  [PMCommitmentType.List]
  [PXUIField(DisplayName = "Type")]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  /// <summary>The status of the commitment.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMCommitmentStatus.ListAttribute" />.
  /// </value>
  [PXDBString(1)]
  [PXDefault("O")]
  [PMCommitmentStatus.List]
  [PXUIField(DisplayName = "Status")]
  public virtual string Status { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMAccountGroup">account group</see> of the commitment.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMAccountGroup.accountID" /> field.
  /// </value>
  [PXDefault]
  [PXForeignReference(typeof (Field<PMCommitment.accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  [AccountGroup]
  public virtual int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the commitment.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMProject.contractID" /> field.
  /// </value>
  [PXDefault]
  [PXForeignReference(typeof (Field<PMCommitment.projectID>.IsRelatedTo<PMProject.contractID>))]
  [PXRestrictor(typeof (Where<PMProject.nonProject, Equal<False>>), "Non-Project is not a valid option.", new System.Type[] {})]
  [PXRestrictor(typeof (Where<Contract.isCancelled, Equal<False>>), "The {0} project or contract is canceled.", new System.Type[] {typeof (PMProject.contractCD)})]
  [ProjectBase]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMTask">project task</see> associated with the commitment.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMTask.taskID" /> field.
  /// </value>
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMCommitment.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [ActiveOrInPlanningProjectTask(typeof (PMCommitment.projectID))]
  [PXForeignReference(typeof (CompositeKey<Field<PMCommitment.projectID>.IsRelatedTo<PMTask.projectID>, Field<PMCommitment.projectTaskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? ProjectTaskID
  {
    get => this._ProjectTaskID;
    set => this._ProjectTaskID = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMTask">project task</see> associated with the commitment.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMCommitment.projectTaskID" /> field.
  /// </value>
  public int? TaskID => this.ProjectTaskID;

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> of the commitment.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.IN.InventoryItem.inventoryID" /> field.
  /// </value>
  [PXUIField(DisplayName = "Inventory ID")]
  [PXDBInt]
  [PMInventorySelector]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMCostCode">cost code</see> of the commitment.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMCostCode.costCodeID" /> field.
  /// </value>
  [CostCode(SkipVerification = true)]
  [PXForeignReference(typeof (Field<PMCommitment.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  /// <summary>
  /// The reference number of the commitment of the external type.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "External Ref. Nbr")]
  public virtual string ExtRefNbr
  {
    get => this._ExtRefNbr;
    set => this._ExtRefNbr = value;
  }

  /// <summary>The unit of measure of the commitment.</summary>
  [PMUnit(typeof (PMCommitment.inventoryID))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  /// <summary>The original quantity of the commitment.</summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBQuantity]
  [PXUIField(DisplayName = "Original Committed Quantity", FieldClass = "CHANGEORDER")]
  public virtual Decimal? OrigQty { get; set; }

  /// <summary>The original amount of the commitment.</summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBBaseCury]
  [PXUIField(DisplayName = "Original Committed Amount", FieldClass = "CHANGEORDER")]
  public virtual Decimal? OrigAmount { get; set; }

  /// <summary>The revised quantity of the commitment.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Committed Quantity")]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  /// <summary>The revised amount of the commitment.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Committed Amount")]
  public virtual Decimal? Amount
  {
    get => this._Amount;
    set => this._Amount = value;
  }

  /// <summary>The received quantity of the commitment.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Received Quantity")]
  public virtual Decimal? ReceivedQty
  {
    get => this._ReceivedQty;
    set => this._ReceivedQty = value;
  }

  /// <summary>The invoiced quantity of the commitment.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Invoiced Quantity")]
  public virtual Decimal? InvoicedQty
  {
    get => this._InvoicedQty;
    set => this._InvoicedQty = value;
  }

  /// <summary>The invoiced amount of the commitment.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Invoiced Amount")]
  public virtual Decimal? InvoicedAmount
  {
    get => this._InvoicedAmount;
    set => this._InvoicedAmount = value;
  }

  /// <summary>
  /// The total quantity of the cost commitment lines of released change orders that are associated with the commitment.
  /// </summary>
  [PXQuantity]
  [PXUIField(DisplayName = "Committed CO Quantity", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CommittedCOQty
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMCommitment.qty), typeof (PMCommitment.origQty)})] get
    {
      Decimal? nullable = this.Qty;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.OrigQty;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>
  /// The total amount of the cost commitment lines of released change orders that are associated with the commitment.
  /// </summary>
  [PXBaseCury]
  [PXUIField(DisplayName = "Committed CO Amount", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CommittedCOAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMCommitment.amount), typeof (PMCommitment.origAmount)})] get
    {
      Decimal? nullable = this.Amount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.OrigAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>
  /// The difference between the values of <see cref="P:PX.Objects.PM.PMCommitment.Qty" /> and <see cref="P:PX.Objects.PM.PMCommitment.InvoicedQty" />.
  /// </summary>
  [PXQuantity]
  [PXUIField(DisplayName = "Committed Variance Quantity", Enabled = false)]
  public virtual Decimal? CommittedVarianceQty
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMCommitment.qty), typeof (PMCommitment.invoicedQty)})] get
    {
      Decimal? nullable = this.Qty;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.InvoicedQty;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>
  /// The difference between the values of <see cref="P:PX.Objects.PM.PMCommitment.Amount" /> and <see cref="P:PX.Objects.PM.PMCommitment.InvoicedAmount" />.
  /// </summary>
  [PXBaseCury]
  [PXUIField(DisplayName = "Committed Variance Amount", Enabled = false)]
  public virtual Decimal? CommittedVarianceAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMCommitment.amount), typeof (PMCommitment.invoicedAmount)})] get
    {
      Decimal? nullable = this.Amount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.InvoicedAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>The project currency.</summary>
  [PXString(5, IsUnicode = true)]
  [PXDBScalar(typeof (Search<PMProject.curyID, Where<PMProject.contractID, Equal<PMCommitment.projectID>>>))]
  [PXUIField(DisplayName = "Project Currency", Enabled = false, FieldClass = "ProjectMultiCurrency")]
  public virtual string ProjectCuryID { get; set; }

  /// <summary>
  /// The open quantity of the commitment that has not been received yet.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Open Quantity")]
  public virtual Decimal? OpenQty
  {
    get => this._OpenQty;
    set => this._OpenQty = value;
  }

  /// <summary>
  /// The open amount of the commitment that has not been received yet.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Open Amount")]
  public virtual Decimal? OpenAmount
  {
    get => this._OpenAmount;
    set => this._OpenAmount = value;
  }

  /// <summary>The document from which the commitment originates.</summary>
  [PXUIField(DisplayName = "Related Document")]
  [PMCommitment.PXRefNote]
  public virtual Guid? RefNoteID
  {
    get => this._RefNoteID;
    set => this._RefNoteID = value;
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

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.CommitmentID" />
  public abstract class commitmentID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMCommitment.commitmentID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.BranchID" />
  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCommitment.branchID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.Type" />
  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCommitment.type>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.Status" />
  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCommitment.status>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.AccountGroupID" />
  public abstract class accountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCommitment.accountGroupID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.ProjectID" />
  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCommitment.projectID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.ProjectTaskID" />
  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCommitment.projectTaskID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.InventoryID" />
  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCommitment.inventoryID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.CostCodeID" />
  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCommitment.costCodeID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.ExtRefNbr" />
  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCommitment.extRefNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.UOM" />
  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCommitment.uOM>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.OrigQty" />
  public abstract class origQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMCommitment.origQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.OrigAmount" />
  public abstract class origAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMCommitment.origAmount>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.Qty" />
  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMCommitment.qty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.Amount" />
  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMCommitment.amount>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.ReceivedQty" />
  public abstract class receivedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMCommitment.receivedQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.InvoicedQty" />
  public abstract class invoicedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMCommitment.invoicedQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.InvoicedAmount" />
  public abstract class invoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCommitment.invoicedAmount>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.CommittedCOQty" />
  public abstract class committedCOQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCommitment.committedCOQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.CommittedCOAmount" />
  public abstract class committedCOAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCommitment.committedCOAmount>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.CommittedVarianceQty" />
  public abstract class committedVarianceQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCommitment.committedVarianceQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.CommittedVarianceAmount" />
  public abstract class committedVarianceAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCommitment.committedVarianceAmount>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.ProjectCuryID" />
  public abstract class projectCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCommitment.projectCuryID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.OpenQty" />
  public abstract class openQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMCommitment.openQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.OpenAmount" />
  public abstract class openAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMCommitment.openAmount>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.RefNoteID" />
  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMCommitment.refNoteID>
  {
  }

  public class PXRefNoteAttribute : PXRefNoteBaseAttribute
  {
    public virtual void FieldSelecting(PXCache cache, PXFieldSelectingEventArgs args)
    {
      using (new PXReadBranchRestrictedScope())
      {
        Guid? nullable = (Guid?) cache.GetValue(args.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
        args.ReturnValue = nullable.HasValue ? (object) this.helper.GetEntityRowID(nullable, (string) null) : (object) string.Empty;
      }
    }

    public virtual void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      // ISSUE: method pointer
      PXButtonDelegate pxButtonDelegate = new PXButtonDelegate((object) this, __methodptr(\u003CCacheAttached\u003Eb__2_0));
      string str = $"{sender.GetItemType().Name}${((PXEventSubscriberAttribute) this)._FieldName}$Link";
      sender.Graph.Actions[str] = (PXAction) Activator.CreateInstance(typeof (PXNamedAction<>).MakeGenericType(typeof (CommitmentInquiry.ProjectBalanceFilter)), (object) sender.Graph, (object) str, (object) pxButtonDelegate, (object) new PXEventSubscriberAttribute[1]
      {
        (PXEventSubscriberAttribute) new PXUIFieldAttribute()
        {
          MapEnableRights = (PXCacheRights) 1
        }
      });
    }
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMCommitment.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMCommitment.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMCommitment.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCommitment.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMCommitment.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMCommitment.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCommitment.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMCommitment.lastModifiedDateTime>
  {
  }
}
