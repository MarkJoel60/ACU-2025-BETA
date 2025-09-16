// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProgressWorksheet
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
using PX.Data.WorkflowAPI;
using PX.Objects.CR;
using PX.Objects.CT;
using PX.TM;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Contains the main properties of a progress worksheet. The records of this type are created and edited through the Progress Worksheets (PM303000) form (which corresponds to the
/// <see cref="T:PX.Objects.PM.ProgressWorksheetEntry" /> graph).
/// </summary>
[ExcludeFromCodeCoverage]
[PXCacheName("Progress Worksheet")]
[PXPrimaryGraph(typeof (ProgressWorksheetEntry))]
[PXEMailSource]
[Serializable]
public class PMProgressWorksheet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssign
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

  /// <summary>The reference number of the progress worksheet.</summary>
  /// <value>
  /// The number is generated from the <see cref="T:PX.Objects.CS.Numbering">numbering sequence</see>,
  /// which is specified on the <see cref="T:PX.Objects.PM.PMSetup">Projects Preferences</see> (PM101000) form.
  /// </value>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PMProgressWorksheet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<PMProgressWorksheet.projectID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProgressWorksheet.hidden, NotEqual<True>>>>>.And<MatchUserFor<PMProject>>>, PMProgressWorksheet>.SearchFor<PMProgressWorksheet.refNbr>), DescriptionField = typeof (PMProgressWorksheet.description))]
  [ProgressWorksheetAutoNumber]
  public virtual string RefNbr { get; set; }

  /// <summary>The status of the progress worksheet.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"H"</c>: On Hold,
  /// <c>"A"</c>: Pending Approval,
  /// <c>"O"</c>: Open,
  /// <c>"C"</c>: Closed,
  /// <c>"R"</c>: Rejected
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [ProgressWorksheetStatus.List]
  [PXDefault("H")]
  [PXUIField]
  public virtual string Status { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document is on hold.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Hold")]
  [PXDefault(true)]
  public virtual bool? Hold { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document is approved.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Approved { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document is rejected.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public bool? Rejected { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document has been released.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Released")]
  [PXDefault(false)]
  public virtual bool? Released { get; set; }

  /// <summary>The date on which the progress worksheet was created.</summary>
  /// <value>
  /// By default, the value is set to the current <see cref="P:PX.Data.AccessInfo.BusinessDate">business date</see>.
  /// </value>
  [PXDBDate]
  [PMFinPeriodDateValidation]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? Date { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the progress worksheet.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXRestrictor(typeof (Where<PMProject.status, Equal<ProjectStatus.active>, And<PMProject.nonProject, Equal<False>>>), "The {0} project is not active.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXDefault]
  [PXForeignReference(typeof (Field<PMProgressWorksheet.projectID>.IsRelatedTo<PMProject.contractID>))]
  [Project(typeof (Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.status, Equal<ProjectStatus.active>, And<PMProject.nonProject, Equal<False>>>>))]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// A counter of the document lines, which is used internally to assign <see cref="P:PX.Objects.PM.PMProgressWorksheetLine.LineNbr">numbers</see> to new lines. We do not recommend that you
  /// rely on this field to determine the exact number of lines because it might not reflect this number under various conditions.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document is hidden on the Progress Worksheets (PM303000) form.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Hidden { get; set; }

  /// <summary>The workgroup that is responsible for the document.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.TM.EPCompanyTree.WorkGroupID">EPCompanyTree.WorkGroupID</see> field.
  /// </value>
  [PXDBInt]
  [PXDefault(typeof (BAccount.workgroupID))]
  [PXCompanyTreeSelector]
  [PXUIField]
  public virtual int? WorkgroupID { get; set; }

  /// <summary>The <see cref="T:PX.Objects.CR.Contact">contact</see> responsible for the document.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [PXDBInt]
  [PXDefault(typeof (BAccount.ownerID))]
  [PXUIField]
  public virtual int? OwnerID { get; set; }

  /// <summary>The reference number of the progress worksheet.</summary>
  /// <value>
  /// If the progress worksheet is unhidden, the value of this field contains the reference number. If the progress worksheet is hidden, the value is empty.
  /// </value>
  [PXString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Worksheet Nbr.")]
  [PXFormula(typeof (Switch<Case<Where<PMProgressWorksheet.hidden, Equal<True>>, Empty>, PMProgressWorksheet.refNbr>))]
  [PXFieldDescription]
  public virtual string HiddenRefNbr { get; set; }

  /// <summary>The description of the progress worksheet.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Description { get; set; }

  /// <summary>The status of the progress worksheet.</summary>
  /// <value>
  /// If the progress worksheet is unhidden, the value of this field contains the status of the progress worksheet. If the progress worksheet is hidden, the value is empty.
  /// </value>
  [PXString(1, IsFixed = true)]
  [ProgressWorksheetStatus.List]
  [PXUIField(DisplayName = "Status")]
  [PXFormula(typeof (Switch<Case<Where<PMProgressWorksheet.hidden, Equal<True>>, Empty>, PMProgressWorksheet.status>))]
  public virtual string HiddenStatus { get; set; }

  /// <summary>
  /// The reference number of the linked daily field report.
  /// </summary>
  [PXString(10)]
  [PXUIField(DisplayName = "Daily Field Report", FieldClass = "ConstructionProjectManagement")]
  public virtual string DailyFieldReportCD { get; set; }

  [ProgressWorksheetSearchable]
  [PXNote(ShowInReferenceSelector = true, DescriptionField = typeof (PMProgressWorksheet.description))]
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

  public class Events : PXEntityEventBase<PMProgressWorksheet>.Container<PMProgressWorksheet.Events>
  {
    public PXEntityEvent<PMProgressWorksheet> Release;
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProgressWorksheet.refNbr>
  {
    public const int Length = 15;
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProgressWorksheet.status>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProgressWorksheet.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProgressWorksheet.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProgressWorksheet.rejected>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProgressWorksheet.released>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMProgressWorksheet.date>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProgressWorksheet.projectID>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProgressWorksheet.lineCntr>
  {
  }

  public abstract class hidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProgressWorksheet.hidden>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProgressWorksheet.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProgressWorksheet.ownerID>
  {
  }

  public abstract class hiddenRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProgressWorksheet.hiddenRefNbr>
  {
    public const int Length = 15;
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProgressWorksheet.description>
  {
  }

  public abstract class hiddenStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProgressWorksheet.hiddenStatus>
  {
  }

  public abstract class dailyFieldReportCD : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProgressWorksheet.dailyFieldReportCD>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMProgressWorksheet.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMProgressWorksheet.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMProgressWorksheet.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProgressWorksheet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMProgressWorksheet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMProgressWorksheet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProgressWorksheet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMProgressWorksheet.lastModifiedDateTime>
  {
  }
}
