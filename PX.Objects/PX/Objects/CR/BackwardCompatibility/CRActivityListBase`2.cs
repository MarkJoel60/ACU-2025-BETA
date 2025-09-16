// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BackwardCompatibility.CRActivityListBase`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Common;
using PX.Common.Mail;
using PX.Data;
using PX.Data.EP;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Reports;
using PX.Reports.Controls;
using PX.Reports.Data;
using PX.Reports.Mail;
using PX.SM;
using PX.TM;
using PX.Web.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.BackwardCompatibility;

/// <exclude />
[Obsolete]
[PXDynamicButton(new string[] {"NewTask", "NewEvent", "ViewActivity", "NewMailActivity", "RegisterActivity", "OpenActivityOwner", "ViewAllActivities", "NewActivity"}, new string[] {"Add Task", "Add Event", "Details", "Add Email", "Register Activity", "OpenActivityOwner", "View Activities", "Add Activity"}, TranslationKeyType = typeof (PX.Objects.CR.Messages))]
public class CRActivityListBase<TPrimaryView, TActivity> : 
  CRActivityListBase<TActivity>,
  IActivityList
  where TPrimaryView : class, IBqlTable, new()
  where TActivity : CRPMTimeActivity, new()
{
  protected const string _PRIMARY_WORKGROUP_ID = "WorkgroupID";
  private int? _entityDefaultEMailAccountId;
  private readonly BqlCommand _originalCommand;
  private readonly string _refField;
  private readonly System.Type _refBqlField;
  private EntityHelper _EntityHelper;

  public EntityHelper EntityHelper
  {
    get
    {
      if (this._EntityHelper == null)
        this._EntityHelper = new EntityHelper(((PXSelectBase) this)._Graph);
      return this._EntityHelper;
    }
  }

  [InjectDependency]
  protected IReportLoaderService ReportLoader { get; private set; }

  [InjectDependency]
  protected IReportDataBinder ReportDataBinder { get; private set; }

  public CRActivityListBase(PXGraph graph)
    : this(graph, (Delegate) null)
  {
  }

  public CRActivityListBase(PXGraph graph, Delegate handler)
  {
    ((PXSelectBase) this)._Graph = graph;
    GraphHelper.EnsureCachePersistence(((PXSelectBase) this)._Graph, typeof (TActivity));
    GraphHelper.EnsureCachePersistence(((PXSelectBase) this)._Graph, typeof (CRReminder));
    PXCache cach = ((PXSelectBase) this)._Graph.Caches[typeof (TActivity)];
    this.ReadNoteIDFieldInfo(out this._refField, out this._refBqlField);
    if (typeof (CRActivity).IsAssignableFrom(typeof (TPrimaryView)))
    {
      PXGraph.RowPersistedEvents rowPersisted = graph.RowPersisted;
      CRActivityListBase<TPrimaryView, TActivity> activityListBase = this;
      // ISSUE: virtual method pointer
      PXRowPersisted pxRowPersisted = new PXRowPersisted((object) activityListBase, __vmethodptr(activityListBase, Table_RowPersisted));
      rowPersisted.AddHandler<TPrimaryView>(pxRowPersisted);
    }
    PXGraph.RowSelectedEvents rowSelected = graph.RowSelected;
    CRActivityListBase<TPrimaryView, TActivity> activityListBase1 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) activityListBase1, __vmethodptr(activityListBase1, Activity_RowSelected));
    rowSelected.AddHandler<TActivity>(pxRowSelected);
    PXGraph.FieldDefaultingEvents fieldDefaulting1 = graph.FieldDefaulting;
    System.Type type1 = typeof (CRActivity);
    string name1 = typeof (CRActivity.refNoteID).Name;
    CRActivityListBase<TPrimaryView, TActivity> activityListBase2 = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting1 = new PXFieldDefaulting((object) activityListBase2, __vmethodptr(activityListBase2, Activity_RefNoteID_FieldDefaulting));
    fieldDefaulting1.AddHandler(type1, name1, pxFieldDefaulting1);
    PXGraph.FieldDefaultingEvents fieldDefaulting2 = graph.FieldDefaulting;
    System.Type type2 = typeof (CRSMEmail);
    string name2 = typeof (CRSMEmail.refNoteID).Name;
    CRActivityListBase<TPrimaryView, TActivity> activityListBase3 = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting2 = new PXFieldDefaulting((object) activityListBase3, __vmethodptr(activityListBase3, Activity_RefNoteID_FieldDefaulting));
    fieldDefaulting2.AddHandler(type2, name2, pxFieldDefaulting2);
    PXGraph.FieldDefaultingEvents fieldDefaulting3 = graph.FieldDefaulting;
    System.Type type3 = typeof (TActivity);
    string name3 = typeof (CRActivity.refNoteID).Name;
    CRActivityListBase<TPrimaryView, TActivity> activityListBase4 = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting3 = new PXFieldDefaulting((object) activityListBase4, __vmethodptr(activityListBase4, Activity_RefNoteID_FieldDefaulting));
    fieldDefaulting3.AddHandler(type3, name3, pxFieldDefaulting3);
    PXGraph.FieldSelectingEvents fieldSelecting = graph.FieldSelecting;
    System.Type type4 = typeof (TActivity);
    string name4 = typeof (CRActivity.body).Name;
    CRActivityListBase<TPrimaryView, TActivity> activityListBase5 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting = new PXFieldSelecting((object) activityListBase5, __vmethodptr(activityListBase5, Activity_Body_FieldSelecting));
    fieldSelecting.AddHandler(type4, name4, pxFieldSelecting);
    this.AddActions(graph);
    this.AddPreview(graph);
    string name5 = typeof (CRActivity.noteID).Name;
    PXUIFieldAttribute.SetVisible(cach, (object) null, name5, false);
    this._originalCommand = CRActivityListBase<TActivity>.GenerateOriginalCommand();
    if ((object) handler == null)
      ((PXSelectBase) this).View = new PXView(graph, false, this.OriginalCommand);
    else
      ((PXSelectBase) this).View = new PXView(graph, false, this.OriginalCommand, handler);
    this.SetCommandCondition(handler);
  }

  private void AddPreview(PXGraph graph)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CRActivityListBase<TPrimaryView, TActivity>.\u003C\u003Ec__DisplayClass19_0 cDisplayClass190 = new CRActivityListBase<TPrimaryView, TActivity>.\u003C\u003Ec__DisplayClass19_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass190.graph = graph;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass190.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    cDisplayClass190.graph.Initialized += new PXGraphInitializedDelegate((object) cDisplayClass190, __methodptr(\u003CAddPreview\u003Eb__0));
  }

  protected void AddActions(PXGraph graph)
  {
    PXGraph graph1 = graph;
    CRActivityListBase<TPrimaryView, TActivity> activityListBase1 = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate handler1 = new PXButtonDelegate((object) activityListBase1, __vmethodptr(activityListBase1, NewTask));
    PXEventSubscriberAttribute[] subscriberAttributeArray1 = Array.Empty<PXEventSubscriberAttribute>();
    this.AddAction(graph1, "NewTask", "Create Task", true, handler1, subscriberAttributeArray1);
    PXGraph graph2 = graph;
    CRActivityListBase<TPrimaryView, TActivity> activityListBase2 = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate handler2 = new PXButtonDelegate((object) activityListBase2, __vmethodptr(activityListBase2, NewEvent));
    PXEventSubscriberAttribute[] subscriberAttributeArray2 = Array.Empty<PXEventSubscriberAttribute>();
    this.AddAction(graph2, "NewEvent", "Create Event", true, handler2, subscriberAttributeArray2);
    PXGraph graph3 = graph;
    CRActivityListBase<TPrimaryView, TActivity> activityListBase3 = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate handler3 = new PXButtonDelegate((object) activityListBase3, __vmethodptr(activityListBase3, ViewActivity));
    this.AddAction(graph3, "ViewActivity", "Details", handler3);
    PXGraph graph4 = graph;
    CRActivityListBase<TPrimaryView, TActivity> activityListBase4 = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate handler4 = new PXButtonDelegate((object) activityListBase4, __vmethodptr(activityListBase4, NewMailActivity));
    PXEventSubscriberAttribute[] subscriberAttributeArray3 = Array.Empty<PXEventSubscriberAttribute>();
    this.AddAction(graph4, "NewMailActivity", "Create Email", true, handler4, subscriberAttributeArray3);
    PXGraph graph5 = graph;
    string empty = string.Empty;
    CRActivityListBase<TPrimaryView, TActivity> activityListBase5 = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate handler5 = new PXButtonDelegate((object) activityListBase5, __vmethodptr(activityListBase5, OpenActivityOwner));
    this.AddAction(graph5, "OpenActivityOwner", empty, handler5);
    PXGraph graph6 = graph;
    CRActivityListBase<TPrimaryView, TActivity> activityListBase6 = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate handler6 = new PXButtonDelegate((object) activityListBase6, __vmethodptr(activityListBase6, ViewAllActivities));
    this.AddAction(graph6, "ViewAllActivities", "View Activities", handler6);
    this.AddActivityQuickActionsAsMenu(graph);
  }

  private void AddActivityQuickActionsAsMenu(PXGraph graph)
  {
    List<ActivityService.IActivityType> source = (List<ActivityService.IActivityType>) null;
    try
    {
      source = ServiceLocator.Current.GetInstance<IActivityService>().GetActivityTypes().ToList<ActivityService.IActivityType>();
    }
    catch (Exception ex)
    {
    }
    PXGraph graph1 = graph;
    string displayName = PXMessages.LocalizeNoPrefix("Create Activity");
    int num = source == null ? 0 : (source.Count > 0 ? 1 : 0);
    CRActivityListBase<TPrimaryView, TActivity> activityListBase = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate handler = new PXButtonDelegate((object) activityListBase, __vmethodptr(activityListBase, NewActivityByType));
    PXEventSubscriberAttribute[] subscriberAttributeArray = new PXEventSubscriberAttribute[1]
    {
      (PXEventSubscriberAttribute) new PXButtonAttribute()
      {
        DisplayOnMainToolbar = false,
        OnClosingPopup = (PXSpecialButtonType) 4
      }
    };
    PXAction pxAction1 = this.AddAction(graph1, "NewActivity", displayName, num != 0, handler, subscriberAttributeArray);
    if (source == null || source.Count <= 0)
      return;
    foreach (ActivityService.IActivityType iactivityType in source.Where<ActivityService.IActivityType>((Func<ActivityService.IActivityType, bool>) (t => t.IsDefault.GetValueOrDefault())).Concat<ActivityService.IActivityType>(source.Where<ActivityService.IActivityType>((Func<ActivityService.IActivityType, bool>) (t => !t.IsDefault.GetValueOrDefault()))))
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CRActivityListBase<TPrimaryView, TActivity>.\u003C\u003Ec__DisplayClass21_0 cDisplayClass210 = new CRActivityListBase<TPrimaryView, TActivity>.\u003C\u003Ec__DisplayClass21_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass210.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass210.type = iactivityType;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PXAction pxAction2 = this.AddAction(graph, "NewActivity" + cDisplayClass210.type.Type.TrimEnd(), PXMessages.LocalizeFormatNoPrefix("Create {0}", new object[1]
      {
        (object) cDisplayClass210.type.Description
      }), true, new PXButtonDelegate((object) cDisplayClass210, __methodptr(\u003CAddActivityQuickActionsAsMenu\u003Eb__2)), (PXEventSubscriberAttribute) new PXButtonAttribute()
      {
        CommitChanges = true,
        DisplayOnMainToolbar = false,
        OnClosingPopup = (PXSpecialButtonType) 4
      });
      pxAction1.AddMenuAction(pxAction2);
    }
  }

  internal void AddAction(
    PXGraph graph,
    string name,
    string displayName,
    PXButtonDelegate handler)
  {
    this.AddAction(graph, name, displayName, true, handler, (PXEventSubscriberAttribute[]) null);
  }

  internal PXAction AddAction(
    PXGraph graph,
    string name,
    string displayName,
    bool visible,
    PXButtonDelegate handler,
    params PXEventSubscriberAttribute[] attrs)
  {
    PXUIFieldAttribute pxuiFieldAttribute = new PXUIFieldAttribute()
    {
      DisplayName = PXMessages.LocalizeNoPrefix(displayName),
      MapEnableRights = (PXCacheRights) 1
    };
    if (!visible)
      pxuiFieldAttribute.Visible = false;
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>()
    {
      (PXEventSubscriberAttribute) pxuiFieldAttribute
    };
    if (attrs != null)
      subscriberAttributeList.AddRange(((IEnumerable<PXEventSubscriberAttribute>) attrs).Where<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (attr => attr != null)));
    PXNamedAction<TPrimaryView> pxNamedAction1 = new PXNamedAction<TPrimaryView>(graph, name, handler, subscriberAttributeList.ToArray());
    graph.Actions[name] = (PXAction) pxNamedAction1;
    string str = name + "_Workflow";
    PXNamedAction<TPrimaryView> pxNamedAction2 = new PXNamedAction<TPrimaryView>(graph, str, handler, subscriberAttributeList.ToArray());
    graph.Actions[str] = (PXAction) pxNamedAction2;
    return (PXAction) pxNamedAction1;
  }

  protected void CreateActivity(int classId, string type)
  {
    PXGraph newActivity = this.CreateNewActivity(classId, type);
    if (newActivity == null)
      return;
    PXRedirectHelper.TryRedirect(newActivity, (PXRedirectHelper.WindowMode) 3);
  }

  protected virtual PXGraph CreateNewActivity(int classId, string type)
  {
    object obj = (object) new CRActivity()
    {
      ClassID = new int?(classId),
      Type = type
    };
    System.Type primaryGraphType = this.EntityHelper.GetPrimaryGraphType(ref obj, true);
    if (!PXAccess.VerifyRights(primaryGraphType))
    {
      ((PXSelectBase) this)._Graph.Views[((PXSelectBase) this)._Graph.PrimaryView].Ask((object) null, "Access denied", PX.Objects.CR.Messages.FormNoAccessRightsMessage(primaryGraphType), (MessageButtons) 0, (MessageIcon) 1);
      return (PXGraph) null;
    }
    PXCache cach;
    if (classId == 4)
    {
      cach = PXGraph.CreateInstance(primaryGraphType).Caches[typeof (CRSMEmail)];
      if (cach == null)
        return (PXGraph) null;
      CRSMEmail instance = (CRSMEmail) ((PXSelectBase) this)._Graph.Caches[typeof (CRSMEmail)].CreateInstance();
      instance.ClassID = new int?(classId);
      instance.Type = type;
      CRSMEmail message = GraphHelper.InitNewRow<CRSMEmail>((PXCache<CRSMEmail>) ((PXSelectBase) this)._Graph.Caches[typeof (CRSMEmail)], instance);
      int? currentOwnerId = EmployeeMaint.GetCurrentOwnerID(((PXSelectBase) this)._Graph);
      int? parentGroup = this.GetParentGroup();
      message.OwnerID = currentOwnerId;
      if (message.OwnerID.HasValue && OwnerAttribute.BelongsToWorkGroup(((PXSelectBase) this)._Graph, parentGroup, message.OwnerID))
        message.WorkgroupID = parentGroup;
      message.MailAccountID = this.DefaultEMailAccountId;
      this.FillMailReply(message);
      this.FillMailTo(message);
      if (message.RefNoteID.HasValue)
        this.FillMailCC(message, message.RefNoteID);
      this.FillMailSubject(message);
      message.Body = this.GenerateMailBody();
      message.ClassID = new int?(classId);
      ((PXSelectBase) this)._Graph.Caches[typeof (CRSMEmail)].SetValueExt((object) message, typeof (CRActivity.type).Name, !string.IsNullOrEmpty(type) ? (object) type : (object) message.Type);
      if (((PXSelectBase) this)._Graph.IsDirty)
      {
        if (((PXSelectBase) this)._Graph.IsMobile)
        {
          PXCache cache = ((PXSelectBase) this)._Graph.Views[((PXSelectBase) this)._Graph.PrimaryView].Cache;
          if (cache.Current != null)
            cache.SetStatus(cache.Current, (PXEntryStatus) 1);
        }
        ((PXSelectBase) this)._Graph.Actions.PressSave();
      }
      cach.Insert((object) message);
    }
    else
    {
      cach = PXGraph.CreateInstance(primaryGraphType).Caches[typeof (CRActivity)];
      if (cach == null)
        return (PXGraph) null;
      CRActivity instance = (CRActivity) ((PXSelectBase) this)._Graph.Caches[typeof (CRActivity)].CreateInstance();
      instance.ClassID = new int?(classId);
      instance.Type = type;
      CRActivity crActivity = GraphHelper.InitNewRow<CRActivity>((PXCache<CRActivity>) ((PXSelectBase) this)._Graph.Caches[typeof (CRActivity)], instance);
      int? currentOwnerId = EmployeeMaint.GetCurrentOwnerID(((PXSelectBase) this)._Graph);
      int? parentGroup = this.GetParentGroup();
      crActivity.OwnerID = currentOwnerId;
      if (crActivity.OwnerID.HasValue && OwnerAttribute.BelongsToWorkGroup(((PXSelectBase) this)._Graph, parentGroup, crActivity.OwnerID))
        crActivity.WorkgroupID = parentGroup;
      if (((PXSelectBase) this)._Graph.IsDirty)
      {
        if (((PXSelectBase) this)._Graph.IsMobile)
        {
          PXCache cache = ((PXSelectBase) this)._Graph.Views[((PXSelectBase) this)._Graph.PrimaryView].Cache;
          if (cache.Current != null)
            cache.SetStatus(cache.Current, (PXEntryStatus) 1);
        }
        ((PXSelectBase) this)._Graph.Actions.PressSave();
      }
      CRActivity destData = cach.Insert((object) crActivity) as CRActivity;
      UDFHelper.CopyAttributes(((PXSelectBase) this)._Graph.Views[((PXSelectBase) this)._Graph.PrimaryView].Cache, ((PXSelectBase) this)._Graph.Views[((PXSelectBase) this)._Graph.PrimaryView].Cache.Current, cach, (object) destData, (string) null);
    }
    this.CreateTimeActivity(cach, classId);
    foreach (PXCache pxCache in ((IEnumerable<PXCache>) cach.Graph.Caches.Caches).Where<PXCache>((Func<PXCache, bool>) (c => c.IsDirty)))
      pxCache.IsDirty = false;
    return cach.Graph;
  }

  protected virtual void CreateTimeActivity(PXCache graphType, int classId)
  {
  }

  private int? GetParentGroup()
  {
    PXCache cach = ((PXSelectBase) this)._Graph.Caches[typeof (TPrimaryView)];
    return (int?) cach.GetValue(cach.Current, "WorkgroupID");
  }

  protected PMTimeActivity CurrentTimeActivity
  {
    get
    {
      return PXResultset<PMTimeActivity>.op_Implicit(((PXSelectBase) this)._Graph.Caches[typeof (TActivity)].With<PXCache, TActivity>((Func<PXCache, TActivity>) (_ => (TActivity) _.Current)).With<TActivity, Guid>((Func<TActivity, Guid>) (_ => _.TimeActivityNoteID ?? Guid.Empty)).With<Guid, PXResultset<PMTimeActivity>>((Func<Guid, PXResultset<PMTimeActivity>>) (_ => PXSelectBase<PMTimeActivity, PXSelect<PMTimeActivity, Where<PMTimeActivity.noteID, Equal<Required<CRPMTimeActivity.timeActivityNoteID>>>>.Config>.Select(((PXSelectBase) this)._Graph, new object[1]
      {
        (object) _
      }))));
    }
  }

  [Obsolete]
  public void SendNotification(
    string sourceType,
    string notifications,
    int? branchID,
    IDictionary<string, string> parameters,
    bool massProcess = false,
    IList<Guid?> attachments = null)
  {
    NotificationGenerator notificationProvider = this.CreateNotificationProvider(sourceType, notifications, branchID, parameters, attachments);
    notificationProvider.MassProcessMode = massProcess;
    if (notificationProvider == null || !notificationProvider.Send().Any<CRSMEmail>())
      throw new PXException("Email send failed. Email isn't created or email recipient list is empty.");
  }

  [Obsolete]
  public virtual NotificationGenerator CreateNotificationProvider(
    string sourceType,
    string notifications,
    int? branchID,
    IDictionary<string, string> parameters,
    IList<Guid?> attachments = null)
  {
    if (notifications == null)
      return (NotificationGenerator) null;
    IList<string> list = (IList<string>) ((IEnumerable<string>) notifications.Split(',')).Select<string, string>((Func<string, string>) (n => n?.Trim())).Where<string>((Func<string, bool>) (cd => !string.IsNullOrEmpty(cd))).ToList<string>();
    return this.CreateNotificationProvider(sourceType, list, branchID, parameters, attachments);
  }

  [Obsolete]
  public virtual NotificationGenerator CreateNotificationProvider(
    string sourceType,
    IList<string> notificationCDs,
    int? branchID,
    IDictionary<string, string> parameters,
    IList<Guid?> attachments = null)
  {
    PXCache cach1 = ((PXSelectBase) this)._Graph.Caches[typeof (TPrimaryView)];
    if (cach1.Current == null)
      throw new PXException("Email send failed. Source notification object not defined to proceed operation.");
    IList<NotificationSetup> setupNotifications = (IList<NotificationSetup>) this.GetSetupNotifications(sourceType, notificationCDs);
    PXCache cach2 = ((PXSelectBase) this)._Graph.Caches[typeof (TPrimaryView)];
    TPrimaryView current = (TPrimaryView) cach2.Current;
    object instance = cach2.CreateInstance();
    cach2.RestoreCopy(instance, (object) current);
    TPrimaryView row = (TPrimaryView) instance;
    TActivity activity = GraphHelper.InitNewRow<TActivity>((PXCache<TActivity>) ((PXSelectBase) this)._Graph.Caches[typeof (TActivity)], default (TActivity));
    object sourceRow = this.GetSourceRow(sourceType, activity);
    NotificationUtility utility = new NotificationUtility(((PXSelectBase) this)._Graph);
    RecipientList recipientList = (RecipientList) null;
    TemplateNotificationGenerator notificationProvider = (TemplateNotificationGenerator) null;
    for (int index = 0; index < setupNotifications.Count; ++index)
    {
      NotificationSetup setup = setupNotifications[index];
      NotificationSource source1;
      if (sourceRow == null)
        source1 = utility.GetSource(setup);
      else
        source1 = utility.GetSource(sourceType, sourceRow, (IList<Guid?>) new Guid?[1]
        {
          (Guid?) setup?.SetupID
        }, branchID ?? ((PXSelectBase) this)._Graph.Accessinfo.BranchID);
      NotificationSource source2 = source1;
      if (source2 == null && sourceType == "Project")
        source2 = utility.GetSource(sourceType, (object) row, (IList<Guid?>) new Guid?[1]
        {
          (Guid?) setup?.SetupID
        }, branchID ?? ((PXSelectBase) this)._Graph.Accessinfo.BranchID);
      if (source2 == null)
        throw new PXException("There is no active notification source to process the operation.");
      if (notificationProvider == null)
      {
        int? nullable = source2.EMailAccountID ?? setup.EMailAccountID ?? this.DefaultEMailAccountId;
        if (!nullable.HasValue)
          throw new PXException("The email account is empty. Please define the Default Email Account in Email Preferences.");
        if (recipientList == null)
          recipientList = utility.GetRecipients(sourceType, sourceRow, source2);
        notificationProvider = TemplateNotificationGenerator.Create((object) row, source2.NotificationID);
        notificationProvider.MailAccountId = nullable;
        notificationProvider.RefNoteID = activity.RefNoteID;
        notificationProvider.DocumentNoteID = activity.DocumentNoteID;
        notificationProvider.BAccountID = activity.BAccountID;
        notificationProvider.ContactID = activity.ContactID;
        notificationProvider.Watchers = (IEnumerable<NotificationRecipient>) recipientList;
      }
      if (source2.ReportID != null)
      {
        Report report = this.ReportLoader.LoadReport(source2.ReportID, (IPXResultset) null);
        if (report == null)
          throw new ArgumentException(PXMessages.LocalizeFormatNoPrefixNLA("Report '{0}' cannot be found", new object[1]
          {
            (object) source2.ReportID
          }), "reportId");
        this.ReportLoader.InitDefaultReportParameters(report, parameters ?? CRActivityListBase<TPrimaryView, TActivity>.KeyParameters(cach1));
        report.MailSettings.Format = ReportNotificationGenerator.ConvertFormat(source2.Format);
        ReportNode reportNode = this.ReportDataBinder.ProcessReportDataBinding(report);
        reportNode.SendMailMode = true;
        Message message = reportNode.Groups.Select<GroupNode, MailSettings>((Func<GroupNode, MailSettings>) (g => g.MailSettings)).Where<MailSettings>((Func<MailSettings, bool>) (msg => msg != null && msg.ShouldSerialize())).Select<MailSettings, Message>((Func<MailSettings, Message>) (msg => new Message(MailSettings.op_Implicit(msg), reportNode, MailSettings.op_Implicit(msg)))).FirstOrDefault<Message>();
        if (message == null)
        {
          if (index == 0)
            throw new InvalidOperationException(PXMessages.LocalizeFormatNoPrefixNLA("Email cannot be created for the specified report '{0}' because the report has not been generated or the email settings are not specified.", new object[1]
            {
              (object) source2.ReportID
            }));
          continue;
        }
        if (index == 0)
        {
          bool flag = false;
          if (notificationProvider.Body == null)
          {
            string text = ((MailSender.MailMessageT) message).Content.Body;
            flag = text == null;
            if (flag || !CRActivityListBase<TPrimaryView, TActivity>.IsHtml(text))
              text = Tools.ConvertSimpleTextToHtml(((MailSender.MailMessageT) message).Content.Body);
            notificationProvider.Body = text;
            notificationProvider.BodyFormat = "H";
          }
          notificationProvider.Subject = string.IsNullOrEmpty(notificationProvider.Subject) ? ((MailSender.MailMessageT) message).Content.Subject : notificationProvider.Subject;
          notificationProvider.To = string.IsNullOrEmpty(notificationProvider.To) ? ((MailSender.MailMessageT) message).Addressee.To : notificationProvider.To;
          notificationProvider.Cc = string.IsNullOrEmpty(notificationProvider.Cc) ? ((MailSender.MailMessageT) message).Addressee.Cc : notificationProvider.Cc;
          notificationProvider.Bcc = string.IsNullOrEmpty(notificationProvider.Bcc) ? ((MailSender.MailMessageT) message).Addressee.Bcc : notificationProvider.Bcc;
          if (!string.IsNullOrEmpty(((GroupMessage) message).TemplateID))
          {
            NotificationGenerator notification = TemplateNotificationGenerator.Create((object) row, ((GroupMessage) message).TemplateID).ParseNotification();
            if (string.IsNullOrEmpty(notificationProvider.Body) | flag)
              notificationProvider.Body = notification.Body;
            if (string.IsNullOrEmpty(notificationProvider.Subject))
              notificationProvider.Subject = notification.Subject;
            if (string.IsNullOrEmpty(notificationProvider.To))
              notificationProvider.To = notification.To;
            if (string.IsNullOrEmpty(notificationProvider.Cc))
              notificationProvider.Cc = notification.Cc;
            if (string.IsNullOrEmpty(notificationProvider.Bcc))
              notificationProvider.Bcc = notification.Bcc;
          }
          if (string.IsNullOrEmpty(notificationProvider.Subject))
            notificationProvider.Subject = ((ItemNode) ((ItemNode) reportNode).Report).Name;
        }
        foreach (ReportStream attachment in message.Attachments)
        {
          if (notificationProvider.Body == null && notificationProvider.BodyFormat == "H" && attachment.MimeType == "text/html")
            notificationProvider.Body = attachment.Encoding.GetString(attachment.GetBytes());
          else
            notificationProvider.AddAttachment(attachment.Name, attachment.GetBytes(), attachment.CID);
        }
        if (attachments != null)
        {
          foreach (Guid? attachment in (IEnumerable<Guid?>) attachments)
          {
            if (attachment.HasValue)
              notificationProvider.AddAttachmentLink(attachment.Value);
          }
        }
      }
      string recipientFromContext = this.GetPrimaryRecipientFromContext(utility, sourceType, sourceRow, source2);
      if (!string.IsNullOrEmpty(recipientFromContext))
        notificationProvider.To = recipientFromContext;
      switch (source2.RecipientsBehavior)
      {
        case "O":
          notificationProvider.To = (string) null;
          notificationProvider.Cc = (string) null;
          notificationProvider.Bcc = (string) null;
          continue;
        default:
          continue;
      }
    }
    return (NotificationGenerator) notificationProvider;
  }

  [Obsolete]
  protected virtual RecipientList GetRecipientsFromContext(
    NotificationUtility utility,
    string type,
    object row,
    NotificationSource source)
  {
    return (RecipientList) null;
  }

  [Obsolete]
  protected virtual string GetPrimaryRecipientFromContext(
    NotificationUtility utility,
    string type,
    object row,
    NotificationSource source)
  {
    return (string) null;
  }

  [Obsolete]
  protected virtual object GetSourceRow(string sourceType, TActivity activity)
  {
    return activity.BAccountID.With<int?, object>((Func<int?, object>) (_ => new EntityHelper(((PXSelectBase) this)._Graph).GetEntityRowByID(typeof (BAccountR), new int?(_.Value)))) ?? activity.RefNoteID.With<Guid?, object>((Func<Guid?, object>) (_ => new EntityHelper(((PXSelectBase) this)._Graph).GetEntityRow(typeof (BAccountR), new Guid?(_.Value))));
  }

  [Obsolete]
  protected static IDictionary<string, string> KeyParameters(PXCache sourceCache)
  {
    IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
    foreach (string key in (IEnumerable<string>) sourceCache.Keys)
    {
      object valueExt = sourceCache.GetValueExt(sourceCache.Current, key);
      dictionary[key] = valueExt?.ToString();
    }
    return dictionary;
  }

  [Obsolete]
  protected List<NotificationSetup> GetSetupNotifications(
    string sourceType,
    IList<string> notificationCDs)
  {
    List<NotificationSetup> setupNotifications = new List<NotificationSetup>();
    for (int index = 0; index < notificationCDs.Count; ++index)
    {
      (NotificationSetup SetupWithBranch, NotificationSetup SetupWithoutBranch) tuple = new NotificationUtility(((PXSelectBase) this)._Graph).SearchSetup(sourceType, notificationCDs[index], new int?());
      setupNotifications.Add((tuple.SetupWithBranch ?? tuple.SetupWithoutBranch) ?? throw new PXException("Email send failed. Notification Settings '{0}' not found.", new object[1]
      {
        (object) notificationCDs[index]
      }));
    }
    return setupNotifications;
  }

  private static Guid? GetNoteId(PXGraph graph, object row)
  {
    if (row == null)
      return new Guid?();
    System.Type type = row.GetType();
    string noteField = EntityHelper.GetNoteField(type);
    return PXNoteAttribute.GetNoteID(graph.Caches[type], row, noteField);
  }

  private static bool IsHtml(string text)
  {
    if (string.IsNullOrEmpty(text))
      return false;
    int num1 = text.IndexOf("<html", StringComparison.CurrentCultureIgnoreCase);
    int num2 = text.IndexOf("<body", StringComparison.CurrentCultureIgnoreCase);
    return num1 > -1 && num2 > -1 && num2 > num1;
  }

  protected virtual void FillMailReply(CRSMEmail message)
  {
    Mailbox mailbox = (Mailbox) null;
    bool flag = message.MailReply != null && Mailbox.TryParse(message.MailReply, ref mailbox) && !string.IsNullOrEmpty(mailbox.Address);
    if (flag)
      flag = PXSelectBase<EMailAccount, PXSelect<EMailAccount, Where<EMailAccount.address, Equal<Required<EMailAccount.address>>>>.Config>.Select(((PXSelectBase) this)._Graph, new object[1]
      {
        (object) mailbox.Address
      }).Count > 0;
    string str = message.MailReply;
    if (!flag)
      str = this.DefaultEMailAccountId.With<int?, EMailAccount>((Func<int?, EMailAccount>) (_ => PXResultset<EMailAccount>.op_Implicit(PXSelectBase<EMailAccount, PXSelect<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Required<EMailAccount.emailAccountID>>>>.Config>.Select(((PXSelectBase) this)._Graph, new object[1]
      {
        (object) _.Value
      })))).With<EMailAccount, string>((Func<EMailAccount, string>) (_ => _.Address));
    if (string.IsNullOrEmpty(str))
    {
      EMailAccount emailAccount = PXResultset<EMailAccount>.op_Implicit(PXSelectBase<EMailAccount, PXSelect<EMailAccount>.Config>.SelectWindowed(((PXSelectBase) this)._Graph, 0, 1, Array.Empty<object>()));
      if (emailAccount != null)
        str = emailAccount.Address;
    }
    message.MailReply = str;
  }

  protected virtual void FillMailTo(CRSMEmail message)
  {
    CRActivityListBase<TPrimaryView, TActivity>.GetEmailHandler getNewEmailAddress = this.GetNewEmailAddress;
    string o = getNewEmailAddress != null ? getNewEmailAddress() : (string) null;
    if (string.IsNullOrEmpty(o))
      return;
    message.MailTo = o.With<string, string>((Func<string, string>) (_ => _.Trim()));
  }

  protected virtual void FillMailCC(CRSMEmail message, Guid? refNoteId)
  {
    if (!refNoteId.HasValue)
      return;
    IActivityService instance = ServiceLocator.Current.GetInstance<IActivityService>();
    message.MailCc = PXDBEmailAttribute.AppendAddresses(message.MailCc, instance?.GetEmailAddressesForCc(((PXSelectBase) this)._Graph, new Guid?(refNoteId.Value)));
  }

  protected virtual void FillMailSubject(CRSMEmail message)
  {
    if (string.IsNullOrEmpty(this.DefaultSubject))
      return;
    message.Subject = this.DefaultSubject;
  }

  protected virtual string GenerateMailBody()
  {
    return PXRichTextConverter.NormalizeHtml(MailAccountManager.AppendSignature((string) null, ((PXSelectBase) this)._Graph, (MailAccountManager.SignatureOptions) 0));
  }

  [PXButton]
  [PXUIField(Visible = false)]
  public virtual IEnumerable OpenActivityOwner(PXAdapter adapter)
  {
    if (((PXSelectBase) this).Cache.Current is CRActivity current)
    {
      PX.Objects.EP.EPEmployee epEmployee = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelectReadonly<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Required<CRActivity.ownerID>>>>.Config>.Select(((PXSelectBase) this)._Graph, new object[1]
      {
        (object) current.OwnerID
      }));
      if (epEmployee != null)
        PXRedirectHelper.TryRedirect(((PXSelectBase) this)._Graph.Caches[typeof (PX.Objects.EP.EPEmployee)], (object) epEmployee, string.Empty, (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  [PXButton]
  [PXShortCut(true, false, false, new char[] {'K', 'C'})]
  public virtual IEnumerable NewTask(PXAdapter adapter)
  {
    this.CreateActivity(0, (string) null);
    return adapter.Get();
  }

  [PXButton]
  [PXShortCut(true, false, false, new char[] {'E', 'C'})]
  public virtual IEnumerable NewEvent(PXAdapter adapter)
  {
    this.CreateActivity(1, (string) null);
    return adapter.Get();
  }

  [PXButton]
  [PXShortCut(true, false, false, new char[] {'A', 'C'})]
  public virtual IEnumerable NewActivity(PXAdapter adapter) => this.NewActivityByType(adapter);

  [PXButton]
  protected virtual IEnumerable NewActivityByType(PXAdapter adapter)
  {
    return this.NewActivityByType(adapter, adapter.Menu);
  }

  [PXButton]
  protected virtual IEnumerable NewActivityByType(PXAdapter adapter, string type)
  {
    this.CreateActivity(2, type);
    return adapter.Get();
  }

  [PXButton]
  [PXShortCut(true, false, false, new char[] {'A', 'M'})]
  public virtual IEnumerable NewMailActivity(PXAdapter adapter)
  {
    this.CreateActivity(4, (string) null);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewAllActivities(PXAdapter adapter)
  {
    ActivitiesMaint instance = PXGraph.CreateInstance<ActivitiesMaint>();
    ((PXSelectBase<ActivitySource>) instance.Filter).Current.NoteID = GraphHelper.InitNewRow<TActivity>((PXCache<TActivity>) ((PXSelectBase) this)._Graph.Caches[typeof (TActivity)], default (TActivity)).RefNoteID;
    throw new PXPopupRedirectException((PXGraph) instance, string.Empty, true);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewActivity(PXAdapter adapter)
  {
    this.NavigateToActivity(((PXSelectBase) this)._Graph.Caches[typeof (TActivity)].Current);
    return adapter.Get();
  }

  public string DefaultActivityType
  {
    get
    {
      string defaultActivityType = (string) null;
      EPSetup epSetup = PXResultset<EPSetup>.op_Implicit(PXSelectBase<EPSetup, PXSelect<EPSetup>.Config>.Select(((PXSelectBase) this)._Graph, Array.Empty<object>()));
      if (epSetup != null && !string.IsNullOrEmpty(epSetup.DefaultActivityType))
        defaultActivityType = epSetup.DefaultActivityType;
      return defaultActivityType;
    }
  }

  private void NavigateToActivity(object row)
  {
    PXGraph instance = PXGraph.CreateInstance(this.EntityHelper.GetPrimaryGraphType((object) (row as CRActivity), true));
    PXCache cach1 = instance.Caches[row.GetType()];
    if (cach1.GetValue(row, typeof (CRPMTimeActivity.timeActivityNoteID).Name) != null && cach1.GetValue(row, typeof (CRPMTimeActivity.timeActivityNoteID).Name).Equals(cach1.GetValue(row, typeof (CRPMTimeActivity.timeActivityRefNoteID).Name)))
    {
      PMTimeActivity currentTimeActivity = this.CurrentTimeActivity;
      PXCache cach2 = instance.Caches[typeof (CRActivity)];
      PXCache cach3 = instance.Caches[typeof (PMTimeActivity)];
      object obj = GraphHelper.NonDirtyInsert(cach2);
      cach2.SetValue(obj, typeof (CRActivity.noteID).Name, cach3.GetValue((object) currentTimeActivity, typeof (CRPMTimeActivity.refNoteID).Name));
      cach2.SetValue(obj, typeof (CRActivity.type).Name, (object) this.DefaultActivityType);
      cach2.SetValue(obj, typeof (CRActivity.subject).Name, cach3.GetValue((object) currentTimeActivity, typeof (CRPMTimeActivity.summary).Name));
      cach2.SetValue(obj, typeof (CRActivity.ownerID).Name, cach3.GetValue((object) currentTimeActivity, typeof (CRPMTimeActivity.ownerID).Name));
      cach2.SetValue(obj, typeof (CRActivity.startDate).Name, cach3.GetValue((object) currentTimeActivity, typeof (CRPMTimeActivity.date).Name));
      cach2.SetValue(obj, typeof (CRActivity.uistatus).Name, (object) "CD");
      cach2.Normalize();
      cach3.SetValue((object) currentTimeActivity, typeof (PMTimeActivity.summary).Name, cach2.GetValue(obj, typeof (CRActivity.subject).Name));
      cach3.Current = (object) currentTimeActivity;
      cach3.SetStatus(cach3.Current, (PXEntryStatus) 1);
      PXRedirectHelper.TryRedirect(instance, (PXRedirectHelper.WindowMode) 3);
    }
    else
      PXRedirectHelper.TryRedirect(instance, row, (PXRedirectHelper.WindowMode) 3);
  }

  public PXAction ButtonViewAllActivities
  {
    get => ((PXSelectBase) this)._Graph.Actions["ViewAllActivities"];
  }

  public virtual CRActivityListBase<TPrimaryView, TActivity>.GetEmailHandler GetNewEmailAddress { get; set; }

  public string DefaultSubject { get; set; }

  public int? DefaultEMailAccountId
  {
    get
    {
      int? emailAccountId = (int?) MailAccountManager.GetUserSettingsEmailAccount(new Guid?(PXAccess.GetUserID()), true)?.EmailAccountID;
      if (emailAccountId.HasValue)
        return emailAccountId;
      int? defaultEmailAccountId = this._entityDefaultEMailAccountId;
      if (defaultEmailAccountId.HasValue)
        return defaultEmailAccountId;
      return MailAccountManager.GetSystemSettingsEmailAccount(new Guid?(PXAccess.GetUserID()), true)?.EmailAccountID;
    }
    set => this._entityDefaultEMailAccountId = value;
  }

  internal void CorrectButtons(PXCache sender, object row, PXEntryStatus status)
  {
    row = row ?? sender.Current;
    bool flag = (row == null ? 0 : (Array.IndexOf<PXEntryStatus>(this.NotEditableStatuses, status) < 0 ? 1 : 0)) != 0 && ((PXSelectBase) this).View.Cache.AllowInsert;
    PXActionCollection actions = sender.Graph.Actions;
    actions["NewTask"].SetEnabled(flag);
    actions["NewTask_Workflow"].SetEnabled(flag);
    actions["NewEvent"].SetEnabled(flag);
    actions["NewEvent_Workflow"].SetEnabled(flag);
    actions["NewMailActivity"].SetEnabled(flag);
    actions["NewMailActivity_Workflow"].SetEnabled(flag);
    actions["NewActivity"].SetEnabled(flag);
    if (!(actions["NewActivity"].GetState(row) is PXButtonState state) || state.Menus == null)
      return;
    foreach (ButtonMenu menu in state.Menus)
    {
      actions[menu.Command].SetEnabled(flag);
      actions[menu.Command + "_Workflow"].SetEnabled(flag);
    }
  }

  protected virtual void Table_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.Operation != 2 || e.TranStatus != 1)
      return;
    object row = e.Row;
    this.CorrectButtons(sender, row, (PXEntryStatus) 0);
  }

  protected virtual void Activity_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is TActivity row))
      return;
    int? classId = row.ClassID;
    int num1 = 0;
    int? nullable;
    if (!(classId.GetValueOrDefault() == num1 & classId.HasValue))
    {
      nullable = row.ClassID;
      if (nullable.GetValueOrDefault() != 1)
        return;
    }
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    int num5 = 0;
    foreach (PXResult<CRChildActivity, PMTimeActivity> pxResult in PXSelectBase<CRChildActivity, PXSelectJoin<CRChildActivity, InnerJoin<PMTimeActivity, On<PMTimeActivity.refNoteID, Equal<CRChildActivity.noteID>>>, Where<CRChildActivity.parentNoteID, Equal<Required<CRChildActivity.parentNoteID>>, And<Where<PMTimeActivity.isCorrected, NotEqual<True>, Or<PMTimeActivity.isCorrected, IsNull>>>>>.Config>.Select(((PXSelectBase) this)._Graph, new object[1]
    {
      (object) row.NoteID
    }))
    {
      PMTimeActivity pmTimeActivity = PXResult<CRChildActivity, PMTimeActivity>.op_Implicit(pxResult);
      int num6 = num2;
      nullable = pmTimeActivity.TimeSpent;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      num2 = num6 + valueOrDefault1;
      int num7 = num3;
      nullable = pmTimeActivity.OvertimeSpent;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      num3 = num7 + valueOrDefault2;
      int num8 = num4;
      nullable = pmTimeActivity.TimeBillable;
      int valueOrDefault3 = nullable.GetValueOrDefault();
      num4 = num8 + valueOrDefault3;
      int num9 = num5;
      nullable = pmTimeActivity.OvertimeBillable;
      int valueOrDefault4 = nullable.GetValueOrDefault();
      num5 = num9 + valueOrDefault4;
    }
    row.TimeSpent = new int?(num2);
    row.OvertimeSpent = new int?(num3);
    row.TimeBillable = new int?(num4);
    row.OvertimeBillable = new int?(num5);
  }

  protected virtual void Activity_RefNoteID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    CRActivityListBase<TPrimaryView, TActivity>.GetNoteId(sender.Graph, sender.Graph.Caches[typeof (TPrimaryView)].Current);
    PXCache cach = sender.Graph.Caches[this._refBqlField.DeclaringType];
    e.NewValue = cach.GetValue(cach.Current, this._refBqlField.Name);
  }

  protected virtual void Activity_Body_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is TActivity row) || row.ClassID.GetValueOrDefault() != 4)
      return;
    SMEmailBody smEmailBody = PXResultset<SMEmailBody>.op_Implicit(PXSelectBase<SMEmailBody, PXSelect<SMEmailBody, Where<SMEmailBody.refNoteID, Equal<Required<CRPMTimeActivity.noteID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) row.NoteID
    }));
    e.ReturnValue = (object) smEmailBody.Body;
    UploadFileMaintenance.OverrideAccessRights(PXContext.Session, row.NoteID);
  }

  protected virtual void ReadNoteIDFieldInfo(out string noteField, out System.Type noteBqlField)
  {
    PXCache cach = ((PXSelectBase) this)._Graph.Caches[typeof (TPrimaryView)];
    noteField = EntityHelper.GetNoteField(typeof (TPrimaryView));
    noteBqlField = !string.IsNullOrEmpty(this._refField) ? cach.GetBqlField(this._refField) : throw new ArgumentException($"Type '{MainTools.GetLongName(typeof (TPrimaryView))}' must contain field with PX.Data.NoteIDAttribute on it");
  }

  protected virtual void SetCommandCondition(Delegate handler = null)
  {
    System.Type type1 = this._refBqlField;
    System.Type type2;
    if (typeof (BAccount).IsAssignableFrom(typeof (TPrimaryView)))
    {
      type2 = typeof (CRPMTimeActivity.bAccountID);
      type1 = ((PXSelectBase) this).View.Graph.Caches[typeof (TPrimaryView)].GetBqlField(typeof (BAccount.bAccountID).Name);
    }
    else if (typeof (PX.Objects.CR.Contact).IsAssignableFrom(typeof (TPrimaryView)))
    {
      type2 = typeof (CRPMTimeActivity.contactID);
      type1 = ((PXSelectBase) this).View.Graph.Caches[typeof (TPrimaryView)].GetBqlField(typeof (PX.Objects.CR.Contact.contactID).Name);
    }
    else
      type2 = typeof (CRPMTimeActivity.refNoteID);
    BqlCommand bqlCommand = this.OriginalCommand.WhereAnd(BqlCommand.Compose(new System.Type[5]
    {
      typeof (Where<,>),
      type2,
      typeof (Equal<>),
      typeof (Current<>),
      type1
    }));
    if ((object) handler == null)
      ((PXSelectBase) this).View = new PXView(((PXSelectBase) this).View.Graph, ((PXSelectBase) this).View.IsReadOnly, bqlCommand);
    else
      ((PXSelectBase) this).View = new PXView(((PXSelectBase) this).View.Graph, ((PXSelectBase) this).View.IsReadOnly, bqlCommand, handler);
  }

  protected virtual PXEntryStatus[] NotEditableStatuses
  {
    get
    {
      return new PXEntryStatus[2]
      {
        (PXEntryStatus) 2,
        (PXEntryStatus) 4
      };
    }
  }

  protected BqlCommand OriginalCommand => this._originalCommand;

  public delegate string GetEmailHandler()
    where TPrimaryView : class, IBqlTable, new()
    where TActivity : CRPMTimeActivity, new();
}
