// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMChangeRequestMarkup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Contains a markup of a <see cref="T:PX.Objects.PM.PMChangeRequest">change request</see>.
/// The records of this type are created and edited through the <strong>Markups</strong> tab
/// of the Change Requests (PM308500) form (which corresponds to the <see cref="!:ChangeRequestEntry" /> graph).
/// </summary>
[ExcludeFromCodeCoverage]
[PXCacheName("Markup")]
[Serializable]
public class PMChangeRequestMarkup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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
  /// The reference number of the parent <see cref="T:PX.Objects.PM.PMChangeRequest">change request</see>.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (PMChangeRequest.refNbr))]
  [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
  [PXParent(typeof (Select<PMChangeRequest, Where<PMChangeRequest.refNbr, Equal<Current<PMChangeRequestMarkup.refNbr>>>>))]
  public virtual string RefNbr { get; set; }

  /// <summary>The original sequence number of the line.</summary>
  /// <remarks>
  /// The sequence of line numbers that belongs to a single document can include gaps.
  /// </remarks>
  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (PMChangeRequest.markupLineCntr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public virtual int? LineNbr { get; set; }

  /// <summary>The type of the document markup.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"P"</c>: %,
  /// <c>"F"</c>: Flat Fee,
  /// <c>"C"</c>: Cumulative %
  /// </value>
  [PXUIField(DisplayName = "Type")]
  [PXDefault("P")]
  [PMMarkupLineType.List]
  [PXDBString(1)]
  public virtual string Type { get; set; }

  /// <summary>The description of the markup.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  [PXFieldDescription]
  public virtual string Description { get; set; }

  /// <summary>
  /// The percentage or amount of the markup, depending on the markup type.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Value")]
  public virtual Decimal? Value { get; set; }

  /// <summary>The amount to which the markup is applied.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Subject to Markup", Enabled = false)]
  public virtual Decimal? Amount { get; set; }

  /// <summary>The markup amount.</summary>
  [PXFormula(null, typeof (SumCalc<PMChangeRequest.markupTotal>))]
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Markup Amount", Enabled = false)]
  public virtual Decimal? MarkupAmount { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">task</see> associated with the markup.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMTask.TaskID" /> field.
  /// </value>
  [ProjectTask(typeof (PMChangeRequest.projectID), AlwaysEnabled = true, AllowNull = true)]
  [PXForeignReference(typeof (Field<PMChangeRequestMarkup.taskID>.IsRelatedTo<PMTask.taskID>))]
  public virtual int? TaskID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMAccountGroup">expense account group</see> associated with the markup.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
  /// </value>
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new System.Type[] {typeof (PMAccountGroup.groupCD)})]
  [AccountGroup(typeof (Where<PMAccountGroup.type, Equal<AccountType.income>>))]
  [PXForeignReference(typeof (Field<PMChangeRequestMarkup.accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  public virtual int? AccountGroupID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">cost code</see> associated with the markup.
  /// </summary>
  [CostCode(null, typeof (PMChangeRequestMarkup.taskID), "I", typeof (PMChangeRequestMarkup.accountGroupID), AllowNullValue = true)]
  public virtual int? CostCodeID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> associated with the markup.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Inventory ID")]
  [PMInventorySelector]
  public virtual int? InventoryID { get; set; }

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
  PMChangeRequestMarkup.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeRequestMarkup.lineNbr>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMChangeRequestMarkup.type>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeRequestMarkup.description>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMChangeRequestMarkup.value>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMChangeRequestMarkup.amount>
  {
  }

  public abstract class markupAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMChangeRequestMarkup.markupAmount>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeRequestMarkup.taskID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMChangeRequestMarkup.accountGroupID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeRequestMarkup.costCodeID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMChangeRequestMarkup.inventoryID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMChangeRequestMarkup.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMChangeRequestMarkup.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMChangeRequestMarkup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeRequestMarkup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMChangeRequestMarkup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMChangeRequestMarkup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMChangeRequestMarkup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMChangeRequestMarkup.lastModifiedDateTime>
  {
  }
}
