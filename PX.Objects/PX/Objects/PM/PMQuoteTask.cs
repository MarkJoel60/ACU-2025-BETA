// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMQuoteTask
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.TX;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a project quote task.
/// The records of this type are created and edited through the <strong>Project Tasks</strong> tab of the Project Quotes (PM304500) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.PMQuoteMaint" /> graph).
/// </summary>
[PXCacheName("Project Task")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMQuoteTask : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TaskCD;
  protected string _Description;
  protected DateTime? _PlannedStartDate;
  protected DateTime? _PlannedEndDate;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The reference number of the parent <see cref="T:PX.Objects.PM.PMQuote">project quote</see>.
  /// </summary>
  [PXDBGuid(false, IsKey = true)]
  [PXDBDefault(typeof (PMQuote.quoteID))]
  [PXParent(typeof (Select<PMQuote, Where<PMQuote.quoteID, Equal<Current<PMQuoteTask.quoteID>>>>))]
  public virtual Guid? QuoteID { get; set; }

  /// <summary>The identifier of the project task.</summary>
  [PXDimension("PROTASK")]
  [PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  public virtual string TaskCD
  {
    get => this._TaskCD;
    set => this._TaskCD = value;
  }

  /// <summary>The description of the project task.</summary>
  [PXDBLocalizableString(250, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>The date when the task is expected to be started.</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Planned Start Date")]
  public virtual DateTime? PlannedStartDate
  {
    get => this._PlannedStartDate;
    set => this._PlannedStartDate = value;
  }

  /// <summary>The date when the task is expected to be ended.</summary>
  [PXDBDate]
  [PXVerifyEndDate(typeof (PMQuoteTask.plannedStartDate), AutoChangeWarning = true)]
  [PXUIField(DisplayName = "Planned End Date")]
  public virtual DateTime? PlannedEndDate
  {
    get => this._PlannedEndDate;
    set => this._PlannedEndDate = value;
  }

  /// <summary>The tax category of the task.</summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Category")]
  [PXSelector(typeof (TaxCategory.taxCategoryID), DescriptionField = typeof (TaxCategory.descr))]
  [PXRestrictor(typeof (Where<TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (TaxCategory.taxCategoryID)})]
  public virtual string TaxCategoryID { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the task is a default task.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Default")]
  public virtual bool? IsDefault { get; set; }

  /// <summary>The type of the project task.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"Cost"</c>: Cost Task,
  /// <c>"Rev"</c>: Revenue Task,
  /// <c>"CostRev"</c>: Cost and Revenue Task
  /// </value>
  [PXDBString(10)]
  [PXDefault("CostRev")]
  [PXUIField(DisplayName = "Type", Required = true, FieldClass = "Construction")]
  [ProjectTaskType.List]
  public string Type { get; set; }

  [PXNote(DescriptionField = typeof (PMQuoteTask.taskCD))]
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

  public abstract class quoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMQuoteTask.quoteID>
  {
  }

  public abstract class taskCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuoteTask.taskCD>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuoteTask.description>
  {
  }

  public abstract class plannedStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMQuoteTask.plannedStartDate>
  {
  }

  public abstract class plannedEndDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMQuoteTask.plannedEndDate>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuoteTask.taxCategoryID>
  {
  }

  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMQuoteTask.isDefault>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuoteTask.type>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMQuoteTask.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMQuoteTask.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMQuoteTask.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMQuoteTask.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMQuoteTask.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMQuoteTask.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMQuoteTask.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMQuoteTask.lastModifiedDateTime>
  {
  }
}
