// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPApproval
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXPrimaryGraph(typeof (EPApprovalMaint))]
[PXCacheName("Approval")]
[Serializable]
public class EPApproval : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ApprovalID;
  protected Guid? _RefNoteID;
  protected int? _AssignmentMapID;
  protected int? _NotificationID;
  protected int? _WorkgroupID;
  protected int? _OwnerID;
  protected int? _DocumentOwnerID;
  protected int? _ApprovedByID;
  protected DateTime? _ApproveDate;
  protected Guid? _NoteID;
  protected 
  #nullable disable
  string _Status;
  protected DateTime? _DocDate;
  protected int? _BAccountID;
  protected int? _WaitTime;
  protected int? _PendingWaitTime;
  protected string _Descr;
  protected string _Reason;
  protected string _Details;
  protected long? _CuryInfoID;
  protected Decimal? _CuryTotalAmount;
  protected Decimal? _TotalAmount;
  private string _DocType;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true)]
  [PXUIField]
  public virtual int? ApprovalID
  {
    get => this._ApprovalID;
    set => this._ApprovalID = value;
  }

  /// <summary>
  /// Contains the type of the related entity, that is specified in <see cref="P:PX.Objects.EP.EPApproval.RefNoteID" />.
  /// </summary>
  [PXString]
  [PXUIField(DisplayName = "Related Entity Type", Enabled = false, Visible = false)]
  [PXEntityTypeList]
  [PXDBScalar(typeof (Search<Note.entityType, Where<Note.noteID, Equal<EPApproval.refNoteID>>>))]
  public virtual string RefNoteIDType { get; set; }

  [EntityIDSelector(typeof (EPApproval.refNoteIDType), LastKeyOnly = true)]
  [PXDBGuid(false)]
  [PXUIField(DisplayName = "References Nbr.")]
  public virtual Guid? RefNoteID
  {
    get => this._RefNoteID;
    set => this._RefNoteID = value;
  }

  [PXDBInt]
  [PXUIField]
  [PXSelector(typeof (Search<EPAssignmentMap.assignmentMapID, Where<EPAssignmentMap.assignmentMapID, Equal<Current<EPApproval.assignmentMapID>>>>))]
  public virtual int? AssignmentMapID
  {
    get => this._AssignmentMapID;
    set => this._AssignmentMapID = value;
  }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Map Step", Enabled = false)]
  [PXSelector(typeof (Search<EPRule.ruleID, Where<EPRule.ruleID, Equal<Current<EPApproval.stepID>>>>), SubstituteKey = typeof (EPRule.name), ValidateValue = false)]
  public virtual Guid? StepID { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Map Rule", Enabled = false)]
  [PXSelector(typeof (Search<EPRule.ruleID, Where<EPRule.ruleID, Equal<Current<EPApproval.ruleID>>>>), SubstituteKey = typeof (EPRule.name), ValidateValue = false)]
  public virtual Guid? RuleID { get; set; }

  [PXDBInt]
  public virtual int? NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [PXDBInt]
  [PXCompanyTreeSelector(ValidateValue = false)]
  [PXUIField]
  public virtual int? WorkgroupID
  {
    get => this._WorkgroupID;
    set => this._WorkgroupID = value;
  }

  /// <summary>
  /// When an approval request is created, the system save the assignee (approval request OwnerID) in a OrigOwnerID field.
  /// </summary>
  [Owner]
  public virtual int? OrigOwnerID { get; set; }

  /// <summary>Approval request current assignee.</summary>
  [Owner]
  public virtual int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  /// <summary>
  /// This field is filled in than approval reassigned according to delegation defined in <see cref="T:PX.Objects.EP.EPWingman" />.
  /// </summary>
  /// <value>
  /// This field corresponds to the <see cref="P:PX.Objects.EP.EPWingman.RecordID" />.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Delegation")]
  public virtual int? DelegationRecordID { get; set; }

  /// <summary>
  /// If set to <see langword="true" />, the system does not reassign the approval requests when the request current owner has a delegation active.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Ignore Delegations")]
  public virtual bool? IgnoreDelegations { get; set; }

  [Owner]
  public virtual int? DocumentOwnerID
  {
    get => this._DocumentOwnerID;
    set => this._DocumentOwnerID = value;
  }

  [Owner]
  public virtual int? ApprovedByID
  {
    get => this._ApprovedByID;
    set => this._ApprovedByID = value;
  }

  [PXDBDate(PreserveTime = true)]
  [PXUIField(DisplayName = "Approval Date", Enabled = false)]
  public virtual DateTime? ApproveDate
  {
    get => this._ApproveDate;
    set => this._ApproveDate = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("P")]
  [PXUIField]
  [EPApprovalStatus.List]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Document Date")]
  public virtual DateTime? DocDate
  {
    get => this._DocDate;
    set => this._DocDate = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Business Account")]
  [PXSelector(typeof (BAccount.bAccountID), SubstituteKey = typeof (BAccount.acctCD), DescriptionField = typeof (BAccount.acctName))]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  [PXDBInt]
  [PXUIField]
  public virtual int? WaitTime
  {
    get => this._WaitTime;
    set => this._WaitTime = value;
  }

  [PXInt]
  [PXUIField]
  [PXDBCalced(typeof (Switch<Case<Where<EPApproval.status, Equal<EPApprovalStatus.pending>>, DateDiff<EPApproval.createdDateTime, Now, DateDiff.minute>>, Zero>), typeof (int))]
  public virtual int? PendingWaitTime
  {
    get => this._PendingWaitTime;
    set => this._PendingWaitTime = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsPreApproved { get; set; }

  [PXDBString(500, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Reason", Enabled = false)]
  public virtual string Reason
  {
    get => this._Reason;
    set => this._Reason = value;
  }

  [PXDBString(500, IsUnicode = true)]
  [PXUIField(DisplayName = "Details")]
  public virtual string Details
  {
    get => this._Details;
    set => this._Details = value;
  }

  [PXDBLong]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Amount")]
  public virtual Decimal? CuryTotalAmount
  {
    get => this._CuryTotalAmount;
    set => this._CuryTotalAmount = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalAmount
  {
    get => this._TotalAmount;
    set => this._TotalAmount = value;
  }

  /// <summary>
  /// The detailed (record-level) type of the source entity to
  /// be approved, e.g. "Bill", "Debit Adjustment", "Cash Return".
  /// </summary>
  [PXDBString]
  [PXDefault]
  public virtual string SourceItemType { get; set; }

  /// <summary>
  /// The friendly name of the source entity to be approved's DAC,
  /// as defined by <see cref="T:PX.Data.PXCacheNameAttribute" /> residing on
  /// that DAC. For example, "AP Invoice", "SO Order".
  /// </summary>
  [PXEntityName(typeof (EPApproval.refNoteID))]
  [PXUIField(DisplayName = "Document")]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
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

  [PXDBCreatedDateTime(UseTimeZone = true)]
  [PXUIField(DisplayName = "Assignment Date")]
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

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : PrimaryKeyOf<EPApproval>.By<EPApproval.approvalID>
  {
    public static EPApproval Find(PXGraph graph, int? approvalID, PKFindOptions options = 0)
    {
      return (EPApproval) PrimaryKeyOf<EPApproval>.By<EPApproval.approvalID>.FindBy(graph, (object) approvalID, options);
    }
  }

  public static class FK
  {
    public class ApprovalMap : 
      PrimaryKeyOf<EPAssignmentMap>.By<EPAssignmentMap.assignmentMapID>.ForeignKeyOf<EPApproval>.By<EPApproval.assignmentMapID>
    {
    }

    public class Rule : 
      PrimaryKeyOf<EPRule>.By<EPRule.ruleID>.ForeignKeyOf<EPApproval>.By<EPApproval.ruleID>
    {
    }

    public class Step : 
      PrimaryKeyOf<EPRule>.By<EPRule.ruleID>.ForeignKeyOf<EPApproval>.By<EPApproval.stepID>
    {
    }

    public class DelegationRecord : 
      PrimaryKeyOf<EPWingman>.By<EPWingman.recordID>.ForeignKeyOf<EPApproval>.By<EPApproval.delegationRecordID>
    {
    }
  }

  public abstract class approvalID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPApproval.approvalID>
  {
  }

  public abstract class refNoteIDType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPApproval.refNoteIDType>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPApproval.refNoteID>
  {
  }

  public abstract class assignmentMapID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPApproval.assignmentMapID>
  {
  }

  public abstract class stepID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPApproval.stepID>
  {
  }

  public abstract class ruleID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPApproval.ruleID>
  {
  }

  public abstract class notificationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPApproval.notificationID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPApproval.workgroupID>
  {
  }

  public abstract class origOwnerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPApproval.origOwnerID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPApproval.ownerID>
  {
  }

  public abstract class delegationRecordID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPApproval.delegationRecordID>
  {
  }

  public abstract class ignoreDelegations : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPApproval.ignoreDelegations>
  {
  }

  public abstract class documentOwnerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPApproval.documentOwnerID>
  {
  }

  public abstract class approvedByID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPApproval.approvedByID>
  {
  }

  public abstract class approveDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  EPApproval.approveDate>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPApproval.noteID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPApproval.status>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  EPApproval.docDate>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPApproval.bAccountID>
  {
  }

  public abstract class waitTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPApproval.waitTime>
  {
  }

  public abstract class pendingWaitTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPApproval.pendingWaitTime>
  {
  }

  public abstract class isPreApproved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPApproval.isPreApproved>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPApproval.descr>
  {
  }

  public abstract class reason : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPApproval.reason>
  {
  }

  public abstract class details : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPApproval.details>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  EPApproval.curyInfoID>
  {
  }

  public abstract class curyTotalAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPApproval.curyTotalAmount>
  {
  }

  public abstract class totalAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPApproval.totalAmount>
  {
  }

  public abstract class sourceItemType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPApproval.sourceItemType>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPApproval.docType>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPApproval.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPApproval.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPApproval.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPApproval.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPApproval.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPApproval.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPApproval.lastModifiedDateTime>
  {
  }
}
