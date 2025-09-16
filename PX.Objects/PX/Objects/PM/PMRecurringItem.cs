// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMRecurringItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a recurring billing line.
/// Recurring billing line defines the rules the system uses to create the corresponding invoice line during the project billing.
/// The records of this type are created and edited through the Project Tasks (PM302000) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.ProjectTaskEntry" /> graph),
/// through the Project Template Tasks (PM208010) form (which corresponds to the <see cref="T:PX.Objects.PM.TemplateTaskMaint" /> graph),
/// and through the Common Tasks (PM208030) form (which corresponds to the <see cref="T:PX.Objects.PM.TemplateGlobalTaskMaint" /> graph).
/// </summary>
[PXCacheName("Recurring Items")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMRecurringItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the recurring billing.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMProject.contractID" /> field.
  /// </value>
  [PXDBDefault(typeof (PMTask.projectID))]
  [PXForeignReference(typeof (Field<PMRecurringItem.projectID>.IsRelatedTo<PMProject.contractID>))]
  [PXDBInt(IsKey = true)]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">task</see> associated with the recurring billing.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMTask.taskID" /> field.
  /// </value>
  [PXDBDefault(typeof (PMTask.taskID))]
  [PXParent(typeof (Select<PMTask, Where<PMTask.projectID, Equal<Current<PMRecurringItem.projectID>>, And<PMTask.taskID, Equal<Current<PMRecurringItem.taskID>>>>>))]
  [PXForeignReference(typeof (CompositeKey<Field<PMRecurringItem.projectID>.IsRelatedTo<PMTask.projectID>, Field<PMRecurringItem.taskID>.IsRelatedTo<PMTask.taskID>>))]
  [PXDBInt(IsKey = true)]
  public virtual int? TaskID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> associated with the recurring billing.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.IN.InventoryItem.inventoryID" /> field.
  /// </value>
  [NonStockItem(IsKey = true)]
  [PXDefault]
  [PXForeignReference(typeof (Field<PMRecurringItem.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? InventoryID { get; set; }

  /// <summary>
  /// The unit of measure of the item associated with the recurring billing.
  /// </summary>
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.salesUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMRecurringItem.inventoryID>>>>))]
  [PMUnit(typeof (PMRecurringItem.inventoryID))]
  public virtual string UOM { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see> associated with the recurring billing.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(null, null, true, true, false)]
  public virtual int? BranchID { get; set; }

  /// <summary>The descrption of the recurring item.</summary>
  [PXLocalizableDefault(typeof (Search<PX.Objects.IN.InventoryItem.descr, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMRecurringItem.inventoryID>>>>), typeof (PX.Objects.AR.Customer.localeName))]
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description { get; set; }

  /// <summary>The amount of the recurring item.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? Amount { get; set; }

  /// <summary>The account source of the recurring item.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMAccountSource.RecurentListAttribute" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PMAccountSource.RecurentList]
  [PXDefault("C")]
  [PXUIField(DisplayName = "Account Source")]
  public virtual string AccountSource { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Account">branch</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.GL.Account.accountID" /> field.
  /// </value>
  [Account(DisplayName = "Account", DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  public virtual int? AccountID { get; set; }

  /// <summary>The submask of the recurring item.</summary>
  [PMRecurentBillSubAccountMask]
  public virtual string SubMask { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Sub">subaccount</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.GL.Sub.subID" /> field.
  /// </value>
  [SubAccount(typeof (PMRecurringItem.accountID), DisplayName = "Subaccount", DescriptionField = typeof (Sub.description))]
  public virtual int? SubID { get; set; }

  /// <summary>
  /// The field that defines the frequency of recurring billing usage.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.CT.ResetUsageOption.ListForProjectAttribute" />.
  /// </value>
  [PXDefault("N")]
  [PXUIField(DisplayName = "Reset Usage")]
  [PXDBString(1, IsFixed = true)]
  [ResetUsageOption.ListForProject]
  public virtual string ResetUsage { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Included")]
  public virtual Decimal? Included { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Used", Enabled = false, Visible = false)]
  public virtual Decimal? Used { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Used Total", Enabled = false)]
  public virtual Decimal? UsedTotal { get; set; }

  /// <summary>The latest date when the recurring item was billed.</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Last Billed Date", Enabled = false)]
  public virtual DateTime? LastBilledDate { get; set; }

  /// <summary>
  /// The quantity that was used during the latest billing of the recurring item.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Last Billed Qty.", Enabled = false)]
  public virtual Decimal? LastBilledQty { get; set; }

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

  /// <inheritdoc cref="P:PX.Objects.PM.PMRecurringItem.ProjectID" />
  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRecurringItem.projectID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRecurringItem.TaskID" />
  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRecurringItem.taskID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRecurringItem.InventoryID" />
  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRecurringItem.inventoryID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRecurringItem.UOM" />
  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRecurringItem.uOM>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRecurringItem.BranchID" />
  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRecurringItem.branchID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRecurringItem.Description" />
  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRecurringItem.description>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRecurringItem.Amount" />
  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMRecurringItem.amount>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRecurringItem.AccountSource" />
  public abstract class accountSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMRecurringItem.accountSource>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRecurringItem.AccountID" />
  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRecurringItem.accountID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRecurringItem.SubMask" />
  public abstract class subMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRecurringItem.subMask>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRecurringItem.SubID" />
  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRecurringItem.subID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRecurringItem.ResetUsage" />
  public abstract class resetUsage : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRecurringItem.resetUsage>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRecurringItem.Included" />
  public abstract class included : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMRecurringItem.included>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRecurringItem.Used" />
  public abstract class used : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMRecurringItem.used>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRecurringItem.UsedTotal" />
  public abstract class usedTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMRecurringItem.usedTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRecurringItem.LastBilledDate" />
  public abstract class lastBilledDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMRecurringItem.lastBilledDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMRecurringItem.LastBilledQty" />
  public abstract class lastBilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRecurringItem.lastBilledQty>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMRecurringItem.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMRecurringItem.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMRecurringItem.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMRecurringItem.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMRecurringItem.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMRecurringItem.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMRecurringItem.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMRecurringItem.lastModifiedDateTime>
  {
  }
}
