// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRActivity
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
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.SM;
using PX.TM;
using PX.Web.UI;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <summary>
/// Represents the base entity for activities that are used to log interactions between users and contacts.
/// The records of this type are created and edited on the Activity (CR306010) form
/// (which corresponds to the <see cref="T:PX.Objects.EP.CRActivityMaint" /> graph),
/// the Events (CR306030) form (<see cref="T:PX.Objects.EP.EPEventMaint" />),
/// and the Tasks (CR306020) form (<see cref="T:PX.Objects.CR.CRTaskMaint" />).
/// The <see cref="T:PX.Objects.CR.SMEmail">emails</see> are created and edited on the Emails (CR306015) form
/// (<see cref="T:PX.Objects.CR.CREmailActivityMaint" />).
/// Also, activities (events, tasks, and emails) can be created on the <b>Activities</b> tab
/// of any document form that uses activities; the logic on this tab is implemented by the
/// <see cref="T:PX.Objects.CR.Extensions.ActivityDetailsExt`4" /> generic graph extension.
/// For instance, activities for leads use: <see cref="!:LeadMaint_ActivityDetailsExt" />.
/// </summary>
[PXCacheName("Activity")]
[CRCacheIndependentPrimaryGraph(typeof (CREmailActivityMaint), typeof (Select<CRSMEmail, Where<CRSMEmail.noteID, Equal<Current<CRActivity.noteID>>>>))]
[CRCacheIndependentPrimaryGraph(typeof (CREmailActivityMaint), typeof (Where<Current<CRActivity.classID>, Equal<CRActivityClass.email>>))]
[CRCacheIndependentPrimaryGraph(typeof (CRTaskMaint), typeof (Select<CRActivity, Where<CRActivity.noteID, Equal<Current<CRActivity.noteID>>, And<CRActivity.classID, Equal<CRActivityClass.task>>>>))]
[CRCacheIndependentPrimaryGraph(typeof (CRTaskMaint), typeof (Where<Current<CRActivity.classID>, Equal<CRActivityClass.task>>))]
[CRCacheIndependentPrimaryGraph(typeof (EPEventMaint), typeof (Select<CRActivity, Where<CRActivity.noteID, Equal<Current<CRActivity.noteID>>, And<CRActivity.classID, Equal<CRActivityClass.events>>>>))]
[CRCacheIndependentPrimaryGraph(typeof (EPEventMaint), typeof (Where<Current<CRActivity.classID>, Equal<CRActivityClass.events>>))]
[CRCacheIndependentPrimaryGraph(typeof (CRActivityMaint), typeof (Select<CRActivity, Where<CRActivity.noteID, Equal<Current<CRActivity.noteID>>, And<CRActivity.classID, Equal<CRActivityClass.activity>>>>))]
[CRCacheIndependentPrimaryGraph(typeof (CRActivityMaint), typeof (Where<Current<CRActivity.classID>, Equal<CRActivityClass.activity>>))]
[CRCacheIndependentPrimaryGraph(typeof (CRActivityMaint), typeof (Where<Current<CRActivity.classID>, Equal<PMActivityClass.timeActivity>>))]
[Serializable]
public class CRActivity : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssign, INotable
{
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  /// <inheritdoc />
  [PXSequentialNote(SuppressActivitiesCount = true, IsKey = true)]
  [PXUIField(DisplayName = "ID")]
  [PXTimeTag(typeof (CRActivity.noteID))]
  [CRActivityStatisticFormulas]
  [PXSelector(typeof (CRActivity.noteID), new System.Type[] {typeof (CRActivity.noteID)})]
  [PXReferentialIntegrityCheck]
  public virtual Guid? NoteID { get; set; }

  /// <summary>
  /// The identifier of the parent task or event of the current activity.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.CRActivity.NoteID" /> field.
  /// </value>
  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Parent Activity")]
  [PXSelector(typeof (Search<CRActivity.noteID, Where<CRActivity.classID, Equal<CRActivityClass.task>, Or<CRActivity.classID, Equal<CRActivityClass.events>>>>), new System.Type[] {typeof (CRActivity.classInfo), typeof (CRActivity.subject), typeof (CRActivity.uistatus), typeof (CRActivity.startDate), typeof (CRActivity.endDate), typeof (CRActivity.ownerID), typeof (CRActivity.priority), typeof (CRActivity.refNoteID), typeof (CRActivity.source)})]
  [PXRestrictor(typeof (Where<CRActivity.noteID, NotEqual<Current<CRActivity.noteID>>>), "The activity cannot be its own parent activity.", new System.Type[] {})]
  public virtual Guid? ParentNoteID { get; set; }

