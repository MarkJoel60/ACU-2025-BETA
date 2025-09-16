// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PMCRActivity
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.EP;
using PX.SM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXBreakInheritance]
[PXProjection(typeof (Select2<PMTimeActivity, LeftJoin<CRActivity, On<CRActivity.noteID, Equal<PMTimeActivity.refNoteID>>>, Where<PMTimeActivity.isCorrected, Equal<False>, Or<PMTimeActivity.isCorrected, IsNull>>>), Persistent = true)]
[Serializable]
public class PMCRActivity : CRPMTimeActivity
{
  private int? _ClassID;

  [PXDBSequentialGuid(IsKey = true, BqlField = typeof (PMTimeActivity.noteID))]
  public override Guid? TimeActivityNoteID { get; set; }

  [PXSequentialSelfRefNote(SuppressActivitiesCount = true, NoteField = typeof (PMCRActivity.timeActivityNoteID), Persistent = true, BqlField = typeof (PMTimeActivity.refNoteID))]
  [PXParent(typeof (Select<CRActivity, Where<CRActivity.noteID, Equal<Current<CRPMTimeActivity.refNoteID>>>>), ParentCreate = true)]
  public override Guid? TimeActivityRefNoteID { get; set; }

  [PXDBString(256 /*0x0100*/, InputMask = "", IsUnicode = true, BqlField = typeof (PMTimeActivity.summary))]
  [PXDefault]
  [PXUIField]
  [PXNavigateSelector(typeof (PMCRActivity.summary))]
  public new virtual 
  #nullable disable
  string Summary { get; set; }

