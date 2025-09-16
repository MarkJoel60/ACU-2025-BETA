// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRPMSMEmail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.EP;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <summary>
/// The projection of the <see cref="T:PX.Objects.CR.CRActivity" /> class which is a flattened version of the
/// <see cref="T:PX.Objects.CR.CRActivity" />, <see cref="T:PX.Objects.CR.SMEmail" />, and <see cref="T:PX.Objects.CR.PMTimeActivity" /> classes.
/// It is used only by <see cref="T:PX.Objects.EP.DefaultEmailProcessor" />.
/// This class is preserved for internal use only.
/// </summary>
[PXProjection(typeof (Select2<CRActivity, LeftJoin<SMEmail, On<SMEmail.refNoteID, Equal<CRActivity.noteID>>, LeftJoin<PMTimeActivity, On<PMTimeActivity.refNoteID, Equal<CRActivity.noteID>>>>>), Persistent = true)]
[Serializable]
public class CRPMSMEmail : CRActivity
{
  /// <inheritdoc cref="P:PX.Objects.CR.CRActivity.NoteID" />
  [PXSequentialNote(SuppressActivitiesCount = true, IsKey = true, BqlField = typeof (CRActivity.noteID))]
  public override Guid? NoteID { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.CR.PMTimeActivity.NoteID">PMTimeActivity.noteID</see> field.
  /// </summary>
  [PXDBSequentialGuid(BqlField = typeof (PMTimeActivity.noteID))]
  [PXExtraKey]
  public virtual Guid? TimeActivityNoteID { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.CR.SMEmail.NoteID">PMTimeActivity.noteID</see> field.
  /// </summary>
  [PXDBSequentialGuid(BqlField = typeof (SMEmail.noteID))]
  [PXExtraKey]
  public virtual Guid? EmailNoteID { get; set; }

  /// <summary>
  /// Email <see cref="P:PX.Objects.CR.SMEmail.NoteID" /> in response to which a new incoming (or outgoing) email was created
  /// </summary>
  [PXUIField(DisplayName = "In Response To")]
  [PXDBGuid(false, BqlField = typeof (SMEmail.responseToNoteID))]
  public virtual Guid? ResponseToNoteID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.CRActivity.RefNoteID" />
  [PXDBGuid(false, BqlField = typeof (CRActivity.refNoteID))]
  public override Guid? RefNoteID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.PMTimeActivity.IsBillable" />
  [PXDBBool(BqlField = typeof (PMTimeActivity.isBillable))]
  public virtual bool? IsBillable { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.PMTimeActivity.CostCodeID" />
  [PXDBInt(BqlField = typeof (PMTimeActivity.costCodeID))]
  public virtual int? CostCodeID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.PMTimeActivity.TimeCardCD" />
  [PXDBString(10, BqlField = typeof (PMTimeActivity.timeCardCD))]
  public virtual 
  #nullable disable
  string TimeCardCD { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.PMTimeActivity.ProjectID" />
  [EPProject(typeof (CRActivity.ownerID), FieldClass = "PROJECT", BqlField = typeof (PMTimeActivity.projectID))]
  public virtual int? ProjectID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.PMTimeActivity.ProjectTaskID" />
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<CRPMSMEmail.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [EPTimecardProjectTask(typeof (CRPMSMEmail.projectID), "TA", DisplayName = "Project Task", BqlField = typeof (PMTimeActivity.projectTaskID))]
  public virtual int? ProjectTaskID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.SMEmail.MPStatus" />
  [PXDBString(2, IsFixed = true, IsUnicode = false, BqlField = typeof (SMEmail.mpstatus))]
  public virtual string MPStatus { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.SMEmail.IsArchived" />
  [PXDBBool(BqlField = typeof (SMEmail.isArchived))]
  [PXDefault(false)]
  public virtual bool? IsArchived { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.SMEmail.ID" />
  [PXDBIdentity(BqlField = typeof (SMEmail.id))]
  public virtual int? ID { get; set; }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRPMSMEmail.noteID>
  {
  }

  public abstract class timeActivityNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRPMSMEmail.timeActivityNoteID>
  {
  }

  public abstract class emailNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRPMSMEmail.emailNoteID>
  {
  }

  public abstract class responseToNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRPMSMEmail.responseToNoteID>
  {
  }

  public new abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRPMSMEmail.refNoteID>
  {
  }

  public abstract class isBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRPMSMEmail.isBillable>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMSMEmail.costCodeID>
  {
  }

  public abstract class timeCardCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRPMSMEmail.timeCardCD>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMSMEmail.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMSMEmail.projectTaskID>
  {
  }

  public abstract class mpstatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRPMSMEmail.mpstatus>
  {
  }

  public abstract class isArchived : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRPMSMEmail.isArchived>
  {
  }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMSMEmail.id>
  {
  }
}