  /// <summary>
  /// Contains the type of the related entity, that is specified in <see cref="P:PX.Objects.CR.CRActivity.RefNoteID" />.
  /// </summary>
  [PXString]
  [PXUIField(DisplayName = "Related Entity Type")]
  [PXEntityTypeList]
  [PXUIEnabled(typeof (Where<BqlOperand<IsMobile, IBqlBool>.IsEqual<False>>))]
  [PXDBScalar(typeof (Search<Note.entityType, Where<Note.noteID, Equal<CRActivity.refNoteID>>>))]
  public virtual 
  #nullable disable
  string RefNoteIDType { get; set; }

  /// <summary>
  /// Contains the <see cref="P:PX.Data.INotable.NoteID" /> value of the related entity.
  /// This activity is displayed on the <b>Activities</b> tab of the entity's form.
  /// </summary>
  /// <remarks>The related document may or may not implement the <see cref="T:PX.Data.INotable" /> interface,
  /// but it must have a field marked with the <see cref="T:PX.Data.PXNoteAttribute" /> attribute
  /// with the <see cref="P:PX.Data.PXNoteAttribute.ShowInReferenceSelector" /> property set to <see langword="true" />.
  /// </remarks>
  [ActivityEntityIDSelector(typeof (CRActivity.refNoteIDType), typeof (CRActivity.contactID), typeof (CRActivity.bAccountID))]
  [PXDBGuid(false)]
  [PXParent(typeof (Select<CRActivityStatistics, Where<CRActivityStatistics.noteID, Equal<Current<CRActivity.refNoteID>>>>), LeaveChildren = true, ParentCreate = true)]
  [PXUIField(DisplayName = "Related Entity")]
  [PXDefault]
  [PXUIRequired(typeof (Where<CRActivity.refNoteIDType, IsNotNull>))]
  public virtual Guid? RefNoteID { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Data.INotable.NoteID" /> value of the related document.
  /// It is similar to <see cref="P:PX.Objects.CR.CRActivity.RefNoteID" />, but contains an additional reference
  /// to the source of the activity (usually the source of the activity is a document).
  /// For example, the (<see cref="T:PX.Objects.CR.CRMassMailMaint" />) graph fills this value with a link to the source
  /// <see cref="T:PX.Objects.CR.CRCampaign" /> or <see cref="T:PX.Objects.CR.CRMarketingList" /> object.
  /// </summary>
  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Related Document")]
  public virtual Guid? DocumentNoteID { get; set; }

  /// <summary>
  /// The description of the entity whose <tt>NoteID</tt> value is specified as <see cref="P:PX.Objects.CR.CRActivity.RefNoteID" />.
  /// </summary>
  /// <value>
  /// The description is retrieved by the
  /// <see cref="M:PX.Data.EntityHelper.GetEntityDescription(System.Nullable{System.Guid},System.Type)" /> method.
  /// </value>
  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Related Entity", Enabled = false)]
  [PXFormula(typeof (PX.Objects.CR.EntityDescription<CRActivity.refNoteID>))]
  public virtual string Source { get; set; }

  /// <summary>The class of the activity.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.CRActivityClass" /> class.
  /// The default values are <see cref="F:PX.Objects.CR.CRActivityClass.Activity" /> for the <see cref="T:PX.Objects.EP.CRActivityMaint" /> graph,
  /// <see cref="F:PX.Objects.CR.CRActivityClass.Task" /> for the <see cref="T:PX.Objects.CR.CRTaskMaint" /> graph,
  /// <see cref="F:PX.Objects.CR.CRActivityClass.Event" /> for the <see cref="T:PX.Objects.EP.EPEventMaint" /> graph,
  /// and <see cref="F:PX.Objects.CR.CRActivityClass.Email" /> for the <see cref="T:PX.Objects.CR.CREmailActivityMaint" /> graph.
  /// This field must be specified at the initialization of an email and not be changed afterwards.
  /// </value>
  [PXDBInt]
  [CRActivityClass]
  [PXDefault(typeof (CRActivityClass.activity))]
  [PXUIField]
  [PXFieldDescription]
  public virtual int? ClassID { get; set; }

  /// <summary>
  /// The URL of the icon that corresponds to the <see cref="P:PX.Objects.CR.CRActivity.ClassID" /> of the activity.
  /// </summary>
  /// <value>
  /// The icon is displayed in a grid to indicate the activity type.
  /// The available values are listed in the <see cref="T:PX.Objects.CR.CRActivity.classIcon" /> class.
  /// </value>
  [PXUIField(DisplayName = "Class Icon", IsReadOnly = true)]
  [PXImage]
  [PXFormula(typeof (Switch<Case<Where<CRActivity.classID, Equal<CRActivityClass.task>>, CRActivity.classIcon.task, Case<Where<CRActivity.classID, Equal<CRActivityClass.events>>, CRActivity.classIcon.events, Case<Where<CRActivity.classID, Equal<CRActivityClass.email>, And<CRActivity.incoming, Equal<True>>>, CRActivity.classIcon.email, Case<Where<CRActivity.classID, Equal<CRActivityClass.email>, And<CRActivity.outgoing, Equal<True>>>, CRActivity.classIcon.emailResponse>>>>, Selector<Current2<CRActivity.type>, EPActivityType.imageUrl>>))]
  public virtual string ClassIcon { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  [PXFormula(typeof (Switch<Case<Where<Current2<CRActivity.noteID>, IsNull>, StringEmpty, Case<Where<CRActivity.classID, Equal<CRActivityClass.activity>, And<CRActivity.type, IsNotNull>>, Selector<CRActivity.type, EPActivityType.description>, Case<Where<CRActivity.classID, Equal<CRActivityClass.email>, And<CRActivity.incoming, Equal<True>>>, CRActivity.classInfo.emailResponse>>>, String<CRActivity.classID>>))]
  public virtual string ClassInfo { get; set; }

  /// <summary>The type of the activity.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.EP.EPActivityType.Type" /> field.
  /// </value>
  [PXDBString(5, IsFixed = true, IsUnicode = false)]
  [PXUIField(DisplayName = "Type", Required = true)]
  [PXSelector(typeof (Search<EPActivityType.type, Where<Current<CRActivity.classID>, Equal<EPActivityType.classID>>>), DescriptionField = typeof (EPActivityType.description))]
  [PXRestrictor(typeof (Where<EPActivityType.active, Equal<True>>), "Activity Type '{0}' is not active.", new System.Type[] {typeof (EPActivityType.type)})]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPActivityType.application, NotEqual<PXActivityApplicationAttribute.portal>>>>>.Or<BqlOperand<EPActivityType.isSystem, IBqlBool>.IsEqual<True>>>), "The '{0}' activity type is originated by Portal. Only Portal can create activities of this type.", new System.Type[] {typeof (EPActivityType.type)})]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPActivityType.application, NotEqual<PXActivityApplicationAttribute.system>>>>>.Or<BqlOperand<UnattendedMode, IBqlBool>.IsEqual<True>>>), "The '{0}' activity type is originated by System. Only System can create activities of this type.\r\n ", new System.Type[] {typeof (EPActivityType.type)})]
  [PXDefault(typeof (FbqlSelect<SelectFromBase<EPActivityType, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPActivityType.application, Equal<PXActivityApplicationAttribute.backend>>>>, And<BqlOperand<EPActivityType.classID, IBqlInt>.IsEqual<BqlField<CRActivity.classID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<EPActivityType.active, IBqlBool>.IsEqual<True>>>, EPActivityType>.SearchFor<EPActivityType.type>))]
  [PXFormula(typeof (Default<CRActivity.classID>))]
  public virtual string Type { get; set; }

  /// <summary>The summary description of the activity.</summary>
  [PXDBString(256 /*0x0100*/, InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  [PXNavigateSelector(typeof (CRActivity.subject))]
  public virtual string Subject { get; set; }

  /// <summary>The location of the event.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Location")]
  public virtual string Location { get; set; }

  /// <summary>The HTML body of the activity.</summary>
  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Activity Details")]
  public virtual string Body { get; set; }

  /// <summary>The priority of the activity.</summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Priority")]
  [PXDefault(1)]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"Low", "Normal", "High"})]
  public virtual int? Priority { get; set; }

  /// <summary>
  /// The URL of the icon to indicate the <see cref="P:PX.Objects.CR.CRActivity.Priority" /> of the activity.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.CRActivity.priorityIcon" /> class.
  /// If the value is <see langword="null" />, no icon is used.
  /// </value>
  [PXUIField(DisplayName = "Priority Icon", IsReadOnly = true)]
  [PXImage(HeaderImage = "control@PriorityHead")]
  [PXFormula(typeof (Switch<Case<Where<CRActivity.priority, Equal<int0>>, CRActivity.priorityIcon.low, Case<Where<CRActivity.priority, Equal<int2>>, CRActivity.priorityIcon.high>>>))]
  public virtual string PriorityIcon { get; set; }

  /// <summary>The status of the activity.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.ActivityStatusListAttribute" /> class.
  /// The default value is <see cref="F:PX.Objects.CR.ActivityStatusListAttribute.Open" />.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [ActivityStatus]
  [PXUIField(DisplayName = "Status")]
  [PXDefault("OP")]
  public virtual string UIStatus { get; set; }

  /// <summary>
  /// Indicates whether the activity is overdue
  /// (the <see cref="P:PX.Objects.CR.CRActivity.UIStatus" /> is neither <see cref="F:PX.Objects.CR.ActivityStatusListAttribute.Completed" />
  /// nor <see cref="F:PX.Objects.CR.ActivityStatusListAttribute.Canceled" /> and <see cref="P:PX.Objects.CR.CRActivity.EndDate" /> is passed).
  /// </summary>
  [PXBool]
  public virtual bool? IsOverdue
  {
    [PXDependsOnFields(new System.Type[] {typeof (CRActivity.uistatus), typeof (CRActivity.endDate)})] get
    {
      int num;
      if (this.UIStatus != "CD" && this.UIStatus != "CL" && this.EndDate.HasValue)
      {
        DateTime? endDate = this.EndDate;
        DateTime now = PXTimeZoneInfo.Now;
        num = endDate.HasValue ? (endDate.GetValueOrDefault() < now ? 1 : 0) : 0;
      }
      else
        num = 0;
      return new bool?(num != 0);
    }
  }

  /// <summary>
  /// The URL of the icon for a completed activity (<see cref="P:PX.Objects.CR.CRActivity.UIStatus" /> is
  /// <see cref="F:PX.Objects.CR.ActivityStatusListAttribute.Completed" />).
  /// </summary>
  [PXUIField(DisplayName = "Complete Icon", IsReadOnly = true)]
  [PXImage(HeaderImage = "control@CompleteHead")]
  [PXFormula(typeof (Switch<Case<Where<CRActivity.uistatus, Equal<ActivityStatusListAttribute.completed>>, CRActivity.isCompleteIcon.completed>>))]
  public virtual string IsCompleteIcon { get; set; }

  /// <summary>The identifier of the task or event category.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.EP.EPEventCategory.CategoryID">EPEventCategory.CategoryID</see> field.
  /// </value>
  [PXDBInt]
  [PXSelector(typeof (EPEventCategory.categoryID), SubstituteKey = typeof (EPEventCategory.description))]
  [PXUIField(DisplayName = "Category")]
  public virtual int? CategoryID { get; set; }

  /// <summary>Specifies whether this event is an all-day event.</summary>
  [EPAllDay(typeof (CRActivity.startDate), typeof (CRActivity.endDate))]
  [PXUIField(DisplayName = "All Day")]
  [PXDefault(false)]
  [PXFormula(typeof (Switch<Case<Where<CRActivity.classID, Equal<CRActivityClass.task>>, True>, False>))]
  public virtual bool? AllDay { get; set; }

  /// <summary>The custom time zone of the activity.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Data.PXTimeZoneAttribute" />.
  /// If it does not set to <see langword="null" /> then adjusts <see cref="P:PX.Objects.CR.CRActivity.StartDate" /> and <see cref="P:PX.Objects.CR.CRActivity.EndDate" />
  /// field values to the specified time zone.
  /// </value>
  /// <remarks>
  /// It is currently used only by <see cref="T:PX.Objects.EP.EPEventMaint" />.
  /// </remarks>
  [PXDBString(32 /*0x20*/)]
  [PXUIField(DisplayName = "Time Zone")]
  [PXTimeZone(false)]
  [PXUIEnabled(typeof (Where<CRActivity.allDay, Equal<False>>))]
  public virtual string TimeZone { get; set; }

  /// <summary>
  /// The date and time when activity was completed
  /// (<see cref="P:PX.Objects.CR.CRActivity.UIStatus" /> was set to <see cref="F:PX.Objects.CR.ActivityStatusListAttribute.Completed" />).
  /// </summary>
  [PXDBDateAndTime(DisplayNameDate = "Completed On", InputMask = "g")]
  [PXUIField(DisplayName = "Completed On", Enabled = false)]
  [PXVerifyEndDate(typeof (CRActivity.startDate), AllowAutoChange = true, AutoChangeWarning = true)]
  [PXDefault(typeof (BqlOperand<CRActivity.completedDate, IBqlDateTime>.IfNullThen<BqlOperand<PXDateAndTimeAttribute.now, IBqlDateTime>.When<BqlOperand<CRActivity.uistatus, IBqlString>.IsEqual<ActivityStatusListAttribute.completed>>.ElseNull>))]
  [PXFormula(typeof (Default<CRActivity.uistatus>))]
  public virtual DateTime? CompletedDate { get; set; }

  /// <summary>
  /// The day of week of <see cref="P:PX.Objects.CR.CRActivity.StartDate" />.
  /// </summary>
  /// <value>
  /// The field is calculated automatically on the basis of <see cref="P:PX.Objects.CR.CRActivity.StartDate" />.
  /// The following values are possible: <see langword="null" /> (when <see cref="P:PX.Objects.CR.CRActivity.StartDate" /> is <see langword="null" />),
  /// and the values defined by the <see cref="T:System.DayOfWeek" /> enumeration and converted to <see langword="int" />.
  /// </value>
  [PXInt]
  [PXUIField(DisplayName = "Day Of Week")]
  [PX.Objects.EP.DayOfWeek]
  public virtual int? DayOfWeek
  {
    [PXDependsOnFields(new System.Type[] {typeof (CRActivity.startDate)})] get
    {
      DateTime? startDate = this.StartDate;
      return !startDate.HasValue ? new int?() : new int?((int) startDate.Value.DayOfWeek);
    }
  }

  /// <summary>
  /// The estimation of the task completion expressed as a percentage.
  /// </summary>
  [PXDBInt(MinValue = 0, MaxValue = 100)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Completion (%)")]
  public virtual int? PercentCompletion { get; set; }

  /// <inheritdoc />
  [PXChildUpdatable(AutoRefresh = true)]
  [Owner(typeof (CRActivity.workgroupID))]
  [PXDefault(typeof (Coalesce<Search<EPCompanyTreeMember.contactID, Where<EPCompanyTreeMember.workGroupID, Equal<Current<CRActivity.workgroupID>>, And<EPCompanyTreeMember.contactID, Equal<Current<AccessInfo.contactID>>>>>, Search<Contact.contactID, Where<Contact.contactID, Equal<Current<AccessInfo.contactID>>, And<Current<CRActivity.workgroupID>, IsNull>>>>))]
  [PXFormula(typeof (Default<CRActivity.workgroupID>))]
  public virtual int? OwnerID { get; set; }

  /// <summary>The start date and time of the event.</summary>
  [EPStartDate(AllDayField = typeof (CRActivity.allDay), DisplayName = "Start Date", DisplayNameDate = "Start Date", DisplayNameTime = "Start Time")]
  [PXFormula(typeof (TimeZoneNow))]
  [PXUIField(DisplayName = "Start Date")]
  public virtual DateTime? StartDate { get; set; }

  /// <summary>The end date and time of the event.</summary>
  [EPEndDate(typeof (CRActivity.classID), typeof (CRActivity.startDate), null, AllDayField = typeof (CRActivity.allDay), DisplayNameDate = "End Date", DisplayNameTime = "End Time")]
  [PXUIField(DisplayName = "End Date")]
  public virtual DateTime? EndDate { get; set; }

  /// <exclude />
  [PXString(IsUnicode = true)]
  [PXUIField]
  [PXDependsOnFields(new System.Type[] {typeof (CRActivity.subject), typeof (CRActivity.type), typeof (CRActivity.startDate)})]
  public virtual string SelectorDescription { get; set; }

  /// <inheritdoc />
  [PXDBInt]
  [PXChildUpdatable(UpdateRequest = true)]
  [PXUIField(DisplayName = "Workgroup")]
  [PXCompanyTreeSelector]
  public virtual int? WorkgroupID { get; set; }

  /// <summary>
  /// Specifies whether this activity was created by
  /// an external user on the portal.
  /// </summary>
  [PXDBBool]
  [PXUIField(Visible = false)]
  public virtual bool? IsExternal { get; set; }

  /// <summary>
  /// Specifies whether this activity is hidden from external users
  /// and not visible on the portal site.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Internal")]
  [PXFormula(typeof (IsNull<Selector<CRActivity.type, EPActivityType.privateByDefault>, False>))]
  public virtual bool? IsPrivate { get; set; }

  /// <summary>
  /// Specifies whether this activity is provides case solution or not.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Case Solution Provided", Visible = false)]
  [PXDefault(false)]
  public virtual bool? ProvidesCaseSolution { get; set; }

  /// <summary>Specifies whether this activity is incoming.</summary>
  /// <value>
  /// The value is equal to <see cref="P:PX.Objects.EP.EPActivityType.Incoming">EPActivityType.Incoming</see> of the current <see cref="P:PX.Objects.CR.CRActivity.Type" />.
  /// </value>
  [PXDBBool]
  [PXFormula(typeof (Switch<Case<Where<CRActivity.type, IsNotNull>, Selector<CRActivity.type, EPActivityType.incoming>>, False>))]
  [PXUIField(DisplayName = "Incoming")]
  public virtual bool? Incoming { get; set; }

  /// <summary>Specifies whether this activity is outgoing.</summary>
  /// <value>
  /// The value is equal to <see cref="P:PX.Objects.EP.EPActivityType.Outgoing">EPActivityType.Outgoing</see> of the current <see cref="P:PX.Objects.CR.CRActivity.Type" />.
  /// </value>
  [PXDBBool]
  [PXFormula(typeof (Switch<Case<Where<CRActivity.type, IsNotNull>, Selector<CRActivity.type, EPActivityType.outgoing>>, False>))]
  [PXUIField(DisplayName = "Outgoing")]
  public virtual bool? Outgoing { get; set; }

  /// <summary>
  /// Specifies whether the activity should be included in the exchange synchronization.
  /// </summary>
  /// <value>
  /// The value is used in the exchange integration (see <see cref="P:PX.Objects.CS.FeaturesSet.ExchangeIntegration" />).
  /// The default value is <see langword="true" />.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Synchronize")]
  public virtual bool? Synchronize { get; set; }

  /// <summary>
  /// The identifier of the related <see cref="T:PX.Objects.CR.BAccount">business account</see>.
  /// Along with <see cref="P:PX.Objects.CR.CRActivity.ContactID" />, this field is used as an additional reference,
  /// but unlike <see cref="P:PX.Objects.CR.CRActivity.RefNoteID" /> and <see cref="P:PX.Objects.CR.CRActivity.DocumentNoteID" /> it is used for specific entities.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Related Account")]
  [PXSelector(typeof (Search<BAccount.bAccountID>))]
  public virtual int? BAccountID { get; set; }

  /// <summary>
  /// The identifier of the related <see cref="T:PX.Objects.CR.Contact">contact</see>.
  /// Along with <see cref="P:PX.Objects.CR.CRActivity.BAccountID" />, this field is used as an additional reference,
  /// but unlike <see cref="P:PX.Objects.CR.CRActivity.RefNoteID" /> and <see cref="P:PX.Objects.CR.CRActivity.DocumentNoteID" /> it is used for specific entities.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Related Contact")]
  [PXSelector(typeof (Contact.contactID))]
  public virtual int? ContactID { get; set; }

  /// <summary>
  /// Returns either additional information about the related entity or the last error message. The property is
  /// used by the <see cref="T:PX.Objects.CR.CREmailActivityMaint" /> graph to show additional infomation about the <see cref="T:PX.Objects.CR.SMEmail" /> status.
  /// </summary>
  [PXString(InputMask = "")]
  [PXUIField]
  public virtual string EntityDescription { get; set; }

  /// <summary>
  /// The event status to be displayed on your schedule if it is public.
  /// </summary>
  /// <value>
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Show As")]
  [ShowAsList]
  [PXDefault(typeof (ShowAsListAttribute.busy))]
  public virtual int? ShowAsID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Locked")]
  public virtual bool? IsLocked { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.EP.EPActivityType.application" /> of original Activity Type
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.EP.EPActivityType.Application" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Originated By")]
  [PXActivityApplication]
  [PXFormula(typeof (Selector<CRActivity.type, EPActivityType.application>))]
  public virtual int? Application { get; set; }

  /// <exclude />
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DeletedDatabaseRecord { get; set; }

  [PXDBCreatedByID(DontOverrideValue = true)]
  [PXUIField(Enabled = false)]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Created At", Enabled = false)]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  /// <summary>
  /// The identifier of the activity that is a response to the current activity.
  /// </summary>
  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Response Provided In", Enabled = false, FieldClass = "CaseCommitmentsTracking")]
  [PXSelector(typeof (SearchFor<CRActivity.noteID>.Where<BqlOperand<CRActivity.incoming, IBqlBool>.IsEqual<BqlField<CRActivity.outgoing, IBqlBool>.FromCurrent>>), new System.Type[] {typeof (CRActivity.classInfo), typeof (CRActivity.startDate), typeof (CRActivity.subject), typeof (CRActivity.uistatus), typeof (CRActivity.ownerID), typeof (CRActivity.completedDate)})]
  [PXUIVisible(typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRActivity.refNoteIDType, Equal<TypeNameOf<CRCase>>>>>>.And<BqlOperand<CRActivity.refNoteID, IBqlGuid>.IsNotNull>))]
  public virtual Guid? ResponseActivityNoteID { get; set; }

  /// <summary>
  /// The due date and time for the response to the current activity.
  /// </summary>
  [PXDBDate(PreserveTime = true, InputMask = "g")]
  [PXUIField(DisplayName = "Response Due", Enabled = false, FieldClass = "CaseCommitmentsTracking")]
  [PXUIVisible(typeof (BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRActivity.responseDueDateTime, IsNotNull>>>, And<BqlOperand<CRActivity.refNoteIDType, IBqlString>.IsEqual<TypeNameOf<CRCase>>>>>.And<BqlOperand<CRActivity.refNoteID, IBqlGuid>.IsNotNull>))]
  public virtual DateTime? ResponseDueDateTime { get; set; }

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<CRActivity>.By<CRActivity.noteID>
  {
    public static CRActivity Find(PXGraph graph, Guid? noteID, PKFindOptions options = 0)
    {
      return (CRActivity) PrimaryKeyOf<CRActivity>.By<CRActivity.noteID>.FindBy(graph, (object) noteID, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Business Account</summary>
    public class BusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccountR.bAccountID>.ForeignKeyOf<CRActivity>.By<CRActivity.bAccountID>
    {
    }

    /// <summary>Contact</summary>
    public class Contact : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CRActivity>.By<CRActivity.contactID>
    {
    }

    /// <summary>Parent Task</summary>
    public class ParentActivity : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CRActivity>.By<CRActivity.parentNoteID>
    {
    }

    /// <summary>Response Activity</summary>
    public class ResponseActivity : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CRActivity>.By<CRActivity.responseActivityNoteID>
    {
    }

    /// <summary>Type</summary>
    public class ActivityType : 
      PrimaryKeyOf<EPActivityType>.By<EPActivityType.type>.ForeignKeyOf<CRActivity>.By<CRActivity.type>
    {
    }

    /// <summary>Category</summary>
    public class Category : 
      PrimaryKeyOf<EPEventCategory>.By<EPEventCategory.categoryID>.ForeignKeyOf<CRActivity>.By<CRActivity.categoryID>
    {
    }

    /// <summary>Owner</summary>
    public class Owner : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CRActivity>.By<CRActivity.ownerID>
    {
    }

    /// <summary>Workgroup</summary>
    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<CRActivity>.By<CRActivity.workgroupID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRActivity.selected>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRActivity.noteID>
  {
  }

  public abstract class parentNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRActivity.parentNoteID>
  {
  }

  public abstract class refNoteIDType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRActivity.refNoteIDType>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRActivity.refNoteID>
  {
  }

  public abstract class documentNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRActivity.documentNoteID>
  {
  }

  public abstract class source : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRActivity.source>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.classID>
  {
  }

  public abstract class classIcon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRActivity.classIcon>
  {
    public class task : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRActivity.classIcon.task>
    {
      public task()
        : base(Sprite.Main.GetFullUrl("Task"))
      {
      }
    }

    public class events : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRActivity.classIcon.events>
    {
      public events()
        : base(Sprite.Main.GetFullUrl("Event"))
      {
      }
    }

    public class email : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRActivity.classIcon.email>
    {
      public email()
        : base(Sprite.Main.GetFullUrl("MailSend"))
      {
      }
    }

    public class emailResponse : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      CRActivity.classIcon.emailResponse>
    {
      public emailResponse()
        : base(Sprite.Main.GetFullUrl("MailReceive"))
      {
      }
    }
  }

  public abstract class classInfo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRActivity.classInfo>
  {
    public class emailResponse : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      CRActivity.classInfo.emailResponse>
    {
      public emailResponse()
        : base("Email Response")
      {
      }
    }
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRActivity.type>
  {
  }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRActivity.subject>
  {
  }

  public abstract class location : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRActivity.location>
  {
  }

  public abstract class body : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRActivity.body>
  {
  }

  public abstract class priority : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.priority>
  {
  }

  public abstract class priorityIcon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRActivity.priorityIcon>
  {
    public class low : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRActivity.priorityIcon.low>
    {
      public low()
        : base(Sprite.Control.GetFullUrl("PriorityLow"))
      {
      }
    }

    public class high : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRActivity.priorityIcon.high>
    {
      public high()
        : base(Sprite.Control.GetFullUrl("PriorityHigh"))
      {
      }
    }
  }

  public abstract class uistatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRActivity.uistatus>
  {
  }

  public abstract class isOverdue : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRActivity.isOverdue>
  {
  }

  public abstract class isCompleteIcon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRActivity.isCompleteIcon>
  {
    public class completed : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      CRActivity.isCompleteIcon.completed>
    {
      public completed()
        : base(Sprite.Control.GetFullUrl("Complete"))
      {
      }
    }
  }

  public abstract class categoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.categoryID>
  {
  }

  public abstract class allDay : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRActivity.allDay>
  {
  }

  public abstract class timeZone : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRActivity.timeZone>
  {
  }

  public abstract class completedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRActivity.completedDate>
  {
  }

  public abstract class dayOfWeek : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.dayOfWeek>
  {
  }

  public abstract class percentCompletion : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.percentCompletion>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.ownerID>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRActivity.startDate>
  {
    public abstract class startDate_date : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CRActivity.startDate.startDate_date>
    {
    }

    public abstract class startDate_time : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CRActivity.startDate.startDate_time>
    {
    }
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRActivity.endDate>
  {
    public abstract class endDate_date : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CRActivity.endDate.endDate_date>
    {
    }

    public abstract class endDate_time : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CRActivity.endDate.endDate_time>
    {
    }
  }

  public abstract class selectorDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRActivity.selectorDescription>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.workgroupID>
  {
  }

  public abstract class isExternal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRActivity.isExternal>
  {
  }

  public abstract class isPrivate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRActivity.isPrivate>
  {
  }

  public abstract class providesCaseSolution : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRActivity.providesCaseSolution>
  {
  }

  public abstract class incoming : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRActivity.incoming>
  {
  }

  public abstract class outgoing : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRActivity.outgoing>
  {
  }

  public abstract class synchronize : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRActivity.synchronize>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.bAccountID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.contactID>
  {
  }

  public abstract class entityDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRActivity.entityDescription>
  {
  }

  public abstract class showAsID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.showAsID>
  {
  }

  public abstract class isLocked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRActivity.isLocked>
  {
  }

  public abstract class application : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.application>
  {
  }

  public abstract class deletedDatabaseRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRActivity.deletedDatabaseRecord>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRActivity.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRActivity.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRActivity.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRActivity.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRActivity.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRActivity.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRActivity.Tstamp>
  {
  }

  public abstract class responseActivityNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRActivity.responseActivityNoteID>
  {
  }

  public abstract class responseDueDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRActivity.responseDueDateTime>
  {
  }
}
