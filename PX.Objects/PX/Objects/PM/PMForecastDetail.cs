// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMForecastDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a detail line of a revision of a project budget forecast.
/// The records of this type are created and edited through the Project Budget Forecast (PM209600) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.ForecastMaint" /> graph).
/// </summary>
[ExcludeFromCodeCoverage]
[PXCacheName("Budget Forecast Detail")]
[PXPrimaryGraph(typeof (ForecastMaint))]
[Serializable]
public class PMForecastDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IProjectFilter
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
  /// The <see cref="T:PX.Objects.PM.PMProject">project</see> of the project budget forecast.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXDBDefault(typeof (PMProject.contractID))]
  [PXForeignReference(typeof (Field<PMForecastDetail.projectID>.IsRelatedTo<PMProject.contractID>))]
  [PXDBInt(IsKey = true)]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMForecast">project budget forecast revision</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMForecast.RevisionID" /> field.
  /// </value>
  [PXParent(typeof (Select<PMForecast, Where<PMForecast.projectID, Equal<Current<PMForecastDetail.projectID>>, And<PMForecast.revisionID, Equal<Current<PMForecastDetail.revisionID>>>>>))]
  [PXDBString(15, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Revision")]
  public virtual string RevisionID { get; set; }

  /// <summary>Get or set Project TaskID</summary>
  public int? TaskID => this.ProjectTaskID;

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMTask">project task</see> of the budget line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMTask.taskID" /> field.
  /// </value>
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMForecastDetail.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [PXForeignReference(typeof (CompositeKey<Field<PMForecastDetail.projectID>.IsRelatedTo<PMTask.projectID>, Field<PMForecastDetail.projectTaskID>.IsRelatedTo<PMTask.taskID>>))]
  [BaseProjectTask(typeof (PMForecastDetail.projectID), IsKey = true, Enabled = false, AllowCompleted = true, AllowCanceled = true)]
  public virtual int? ProjectTaskID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMAccountGroup">project account group</see> of the budget line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
  /// </value>
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new Type[] {typeof (PMAccountGroup.groupCD)})]
  [PXForeignReference(typeof (Field<PMForecastDetail.accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  [PXDefault]
  [AccountGroup(IsKey = true, Enabled = false)]
  public virtual int? AccountGroupID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> of the budget line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Inventory ID", Enabled = false)]
  [PMInventorySelector]
  [PXParent(typeof (Select<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMForecastDetail.inventoryID>>>>))]
  [PXDefault]
  [PXForeignReference(typeof (Field<PMForecastDetail.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? InventoryID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMCostCode">project cost code</see> of the budget line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMCostCode.CostCodeID" /> field.
  /// </value>
  [PXForeignReference(typeof (Field<PMForecastDetail.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  [CostCode(null, typeof (PMForecastDetail.projectTaskID), null, typeof (PMForecastDetail.accountGroupID), true, false, IsKey = true, Enabled = false, Filterable = false, SkipVerification = true)]
  public virtual int? CostCodeID { get; set; }

  /// <summary>The financial period.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod.FinPeriodID" /> field.
  /// </value>
  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Financial Period", Enabled = false)]
  [PXSelector(typeof (MasterFinPeriod.finPeriodID), DescriptionField = typeof (MasterFinPeriod.descr))]
  public virtual string PeriodID { get; set; }

  /// <summary>The description of the budget line.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description { get; set; }

  /// <summary>The original budgeted quantity.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Quantity")]
  public virtual Decimal? Qty { get; set; }

  /// <summary>The original budgeted amount.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Amount")]
  public virtual Decimal? CuryAmount { get; set; }

  /// <summary>The revised budgeted quantity.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Quantity")]
  public virtual Decimal? RevisedQty { get; set; }

  /// <summary>The revised budgeted amount.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Amount")]
  public virtual Decimal? CuryRevisedAmount { get; set; }

  [PXNote(DescriptionField = typeof (PMForecastDetail.description))]
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

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMForecastDetail.projectID>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMForecastDetail.revisionID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMForecastDetail.projectTaskID>
  {
  }

  public abstract class accountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMForecastDetail.accountGroupID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMForecastDetail.inventoryID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMForecastDetail.costCodeID>
  {
  }

  public abstract class periodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMForecastDetail.periodID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMForecastDetail.description>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMForecastDetail.qty>
  {
  }

  public abstract class curyAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMForecastDetail.curyAmount>
  {
  }

  public abstract class revisedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMForecastDetail.revisedQty>
  {
  }

  public abstract class curyRevisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMForecastDetail.curyRevisedAmount>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMForecastDetail.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMForecastDetail.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMForecastDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMForecastDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMForecastDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMForecastDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMForecastDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMForecastDetail.lastModifiedDateTime>
  {
  }
}