  [PXDBDateAndTime(DisplayNameDate = "Date", DisplayNameTime = "Time", UseTimeZone = true, BqlField = typeof (PMTimeActivity.date))]
  [PXUIField(DisplayName = "Date")]
  [PXFormula(typeof (IsNull<Current<CRActivity.startDate>, Current<CRSMEmail.startDate>>))]
  public override DateTime? Date { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PMTimeActivity.approvalStatus))]
  [ActivityStatusList]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  [PXFormula(typeof (Switch<Case<Where<CRPMTimeActivity.trackTime, Equal<True>, And<Current2<PMCRActivity.approvalStatus>, IsNull>>, ActivityStatusListAttribute.open, Case<Where<CRPMTimeActivity.released, Equal<True>>, ActivityStatusListAttribute.released, Case<Where<CRPMTimeActivity.approverID, IsNotNull>, ActivityStatusListAttribute.pendingApproval>>>, ActivityStatusListAttribute.completed>))]
  public override string ApprovalStatus { get; set; }

  [PXDBSequentialGuid(BqlField = typeof (CRActivity.noteID))]
  [PXTimeTag(typeof (PMCRActivity.noteID))]
  [PXExtraKey]
  public override Guid? NoteID { get; set; }

  [PXDBInt(BqlField = typeof (CRActivity.classID))]
  [PMActivityClass]
  [PXDefault(typeof (PMActivityClass.timeActivity))]
  [PXUIField]
  [PXFieldDescription]
  public override int? ClassID
  {
    get => new int?(this._ClassID ?? 8);
    set => this._ClassID = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  [PXFormula(typeof (Switch<Case<Where<Current2<PMCRActivity.noteID>, IsNull>, PMActivityClass.UI.timeActivity, Case<Where<PMCRActivity.classID, Equal<CRActivityClass.activity>, And<CRPMTimeActivity.type, IsNotNull>>, Selector<CRPMTimeActivity.type, EPActivityType.description>, Case<Where<PMCRActivity.classID, Equal<CRActivityClass.email>, And<CRPMTimeActivity.incoming, Equal<True>>>, PMCRActivity.classInfo.emailResponse>>>, String<PMCRActivity.classID>>))]
  [Obsolete("This field is not used anymore")]
  public override string ClassInfo { get; set; }

  [PXChildUpdatable(AutoRefresh = true)]
  [Owner(typeof (CRPMTimeActivity.workgroupID), BqlField = typeof (CRActivity.ownerID))]
  [PXDefault(typeof (Coalesce<Search<EPCompanyTreeMember.contactID, Where<EPCompanyTreeMember.workGroupID, Equal<Current<CRPMTimeActivity.workgroupID>>, And<EPCompanyTreeMember.contactID, Equal<Current<AccessInfo.contactID>>>>>, Search<Contact.contactID, Where<Contact.contactID, Equal<Current<AccessInfo.contactID>>, And<Current<CRPMTimeActivity.workgroupID>, IsNull>>>>))]
  public override int? OwnerID
  {
    get => base.OwnerID ?? this.TimeActivityOwner;
    set
    {
      if (!this.NoteID.HasValue)
        return;
      base.OwnerID = this.TimeActivityOwner = value;
    }
  }

  [PXDBString(256 /*0x0100*/, InputMask = "", IsUnicode = true, BqlField = typeof (CRActivity.subject))]
  [PXDefault]
  [PXFormula(typeof (PMCRActivity.summary))]
  [PXUIField]
  [PXNavigateSelector(typeof (PMCRActivity.subject))]
  public override string Subject { get; set; }

  [PXGuid]
  public override Guid? ChildKey
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMCRActivity.noteID)})] get => this.NoteID;
  }

  /// <summary>
  /// The status of the activity. The field has the value of <see cref="P:PX.Objects.CR.CRPMTimeActivity.UIStatus" /> or <see cref="P:PX.Objects.CR.PMCRActivity.ApprovalStatus" /> depending on the <see cref="T:PX.Objects.CR.PMCRActivity.classID" /> of the activity.
  /// </summary>
  [ActivityStatusList]
  [PXFormula(typeof (Switch<IBqlString, TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Empty, Case<Where<BqlOperand<PMCRActivity.classID, IBqlInt>.IsEqual<CRActivityClass.task>>, CRPMTimeActivity.uistatus>>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRPMTimeActivity.trackTime, Equal<True>>>>>.And<BqlOperand<PMCRActivity.approvalStatus, IBqlString>.IsNull>>, ActivityStatusListAttribute.open>>, Case<Where<BqlOperand<CRPMTimeActivity.released, IBqlBool>.IsEqual<True>>, ActivityStatusListAttribute.released>, ActivityStatusListAttribute.pendingApproval>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRPMTimeActivity.approverID, IsNotNull>>>>.And<BqlOperand<PMCRActivity.approvalStatus, IBqlString>.IsNull>>.Else<PMCRActivity.approvalStatus>))]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  [PXString]
  public string DisplayStatus { get; set; }

  public new abstract class timeActivityNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMCRActivity.timeActivityNoteID>
  {
  }

  public new abstract class timeActivityRefNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMCRActivity.timeActivityRefNoteID>
  {
  }

  public new abstract class summary : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCRActivity.summary>
  {
  }

  public new abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMCRActivity.date>
  {
  }

  public new abstract class approvalStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCRActivity.approvalStatus>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMCRActivity.noteID>
  {
  }

  public new abstract class classID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCRActivity.classID>
  {
  }

  public new abstract class classIcon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCRActivity.classIcon>
  {
  }

  [Obsolete("This field is not used anymore")]
  public new abstract class classInfo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCRActivity.classInfo>
  {
    public class emailResponse : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      PMCRActivity.classInfo.emailResponse>
    {
      public emailResponse()
        : base("Email Response")
      {
      }
    }
  }

  public new abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCRActivity.ownerID>
  {
  }

  public new abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCRActivity.subject>
  {
  }

  public new abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMCRActivity.startDate>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMCRActivity.createdDateTime>
  {
  }

  public new abstract class isPrivate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMCRActivity.isPrivate>
  {
  }

  public new abstract class childKey : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMCRActivity.childKey>
  {
  }

  public abstract class displayStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCRActivity.displayStatus>
  {
  }
}
