// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.ActivityDetailsExt`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Mail;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Objects.CR.Descriptor.Exceptions;
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
using System.Net.Mail;

#nullable enable
namespace PX.Objects.CR.Extensions;

public abstract class ActivityDetailsExt<TGraph, TPrimaryEntity, TActivityEntity, TActivityEntity_NoteID> : 
  PXGraphExtension<
  #nullable disable
  TGraph>,
  IActivityDetailsExt
  where TGraph : PXGraph, new()
  where TPrimaryEntity : class, IBqlTable, new()
  where TActivityEntity : CRPMTimeActivity, new()
  where TActivityEntity_NoteID : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<IBqlGuid>>
{
  private EntityHelper _EntityHelper;
  private NotificationUtility _NotificationUtility;
  private int? _entityDefaultEMailAccountId;
  [PXCopyPasteHiddenView]
  [PXViewName("Activities")]
  [PXFilterable(new System.Type[] {})]
  public FbqlSelect<SelectFromBase<TActivityEntity, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<CRReminder>.On<BqlOperand<
  #nullable enable
  CRReminder.refNoteID, IBqlGuid>.IsEqual<
  #nullable disable
  TActivityEntity_NoteID>>>>, TActivityEntity>.View Activities;
  [PXHidden]
  public PXSelect<PX.Objects.CT.Contract> ContractsDummy;
  public PXAction<TPrimaryEntity> NewMailActivity;
  public PXAction<TPrimaryEntity> ViewActivity;
  public PXAction<TPrimaryEntity> OpenActivityOwner;
  public PXAction<TPrimaryEntity> RefreshActivities;

  [InjectDependency]
  protected IReportLoaderService ReportLoader { get; private set; }

  [InjectDependency]
  protected IReportDataBinder ReportDataBinder { get; private set; }

  [InjectDependency]
  protected IActivityService ActivityService { get; private set; }

  [InjectDependency]
  internal ICurrentUserInformationProvider CurrentUserInformationProvider { get; private set; }

  public EntityHelper EntityHelper
  {
    get
    {
      if (this._EntityHelper == null)
        this._EntityHelper = new EntityHelper((PXGraph) this.Base);
      return this._EntityHelper;
    }
  }

  public NotificationUtility NotificationUtility
  {
    get
    {
      if (this._NotificationUtility == null)
        this._NotificationUtility = new NotificationUtility((PXGraph) this.Base);
      return this._NotificationUtility;
    }
  }

  public int? DefaultEmailAccountID
  {
    get
    {
      int? emailAccountId = (int?) MailAccountManager.GetUserSettingsEmailAccount(this.CurrentUserInformationProvider.GetUserId(), true)?.EmailAccountID;
      if (emailAccountId.HasValue)
        return emailAccountId;
      int? defaultEmailAccountId = this._entityDefaultEMailAccountId;
      if (defaultEmailAccountId.HasValue)
        return defaultEmailAccountId;
      return MailAccountManager.GetSystemSettingsEmailAccount(this.CurrentUserInformationProvider.GetUserId(), true)?.EmailAccountID;
    }
    set => this._entityDefaultEMailAccountId = value;
  }

  public string DefaultActivityType
  {
    get
    {
      EPSetup epSetup = PXResultset<EPSetup>.op_Implicit(PXSetup<EPSetup>.Select((PXGraph) this.Base, Array.Empty<object>()));
      return epSetup != null && !string.IsNullOrEmpty(epSetup.DefaultActivityType) ? epSetup.DefaultActivityType : (string) null;
    }
  }

  public string DefaultSubject { get; set; }

  System.Type IActivityDetailsExt.GetActivityType() => typeof (TActivityEntity);

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.AdjustActivitiesView();
    this.AttachPreview();
    this.AttachEvents();
  }

  public PXView ActivitiesView => ((PXSelectBase) this.Activities).View;

  [PXUIField(DisplayName = "Create Email")]
  [PXButton(DisplayOnMainToolbar = false, PopupCommand = "RefreshActivities")]
  public virtual IEnumerable newMailActivity(PXAdapter adapter)
  {
    this.CreateNewActivityAndRedirect(4, (string) null);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(PopupCommand = "RefreshActivities", DisplayOnMainToolbar = false)]
  public virtual IEnumerable viewActivity(PXAdapter adapter)
  {
    this.NavigateToActivity(((PXSelectBase<TActivityEntity>) this.Activities).Current);
    return adapter.Get();
  }

  [PXButton(DisplayOnMainToolbar = false)]
  [PXUIField(Visible = false)]
  public virtual IEnumerable openActivityOwner(PXAdapter adapter)
  {
    CRActivity current = (CRActivity) ((PXSelectBase<TActivityEntity>) this.Activities).Current;
    if (current != null)
    {
      PX.Objects.EP.EPEmployee epEmployee = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelectReadonly<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Required<CRActivity.ownerID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) current.OwnerID
      }));
      if (epEmployee != null)
        PXRedirectHelper.TryRedirect(this.Base.Caches[typeof (PX.Objects.EP.EPEmployee)], (object) epEmployee, string.Empty, (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual void refreshActivities()
  {
    if (this.Base.IsDirty)
      return;
    this.Base.Actions.PressCancel();
  }

  protected virtual void _(PX.Data.Events.RowSelected<TActivityEntity> e)
  {
    TActivityEntity row = e.Row;
    if ((object) row == null)
      return;
    int? nullable = row.ClassID;
    int num1 = 0;
    if (!(nullable.GetValueOrDefault() == num1 & nullable.HasValue))
    {
      nullable = row.ClassID;
      if (nullable.GetValueOrDefault() != 1)
        return;
    }
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    int num5 = 0;
    using (new PXConnectionScope())
    {
      foreach (PXResult<CRChildActivity, PMTimeActivity> pxResult in PXSelectBase<CRChildActivity, PXSelectJoin<CRChildActivity, InnerJoin<PMTimeActivity, On<PMTimeActivity.refNoteID, Equal<CRChildActivity.noteID>>>, Where<CRChildActivity.parentNoteID, Equal<Required<CRChildActivity.parentNoteID>>, And<Where<PMTimeActivity.isCorrected, NotEqual<True>, Or<PMTimeActivity.isCorrected, IsNull>>>>>.Config>.Select((PXGraph) this.Base, new object[1]
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
    }
    row.TimeSpent = new int?(num2);
    row.OvertimeSpent = new int?(num3);
    row.TimeBillable = new int?(num4);
    row.OvertimeBillable = new int?(num5);
    PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<TActivityEntity>>) e).Cache, (object) e.Row).For<CRActivity.providesCaseSolution>((Action<PXUIFieldAttribute>) (ui => ui.Visible = false));
  }

  protected virtual void _(PX.Data.Events.RowInserting<CRSMEmail> e)
  {
    if (e.Row == null)
      return;
    this.InitializeEmail(e.Row);
  }

  protected virtual void ActivityRowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is CRActivity row))
      return;
    this.InitializeActivity(row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<TActivityEntity> e)
  {
    if ((e.Operation & 3) == 3)
      e.Cancel = true;
    if ((object) e.Row == null || e.Row.ChildKey.HasValue)
      return;
    System.Type[] tables = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<TActivityEntity>>) e).Cache.BqlSelect.GetTables();
    foreach (string field in (List<string>) this.Base.Caches[tables.Length > 1 ? tables[1] : typeof (CRActivity)].Fields)
      PXDefaultAttribute.SetPersistingCheck(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<TActivityEntity>>) e).Cache, field, (object) e.Row, (PXPersistingCheck) 2);
    ((PXProjectionAttribute) ((object) e.Row).GetType().GetCustomAttributes(typeof (PXProjectionAttribute), true)[0]).Persistent = false;
  }

  protected virtual void _(PX.Data.Events.RowPersisted<TActivityEntity> e)
  {
    if ((object) e.Row == null)
      return;
    ((PXProjectionAttribute) ((object) e.Row).GetType().GetCustomAttributes(typeof (PXProjectionAttribute), true)[0]).Persistent = true;
  }

  protected virtual void BAccountIDFieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    System.Type baccountIdCommand = this.GetBAccountIDCommand();
    if ((object) baccountIdCommand == null)
      return;
    e.NewValue = (object) this.GetIDByReference(baccountIdCommand);
  }

  protected virtual void ContactIDFieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    System.Type contactIdCommand = this.GetContactIDCommand();
    if ((object) contactIdCommand == null)
      return;
    e.NewValue = (object) this.GetIDByReference(contactIdCommand);
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<TActivityEntity, CRActivity.body> e)
  {
    if ((object) e.Row == null)
      return;
    (bool flag, System.Type graphType) = this.CheckAccessRightsOfTargetGraph(e.Row.ClassID, e.Row.Type);
    if (!flag && !this.Base.UnattendedMode)
    {
      ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<TActivityEntity, CRActivity.body>>) e).ReturnValue = (object) PX.Objects.CR.Messages.FormNoAccessRightsMessage(graphType);
    }
    else
    {
      if (e.Row.ClassID.GetValueOrDefault() != 4)
        return;
      SMEmailBody smEmailBody = PXResultset<SMEmailBody>.op_Implicit(PXSelectBase<SMEmailBody, PXSelect<SMEmailBody, Where<SMEmailBody.refNoteID, Equal<Required<CRActivity.noteID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
      {
        (object) e.Row.NoteID
      }));
      if (smEmailBody == null)
        return;
      ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<TActivityEntity, CRActivity.body>>) e).ReturnValue = (object) smEmailBody.Body;
      UploadFileMaintenance.OverrideAccessRights(PXContext.Session, e.Row.NoteID);
    }
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Enabled", false)]
  protected virtual void _(PX.Data.Events.CacheAttached<CRActivity.body> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Created On")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CRPMTimeActivity.createdDateTime> e)
  {
  }

  public virtual void AdjustActivitiesView()
  {
    System.Type linkConditionClause = this.GetLinkConditionClause();
    if ((object) linkConditionClause != null)
      ((PXSelectBase) this.Activities).View.WhereAnd(linkConditionClause);
    System.Type classConditionClause = this.GetClassConditionClause();
    if ((object) classConditionClause != null)
      ((PXSelectBase) this.Activities).View.WhereAnd(classConditionClause);
    System.Type privateConditionClause = this.GetPrivateConditionClause();
    if ((object) privateConditionClause != null)
      ((PXSelectBase) this.Activities).View.WhereAnd(privateConditionClause);
    System.Type orderByClause = this.GetOrderByClause();
    if ((object) orderByClause == null)
      return;
    ((PXSelectBase) this.Activities).View.OrderByNew(orderByClause);
  }

  public abstract System.Type GetLinkConditionClause();

  public abstract System.Type GetClassConditionClause();

  public abstract System.Type GetPrivateConditionClause();

  public abstract System.Type GetOrderByClause();

  public virtual void AttachEvents()
  {
    foreach (System.Type allActivityType in (IEnumerable<System.Type>) this.GetAllActivityTypes())
    {
      PXGraph.FieldDefaultingEvents fieldDefaulting1 = this.Base.FieldDefaulting;
      System.Type type1 = allActivityType;
      ActivityDetailsExt<TGraph, TPrimaryEntity, TActivityEntity, TActivityEntity_NoteID> activityDetailsExt1 = this;
      // ISSUE: virtual method pointer
      PXFieldDefaulting pxFieldDefaulting1 = new PXFieldDefaulting((object) activityDetailsExt1, __vmethodptr(activityDetailsExt1, BAccountIDFieldDefaulting));
      fieldDefaulting1.AddHandler(type1, "BAccountID", pxFieldDefaulting1);
      PXGraph.FieldDefaultingEvents fieldDefaulting2 = this.Base.FieldDefaulting;
      System.Type type2 = allActivityType;
      ActivityDetailsExt<TGraph, TPrimaryEntity, TActivityEntity, TActivityEntity_NoteID> activityDetailsExt2 = this;
      // ISSUE: virtual method pointer
      PXFieldDefaulting pxFieldDefaulting2 = new PXFieldDefaulting((object) activityDetailsExt2, __vmethodptr(activityDetailsExt2, ContactIDFieldDefaulting));
      fieldDefaulting2.AddHandler(type2, "ContactID", pxFieldDefaulting2);
      PXGraph.RowInsertingEvents rowInserting = this.Base.RowInserting;
      System.Type type3 = allActivityType;
      ActivityDetailsExt<TGraph, TPrimaryEntity, TActivityEntity, TActivityEntity_NoteID> activityDetailsExt3 = this;
      // ISSUE: virtual method pointer
      PXRowInserting pxRowInserting = new PXRowInserting((object) activityDetailsExt3, __vmethodptr(activityDetailsExt3, ActivityRowInserting));
      rowInserting.AddHandler(type3, pxRowInserting);
    }
  }

  public virtual IList<System.Type> GetAllActivityTypes()
  {
    return (IList<System.Type>) new System.Type[3]
    {
      typeof (CRActivity),
      typeof (CRSMEmail),
      typeof (TActivityEntity)
    };
  }

  public virtual void AttachPreview()
  {
    new CRPreviewAttribute(typeof (TPrimaryEntity), typeof (TActivityEntity)).Attach((PXGraph) this.Base, "Activities", (CRPreviewAttribute.GeneratePreview) null);
  }

  public virtual void NavigateToActivity(TActivityEntity row)
  {
    object obj1 = (object) row;
    System.Type primaryGraphType = this.EntityHelper.GetPrimaryGraphType(ref obj1, false);
    if (primaryGraphType == (System.Type) null || !PXAccess.VerifyRights(primaryGraphType))
    {
      this.Base.Views[this.Base.PrimaryView].Ask((object) null, "Access denied", PX.Objects.CR.Messages.FormNoAccessRightsMessage(primaryGraphType), (MessageButtons) 0, (MessageIcon) 1);
    }
    else
    {
      PXGraph instance = PXGraph.CreateInstance(primaryGraphType);
      if (row.TimeActivityNoteID.HasValue && row.TimeActivityNoteID.Equals((object) row.TimeActivityRefNoteID))
      {
        PMTimeActivity pmTimeActivity = PMTimeActivity.PK.Find((PXGraph) this.Base, row.TimeActivityNoteID);
        PXCache cach1 = instance.Caches[typeof (CRActivity)];
        PXCache cach2 = instance.Caches[typeof (PMTimeActivity)];
        object obj2 = GraphHelper.NonDirtyInsert(cach1);
        cach1.SetValue(obj2, "noteID", cach2.GetValue((object) pmTimeActivity, "refNoteID"));
        cach1.SetValue(obj2, "type", (object) this.DefaultActivityType);
        cach1.SetValue(obj2, "subject", cach2.GetValue((object) pmTimeActivity, "summary"));
        cach1.SetValue(obj2, "ownerID", cach2.GetValue((object) pmTimeActivity, "ownerID"));
        cach1.SetValue(obj2, "startDate", cach2.GetValue((object) pmTimeActivity, "date"));
        cach1.SetValue(obj2, "uistatus", (object) "CD");
        cach1.Normalize();
        cach2.SetValue((object) pmTimeActivity, "summary", cach1.GetValue(obj2, "subject"));
        cach2.Current = (object) pmTimeActivity;
        GraphHelper.MarkUpdated(cach2, cach2.Current);
        if ((!string.IsNullOrEmpty(pmTimeActivity?.TimeCardCD) ? 1 : (pmTimeActivity != null ? (pmTimeActivity.Billed.GetValueOrDefault() ? 1 : 0) : 0)) != 0)
          cach1.IsDirty = false;
        ((PXSelectBase) this.Activities).Cache.Clear();
        PXRedirectHelper.TryRedirect(instance, (PXRedirectHelper.WindowMode) 3);
      }
      else
      {
        GraphHelper.GetPrimaryCache(instance).Current = obj1;
        ((PXSelectBase) this.Activities).Cache.Clear();
        PXRedirectHelper.TryRedirect(instance, (PXRedirectHelper.WindowMode) 3);
      }
    }
  }

  public virtual Guid? EnsureNoteID(object row)
  {
    if (row == null)
      return new Guid?();
    System.Type type = row.GetType();
    string noteField = EntityHelper.GetNoteField(type);
    return PXNoteAttribute.GetNoteID(this.Base.Caches[type], row, noteField);
  }

  public virtual (bool, System.Type) CheckAccessRightsOfTargetGraph(
    int? classID,
    string activityType)
  {
    object obj = (object) new CRActivity()
    {
      ClassID = classID,
      Type = activityType
    };
    System.Type primaryGraphType = this.EntityHelper.GetPrimaryGraphType(ref obj, false);
    return (PXAccess.VerifyRights(primaryGraphType), primaryGraphType);
  }

  public virtual void CreateNewActivityAndRedirect(int classID, string activityType)
  {
    PXGraph newActivity = this.CreateNewActivity(classID, activityType);
    if (newActivity != null)
    {
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException(newActivity, string.Empty, true);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  public virtual PXGraph CreateNewActivity(int classID, string activityType)
  {
    (bool flag, System.Type graphType) = this.CheckAccessRightsOfTargetGraph(new int?(classID), activityType);
    if (!flag)
    {
      this.Base.Views[this.Base.PrimaryView].Ask((object) null, "Access denied", PX.Objects.CR.Messages.FormNoAccessRightsMessage(graphType), (MessageButtons) 0, (MessageIcon) 1);
      return (PXGraph) null;
    }
    PXGraph instance = PXGraph.CreateInstance(graphType);
    this.CreatePrimaryActivity(instance, classID, activityType);
    this.CreateTimeActivity(instance, classID, activityType);
    foreach (PXCache pxCache in ((IEnumerable<PXCache>) instance.Caches.Caches).Where<PXCache>((Func<PXCache, bool>) (c => c.IsDirty)))
      pxCache.IsDirty = false;
    return instance;
  }

  public virtual void CreatePrimaryActivity(PXGraph targetGraph, int classID, string activityType)
  {
    if (!(this.Base.Caches[targetGraph.PrimaryItemType].CreateInstance() is CRActivity instance))
      return;
    instance.ClassID = new int?(classID);
    instance.Type = activityType;
    CRActivity activity = this.InitNewRow<CRActivity>(this.Base.Caches[targetGraph.PrimaryItemType], instance);
    if (this.Base.IsDirty)
    {
      if (this.Base.IsMobile)
      {
        PXCache cache = this.Base.Views[this.Base.PrimaryView].Cache;
        if (cache.Current != null)
          cache.SetStatus(cache.Current, (PXEntryStatus) 1);
      }
      this.Base.Actions.PressSave();
    }
    PXCache primaryCache = GraphHelper.GetPrimaryCache(targetGraph);
    this.InsertActivityIntoTargetGraph(targetGraph, activity);
    foreach (CRActivity destData in primaryCache.Inserted)
      UDFHelper.CopyAttributes(this.Base.Views[this.Base.PrimaryView].Cache, this.Base.Views[this.Base.PrimaryView].Cache.Current, primaryCache, (object) destData, (string) null);
  }

  public virtual void CreateTimeActivity(PXGraph targetGraph, int classID, string activityType)
  {
  }

  public virtual void InsertActivityIntoTargetGraph(PXGraph targetGraph, CRActivity activity)
  {
    PXCache primaryCache = GraphHelper.GetPrimaryCache(targetGraph);
    Dictionary<string, object> dictionary = primaryCache.ToDictionary((object) activity);
    foreach (string key in dictionary.Keys.ToList<string>())
    {
      if (dictionary[key] == null)
        dictionary[key] = PXCache.NotSetValue;
    }
    primaryCache.Insert((IDictionary) dictionary);
  }

  public virtual TNode InitNewRow<TNode>(PXCache cache, TNode node) where TNode : class, IBqlTable, new()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ActivityDetailsExt<TGraph, TPrimaryEntity, TActivityEntity, TActivityEntity_NoteID>.\u003C\u003Ec__DisplayClass72_0<TNode> cDisplayClass720 = new ActivityDetailsExt<TGraph, TPrimaryEntity, TActivityEntity, TActivityEntity_NoteID>.\u003C\u003Ec__DisplayClass72_0<TNode>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass720.result = default (TNode);
    if ((object) node == null)
    {
      // ISSUE: reference to a compiler-generated field
      return cDisplayClass720.result;
    }
    // ISSUE: method pointer
    cache.Graph.RowInserting.AddHandler(node.GetType(), new PXRowInserting((object) cDisplayClass720, __methodptr(\u003CInitNewRow\u003Eg__RowInserting\u007C0)));
    try
    {
      cache.Insert((object) node);
    }
    finally
    {
      // ISSUE: method pointer
      cache.Graph.RowInserting.RemoveHandler(node.GetType(), new PXRowInserting((object) cDisplayClass720, __methodptr(\u003CInitNewRow\u003Eg__RowInserting\u007C0)));
    }
    // ISSUE: reference to a compiler-generated field
    return cDisplayClass720.result;
  }

  public virtual void InitializeActivity(CRActivity row)
  {
    CRActivity crActivity1 = row;
    int? nullable;
    if (!crActivity1.OwnerID.HasValue)
      crActivity1.OwnerID = nullable = EmployeeMaint.GetCurrentOwnerID((PXGraph) this.Base);
    int? entityWorkgroupId = this.GetPrimaryEntityWorkgroupID();
    if (row.OwnerID.HasValue && OwnerAttribute.BelongsToWorkGroup((PXGraph) this.Base, entityWorkgroupId, row.OwnerID))
    {
      CRActivity crActivity2 = row;
      if (!crActivity2.WorkgroupID.HasValue)
        crActivity2.WorkgroupID = nullable = entityWorkgroupId;
    }
    CRActivity crActivity3 = row;
    if (!crActivity3.RefNoteID.HasValue)
      crActivity3.RefNoteID = this.GetRefNoteID();
    System.Type baccountIdCommand = this.GetBAccountIDCommand();
    if ((object) baccountIdCommand != null)
    {
      CRActivity crActivity4 = row;
      if (!crActivity4.BAccountID.HasValue)
        crActivity4.BAccountID = nullable = this.GetIDByReference(baccountIdCommand);
    }
    System.Type contactIdCommand = this.GetContactIDCommand();
    if ((object) contactIdCommand == null)
      return;
    CRActivity crActivity5 = row;
    if (crActivity5.ContactID.HasValue)
      return;
    crActivity5.ContactID = nullable = this.GetIDByReference(contactIdCommand);
  }

  public virtual int? GetPrimaryEntityWorkgroupID()
  {
    return !(this.Base.Caches[typeof (TPrimaryEntity)].Current is IAssign current) ? new int?() : current.WorkgroupID;
  }

  public virtual System.Type GetContactIDCommand() => (System.Type) null;

  public virtual System.Type GetBAccountIDCommand() => (System.Type) null;

  public virtual Guid? GetRefNoteID()
  {
    PXCache cach = this.Base.Caches[typeof (TPrimaryEntity)];
    object current = cach.Current;
    if (current == null)
      return new Guid?();
    if (current is INotable row)
    {
      this.EnsureNoteID((object) row);
      return row.NoteID;
    }
    if (!(cach.GetValue(current, "NoteID") is Guid guid))
      return new Guid?();
    this.EnsureNoteID(current);
    return new Guid?(guid);
  }

  public virtual int? GetIDByReference(System.Type reference)
  {
    if (typeof (IBqlSelect).IsAssignableFrom(reference))
    {
      PXView pxView = new PXView((PXGraph) this.Base, true, BqlCommand.CreateInstance(new System.Type[1]
      {
        reference
      }));
      object obj = pxView.SelectSingle(Array.Empty<object>());
      return obj == null ? new int?() : this.Base.Caches[obj.GetType()].GetValue(obj, EntityHelper.GetIDField(pxView.Cache)) as int?;
    }
    if (!typeof (IBqlField).IsAssignableFrom(reference))
      return new int?();
    PXCache cach = this.Base.Caches[reference.DeclaringType];
    return cach.Current == null ? new int?() : cach.GetValue(cach.Current, reference.Name) as int?;
  }

  public virtual void InitializeEmail(CRSMEmail row)
  {
    CRSMEmail crsmEmail1 = row;
    if (!crsmEmail1.MailAccountID.HasValue)
    {
      int? defaultEmailAccountId;
      crsmEmail1.MailAccountID = defaultEmailAccountId = this.DefaultEmailAccountID;
    }
    CRSMEmail crsmEmail2 = row;
    string str;
    if (crsmEmail2.MailReply == null)
      crsmEmail2.MailReply = str = this.GetMailReply(row, row.MailReply);
    CRSMEmail crsmEmail3 = row;
    if (crsmEmail3.MailTo == null)
      crsmEmail3.MailTo = str = this.GetMailTo(row);
    if (row.RefNoteID.HasValue)
    {
      CRSMEmail crsmEmail4 = row;
      if (crsmEmail4.MailCc == null)
        crsmEmail4.MailCc = str = this.GetMailCc(row, row.RefNoteID);
    }
    CRSMEmail crsmEmail5 = row;
    if (crsmEmail5.Subject == null)
      crsmEmail5.Subject = str = this.GetSubject(row);
    CRSMEmail crsmEmail6 = row;
    if (crsmEmail6.Body != null)
      return;
    crsmEmail6.Body = str = this.GetBody();
  }

  public virtual string GetMailReply(CRSMEmail message, string currentMailReply)
  {
    Mailbox mailbox = (Mailbox) null;
    bool flag = currentMailReply != null && Mailbox.TryParse(currentMailReply, ref mailbox) && !string.IsNullOrEmpty(mailbox.Address);
    if (flag)
      flag = PXSelectBase<EMailAccount, PXSelect<EMailAccount, Where<EMailAccount.address, Equal<Required<EMailAccount.address>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) mailbox.Address
      }).Count > 0;
    string mailReply = currentMailReply;
    if (!flag)
      mailReply = EMailAccount.PK.Find((PXGraph) this.Base, this.DefaultEmailAccountID, (PKFindOptions) 0)?.Address;
    if (!string.IsNullOrEmpty(mailReply))
      return mailReply;
    EMailAccount emailAccount = PXResultset<EMailAccount>.op_Implicit(PXSelectBase<EMailAccount, PXSelect<EMailAccount>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, Array.Empty<object>()));
    if (emailAccount != null)
      mailReply = emailAccount.Address;
    return mailReply;
  }

  public virtual string GetMailTo(CRSMEmail message)
  {
    string str = this.GetCustomMailTo() ?? this.GetDefaultMailTo();
    return string.IsNullOrWhiteSpace(str) ? (string) null : str.Trim();
  }

  public virtual string GetMailCc(CRSMEmail message, Guid? refNoteId)
  {
    return !refNoteId.HasValue ? (string) null : PXDBEmailAttribute.AppendAddresses(message.MailCc, this.ActivityService?.GetEmailAddressesForCc((PXGraph) this.Base, new Guid?(refNoteId.Value)));
  }

  public virtual string GetSubject(CRSMEmail message)
  {
    return string.IsNullOrEmpty(this.DefaultSubject) ? (string) null : this.DefaultSubject;
  }

  public virtual string GetBody()
  {
    return PXRichTextConverter.NormalizeHtml(MailAccountManager.AppendSignature((string) null, (PXGraph) this.Base, (MailAccountManager.SignatureOptions) 0));
  }

  public virtual string GetDefaultMailTo()
  {
    string defaultMailTo = (string) null;
    System.Type emailMessageTarget1 = this.GetEmailMessageTarget();
    IEmailMessageTarget emailMessageTarget2;
    if ((object) emailMessageTarget1 != null && typeof (IBqlSelect).IsAssignableFrom(emailMessageTarget1))
      emailMessageTarget2 = new PXView((PXGraph) this.Base, true, BqlCommand.CreateInstance(new System.Type[1]
      {
        emailMessageTarget1
      })).SelectSingle(Array.Empty<object>()) as IEmailMessageTarget;
    else
      emailMessageTarget2 = GraphHelper.GetPrimaryCache((PXGraph) this.Base).Current as IEmailMessageTarget;
    if (emailMessageTarget2 == null)
      return (string) null;
    string str1 = emailMessageTarget2.DisplayName?.Trim();
    string str2 = emailMessageTarget2.Address?.Trim();
    if (!string.IsNullOrEmpty(str2))
      defaultMailTo = PXDBEmailAttribute.FormatAddressesWithSingleDisplayName(str2, str1);
    return defaultMailTo;
  }

  public virtual System.Type GetEmailMessageTarget() => (System.Type) null;

  public virtual string GetCustomMailTo() => (string) null;

  public virtual void SendNotification(
    Func<TPrimaryEntity, string> sourceTypeGetter,
    string notifications,
    TPrimaryEntity document,
    Func<TPrimaryEntity, int?> branchIDGetter,
    Func<TPrimaryEntity, IDictionary<string, string>> documentParametersGetter,
    MassEmailingActionParameters emailingParameters,
    IList<Guid?> attachments = null)
  {
    this.SendNotification<TPrimaryEntity>(sourceTypeGetter, notifications, document, branchIDGetter, documentParametersGetter, emailingParameters, attachments);
  }

  public virtual void SendNotification<TTemplateEntityType>(
    Func<TTemplateEntityType, string> sourceTypeGetter,
    string notifications,
    TTemplateEntityType document,
    Func<TTemplateEntityType, int?> branchIDGetter,
    Func<TTemplateEntityType, IDictionary<string, string>> documentParametersGetter,
    MassEmailingActionParameters emailingParameters,
    IList<Guid?> attachments = null)
    where TTemplateEntityType : class, IBqlTable, new()
  {
    this.Base.Caches[typeof (TTemplateEntityType)].Current = (object) document;
    NotificationGenerator notificationProvider = this.CreateNotificationProvider<TTemplateEntityType>(sourceTypeGetter(document), notifications, branchIDGetter(document), documentParametersGetter(document), attachments);
    notificationProvider.MassProcessMode = emailingParameters.MassProcess;
    if (!notificationProvider.Send().Any<CRSMEmail>())
      throw new PXException("Email send failed. Email isn't created or email recipient list is empty.");
  }

  public virtual void SendNotifications(
    Func<TPrimaryEntity, string> sourceTypeGetter,
    string notifications,
    IList<TPrimaryEntity> documents,
    Func<TPrimaryEntity, int?> branchIDGetter,
    Func<TPrimaryEntity, IDictionary<string, string>> documentParametersGetter,
    MassEmailingActionParameters emailingParameters,
    Func<TPrimaryEntity, object> groupingQualifier = null,
    IList<Guid?> attachments = null)
  {
    this.SendNotifications<TPrimaryEntity>(sourceTypeGetter, notifications, documents, branchIDGetter, documentParametersGetter, emailingParameters, groupingQualifier, attachments);
  }

  public virtual void SendNotifications<TTemplateEntityType>(
    Func<TTemplateEntityType, string> sourceTypeGetter,
    string notifications,
    IList<TTemplateEntityType> documents,
    Func<TTemplateEntityType, int?> branchIDGetter,
    Func<TTemplateEntityType, IDictionary<string, string>> documentParametersGetter,
    MassEmailingActionParameters emailingParameters,
    Func<TTemplateEntityType, object> groupingQualifier = null,
    IList<Guid?> attachments = null)
    where TTemplateEntityType : class, IBqlTable, new()
  {
    bool? aggregateEmails = emailingParameters.AggregateEmails;
    if (aggregateEmails.HasValue && aggregateEmails.GetValueOrDefault())
    {
      if (groupingQualifier != null)
      {
        foreach (IGrouping<object, TTemplateEntityType> source in documents.GroupBy<TTemplateEntityType, object>(groupingQualifier))
          this.SendAggregatedNotifications<TTemplateEntityType>(sourceTypeGetter, notifications, documents, (IList<TTemplateEntityType>) source.ToList<TTemplateEntityType>(), branchIDGetter, documentParametersGetter, emailingParameters, attachments);
      }
      else
        this.SendAggregatedNotifications<TTemplateEntityType>(sourceTypeGetter, notifications, documents, documents, branchIDGetter, documentParametersGetter, emailingParameters, attachments);
    }
    else
      this.SendSeparateNotifications<TTemplateEntityType>(sourceTypeGetter, notifications, documents, branchIDGetter, documentParametersGetter, emailingParameters, attachments);
  }

  public virtual void SendSeparateNotifications<TTemplateEntityType>(
    Func<TTemplateEntityType, string> sourceTypeGetter,
    string notifications,
    IList<TTemplateEntityType> documents,
    Func<TTemplateEntityType, int?> branchIDGetter,
    Func<TTemplateEntityType, IDictionary<string, string>> documentParametersGetter,
    MassEmailingActionParameters emailingParameters,
    IList<Guid?> attachments = null)
    where TTemplateEntityType : class, IBqlTable, new()
  {
    bool flag = false;
    int num = 0;
    foreach (TTemplateEntityType document in (IEnumerable<TTemplateEntityType>) documents)
    {
      try
      {
        if (emailingParameters.MassProcess)
          num = documents.IndexOf(document);
        this.SendNotification<TTemplateEntityType>(sourceTypeGetter, notifications, document, branchIDGetter, documentParametersGetter, emailingParameters, attachments);
        if (emailingParameters.MassProcess)
          PXProcessing.SetInfo(num, "The record has been processed successfully.");
      }
      catch (Exception ex) when (emailingParameters.MassProcess)
      {
        PXProcessing.SetError(num, ex);
        flag = true;
      }
    }
    if (flag)
      throw new PXOperationCompletedWithErrorException("At least one item has not been processed.");
  }

  public virtual void SendAggregatedNotifications<TTemplateEntityType>(
    Func<TTemplateEntityType, string> sourceTypeGetter,
    string notifications,
    IList<TTemplateEntityType> allDocuments,
    IList<TTemplateEntityType> groupedDocuments,
    Func<TTemplateEntityType, int?> branchIDGetter,
    Func<TTemplateEntityType, IDictionary<string, string>> documentParametersGetter,
    MassEmailingActionParameters emailingParameters,
    IList<Guid?> attachments = null)
    where TTemplateEntityType : class, IBqlTable, new()
  {
    bool flag1 = groupedDocuments.Count<TTemplateEntityType>() > 1;
    string notifications1 = string.Join(",", (IEnumerable<string>) ((IEnumerable<string>) notifications.Split(',')).Where<string>((Func<string, bool>) (cd => !string.IsNullOrEmpty(cd))).Select<string, string>((Func<string, string>) (n => n.Trim() + " MULTIPLE")).ToList<string>());
    bool flag2 = false;
    List<EmailExtracted> source1 = new List<EmailExtracted>();
    NotificationGenerator notificationGenerator = (NotificationGenerator) null;
    int num = 0;
    foreach (TTemplateEntityType groupedDocument in (IEnumerable<TTemplateEntityType>) groupedDocuments)
    {
      try
      {
        if (emailingParameters.MassProcess)
          num = allDocuments.IndexOf(groupedDocument);
        string sourceType = sourceTypeGetter(groupedDocument);
        int? branchID = branchIDGetter(groupedDocument);
        IDictionary<string, string> parameters = documentParametersGetter(groupedDocument);
        this.Base.Caches[typeof (TTemplateEntityType)].Current = (object) groupedDocument;
        notificationGenerator = this.CreateNotificationProvider<TTemplateEntityType>(sourceType, notifications1, branchID, parameters, attachments);
        if (flag1)
          notificationGenerator.RefNoteID = new Guid?();
        notificationGenerator.MassProcessMode = false;
        CRSMEmail crsmEmail = notificationGenerator.MailMessages().FirstOrDefault<CRSMEmail>();
        notificationGenerator.MassProcessMode = emailingParameters.MassProcess;
        if (crsmEmail != null)
        {
          source1.Add(new EmailExtracted()
          {
            Email = crsmEmail,
            MailTo = EmailParser.ParseAddresses(crsmEmail.MailTo),
            MailCc = EmailParser.ParseAddresses(crsmEmail.MailCc),
            MailBcc = EmailParser.ParseAddresses(crsmEmail.MailBcc),
            Attachments = notificationGenerator.GetAttachments(),
            AttachmentLinks = notificationGenerator.GetAttachmentLinks()
          });
          if (emailingParameters.MassProcess)
            PXProcessing.SetInfo(num, "The record has been processed successfully.");
        }
      }
      catch (Exception ex) when (emailingParameters.MassProcess)
      {
        PXProcessing.SetError(num, ex);
        flag2 = true;
      }
    }
    if (notificationGenerator == null)
      return;
    List<CRSMEmail> messages = new List<CRSMEmail>();
    try
    {
      foreach (IEnumerable<EmailExtracted> source2 in source1.GroupBy<EmailExtracted, string>(new Func<EmailExtracted, string>(this.GetRecipientKey)))
      {
        List<EmailExtracted> list = source2.ToList<EmailExtracted>();
        for (int index = 0; index < list.Count; index += 100)
        {
          EmailExtracted groupedEmail = this.MergeEmails((IList<EmailExtracted>) list.GetRange(index, Math.Min(100, list.Count - index)));
          bool? aggregateAttachments = emailingParameters.AggregateAttachments;
          if (aggregateAttachments.HasValue && aggregateAttachments.GetValueOrDefault())
            groupedEmail = this.MergeAttachments(groupedEmail, emailingParameters.AggregatedAttachmentFileName ?? this.GetSingleEmailCombinedFileName());
          notificationGenerator.SetAttachments(groupedEmail.Attachments);
          notificationGenerator.SetAttachmentLinks(groupedEmail.AttachmentLinks);
          if (groupedEmail != null)
          {
            messages.Add(groupedEmail.Email);
            notificationGenerator.Send((IEnumerable<CRSMEmail>) messages);
          }
        }
      }
    }
    catch (Exception ex) when (emailingParameters.MassProcess)
    {
      foreach (TTemplateEntityType groupedDocument in (IEnumerable<TTemplateEntityType>) groupedDocuments)
        PXProcessing.SetError(allDocuments.IndexOf(groupedDocument), ex);
      flag2 = true;
    }
    if (flag2 && !emailingParameters.MassProcess)
      throw new PXOperationCompletedWithErrorException("At least one item has not been processed.");
  }

  public virtual void SendNotification(
    string sourceType,
    string notifications,
    int? branchID,
    IDictionary<string, string> parameters,
    bool massProcess = false,
    IList<Guid?> attachments = null)
  {
    MassEmailingActionParameters emailingParameters = new MassEmailingActionParameters()
    {
      MassProcess = massProcess
    };
    this.SendNotification<TPrimaryEntity>((Func<TPrimaryEntity, string>) (_ => sourceType), notifications, GraphHelper.GetPrimaryCache((PXGraph) this.Base).Current as TPrimaryEntity, (Func<TPrimaryEntity, int?>) (_ => branchID), (Func<TPrimaryEntity, IDictionary<string, string>>) (_ => parameters), emailingParameters, attachments);
  }

  public virtual void SendNotification<TTemplateEntityType>(
    string sourceType,
    string notifications,
    int? branchID,
    IDictionary<string, string> parameters,
    bool massProcess = false,
    IList<Guid?> attachments = null)
    where TTemplateEntityType : class, IBqlTable, new()
  {
    MassEmailingActionParameters emailingParameters = new MassEmailingActionParameters()
    {
      MassProcess = massProcess
    };
    this.SendNotification<TTemplateEntityType>((Func<TTemplateEntityType, string>) (_ => sourceType), notifications, this.Base.Caches[typeof (TTemplateEntityType)].Current as TTemplateEntityType, (Func<TTemplateEntityType, int?>) (_ => branchID), (Func<TTemplateEntityType, IDictionary<string, string>>) (_ => parameters), emailingParameters, attachments);
  }

  public virtual NotificationGenerator CreateNotificationProvider(
    string sourceType,
    string notifications,
    int? branchID,
    IDictionary<string, string> parameters,
    IList<Guid?> attachments = null)
  {
    return this.CreateNotificationProvider<TPrimaryEntity>(sourceType, notifications, branchID, parameters, attachments);
  }

  public virtual NotificationGenerator CreateNotificationProvider<TTemplateEntityType>(
    string sourceType,
    string notifications,
    int? branchID,
    IDictionary<string, string> parameters,
    IList<Guid?> attachments = null)
  {
    if (notifications == null)
      return (NotificationGenerator) null;
    IList<string> list = (IList<string>) ((IEnumerable<string>) notifications.Split(',')).Select<string, string>((Func<string, string>) (n => n?.Trim())).Where<string>((Func<string, bool>) (cd => !string.IsNullOrEmpty(cd))).ToList<string>();
    return this.CreateNotificationProvider<TTemplateEntityType>(sourceType, list, branchID, parameters, attachments);
  }

  public virtual NotificationGenerator CreateNotificationProvider(
    string sourceType,
    IList<string> notificationCDs,
    int? branchID,
    IDictionary<string, string> parameters,
    IList<Guid?> attachments = null)
  {
    return this.CreateNotificationProvider<TPrimaryEntity>(sourceType, notificationCDs, branchID, parameters, attachments);
  }

  public virtual NotificationGenerator CreateNotificationProvider<TTemplateEntityType>(
    string sourceType,
    IList<string> notificationCDs,
    int? branchID,
    IDictionary<string, string> parameters,
    IList<Guid?> attachments = null)
  {
    PXCache cach1 = this.Base.Caches[typeof (TTemplateEntityType)];
    if (cach1.Current == null)
      throw new PXException("Email send failed. Source notification object not defined to proceed operation.");
    List<(NotificationSetup SetupWithBranch, NotificationSetup SetupWithoutBranch)> setupNotifications = this.GetSetupNotifications(sourceType, notificationCDs, branchID);
    PXCache cach2 = this.Base.Caches[typeof (TTemplateEntityType)];
    TTemplateEntityType current = (TTemplateEntityType) cach2.Current;
    object instance = cach2.CreateInstance();
    cach2.RestoreCopy(instance, (object) current);
    TTemplateEntityType row1 = (TTemplateEntityType) instance;
    TActivityEntity activity = GraphHelper.InitNewRow<TActivityEntity>((PXCache<TActivityEntity>) this.Base.Caches[typeof (TActivityEntity)], default (TActivityEntity));
    object baccountRow = this.GetBAccountRow(sourceType, activity);
    RecipientList recipientList = (RecipientList) null;
    TemplateNotificationGenerator notificationProvider = (TemplateNotificationGenerator) null;
    for (int index = 0; index < setupNotifications.Count; ++index)
    {
      (NotificationSetup SetupWithBranch, NotificationSetup SetupWithoutBranch) tuple = setupNotifications[index];
      int? nullable1;
      NotificationSource source1;
      if (baccountRow == null)
      {
        source1 = this.NotificationUtility.GetSource(tuple.SetupWithBranch ?? tuple.SetupWithoutBranch);
      }
      else
      {
        NotificationUtility notificationUtility = this.NotificationUtility;
        string sourceType1 = sourceType;
        object row2 = baccountRow;
        Guid?[] setupIDs = new Guid?[2]
        {
          (Guid?) tuple.SetupWithBranch?.SetupID,
          (Guid?) tuple.SetupWithoutBranch?.SetupID
        };
        nullable1 = branchID;
        int? branchID1 = nullable1 ?? this.Base.Accessinfo.BranchID;
        source1 = notificationUtility.GetSource(sourceType1, row2, (IList<Guid?>) setupIDs, branchID1);
      }
      NotificationSource source2 = source1;
      if (source2 == null && sourceType == "Project")
      {
        NotificationUtility notificationUtility = this.NotificationUtility;
        string sourceType2 = sourceType;
        // ISSUE: variable of a boxed type
        __Boxed<TTemplateEntityType> row3 = (object) row1;
        Guid?[] setupIDs = new Guid?[2]
        {
          (Guid?) tuple.SetupWithBranch?.SetupID,
          (Guid?) tuple.SetupWithoutBranch?.SetupID
        };
        nullable1 = branchID;
        int? branchID2 = nullable1 ?? this.Base.Accessinfo.BranchID;
        source2 = notificationUtility.GetSource(sourceType2, (object) row3, (IList<Guid?>) setupIDs, branchID2);
      }
      if (source2 == null)
        throw new PXException("There is no active notification source to process the operation.");
      if (notificationProvider == null)
      {
        Notification notification = Notification.PK.Find((PXGraph) this.Base, source2.NotificationID, (PKFindOptions) 0);
        int? nullable2;
        if (notification == null)
        {
          nullable1 = new int?();
          nullable2 = nullable1;
        }
        else
          nullable2 = notification.NFrom;
        int? nullable3 = nullable2;
        nullable1 = source2.EMailAccountID;
        int? nullable4;
        if (!nullable1.HasValue)
        {
          NotificationSetup setupWithBranch = tuple.SetupWithBranch;
          int? nullable5;
          int? nullable6;
          if (setupWithBranch == null)
          {
            nullable5 = new int?();
            nullable6 = nullable5;
          }
          else
            nullable6 = setupWithBranch.EMailAccountID;
          int? nullable7 = nullable6;
          if (!nullable7.HasValue)
          {
            NotificationSetup setupWithoutBranch = tuple.SetupWithoutBranch;
            int? nullable8;
            int? nullable9;
            if (setupWithoutBranch == null)
            {
              nullable8 = new int?();
              nullable9 = nullable8;
            }
            else
              nullable9 = setupWithoutBranch.EMailAccountID;
            nullable5 = nullable9;
            if (!nullable5.HasValue)
            {
              nullable8 = nullable3;
              nullable4 = nullable8 ?? this.DefaultEmailAccountID;
            }
            else
              nullable4 = nullable5;
          }
          else
            nullable4 = nullable7;
        }
        else
          nullable4 = nullable1;
        int? nullable10 = nullable4;
        if (!nullable10.HasValue)
          throw new PXException("The email account is empty. Please define the Default Email Account in Email Preferences.");
        if (recipientList == null)
          recipientList = this.NotificationUtility.GetRecipients(sourceType, baccountRow, source2);
        notificationProvider = TemplateNotificationGenerator.Create((object) row1, source2.NotificationID);
        notificationProvider.MailAccountId = nullable10;
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
        this.ReportLoader.InitDefaultReportParameters(report, parameters ?? this.GetKeyParameters(cach1));
        report.MailSettings.Format = ReportNotificationGenerator.ConvertFormat(source2.Format);
        ReportNode reportNode = this.ReportDataBinder.ProcessReportDataBinding(report);
        reportNode.SendMailMode = true;
        Message message = reportNode.Groups.Select<GroupNode, MailSettings>((Func<GroupNode, MailSettings>) (g => g.MailSettings)).Where<MailSettings>((Func<MailSettings, bool>) (msg => msg != null && msg.ShouldSerialize())).Select<MailSettings, Message>((Func<MailSettings, Message>) (msg => new Message(MailSettings.op_Implicit(msg), reportNode, MailSettings.op_Implicit(msg)))).FirstOrDefault<Message>();
        if (message == null)
        {
          if (index == 0)
            throw new EmailFromReportCannotBeCreatedException(PXMessages.LocalizeFormatNoPrefixNLA("Email cannot be created for the specified report '{0}' because the report has not been generated or the email settings are not specified.", new object[1]
            {
              (object) source2.ReportID
            }), source2.ReportID);
          continue;
        }
        if (index == 0)
        {
          bool flag = false;
          if (notificationProvider.Body == null)
          {
            string text = ((MailSender.MailMessageT) message).Content.Body;
            flag = text == null;
            if (flag || !this.IsHtml(text))
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
            NotificationGenerator notification = TemplateNotificationGenerator.Create((object) row1, ((GroupMessage) message).TemplateID).ParseNotification();
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
      }
      if (attachments != null)
      {
        foreach (Guid? attachment in (IEnumerable<Guid?>) attachments)
        {
          if (attachment.HasValue)
            notificationProvider.AddAttachmentLink(attachment.Value);
        }
      }
      string recipientFromContext = this.GetPrimaryRecipientFromContext(this.NotificationUtility, sourceType, baccountRow, source2);
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

  public virtual string GetPrimaryRecipientFromContext(
    NotificationUtility utility,
    string type,
    object row,
    NotificationSource source)
  {
    return (string) null;
  }

  public virtual object GetBAccountRow(string sourceType, TActivityEntity activity)
  {
    return this.EntityHelper.GetEntityRowByID(typeof (BAccountR), activity.BAccountID) ?? this.EntityHelper.GetEntityRow(typeof (BAccountR), activity.RefNoteID);
  }

  public virtual IDictionary<string, string> GetKeyParameters(PXCache sourceCache)
  {
    IDictionary<string, string> keyParameters = (IDictionary<string, string>) new Dictionary<string, string>();
    foreach (string key in (IEnumerable<string>) sourceCache.Keys)
    {
      object valueExt = sourceCache.GetValueExt(sourceCache.Current, key);
      keyParameters[key] = valueExt?.ToString();
    }
    return keyParameters;
  }

  public virtual List<(NotificationSetup SetupWithBranch, NotificationSetup SetupWithoutBranch)> GetSetupNotifications(
    string sourceType,
    IList<string> notificationCDs,
    int? branchID)
  {
    List<(NotificationSetup, NotificationSetup)> setupNotifications = new List<(NotificationSetup, NotificationSetup)>();
    for (int index = 0; index < notificationCDs.Count; ++index)
    {
      (NotificationSetup SetupWithBranch, NotificationSetup SetupWithoutBranch) tuple = this.NotificationUtility.SearchSetup(sourceType, notificationCDs[index], branchID);
      (NotificationSetup SetupWithBranch, NotificationSetup SetupWithoutBranch) = tuple;
      if (SetupWithBranch == null && SetupWithoutBranch == null)
        throw new PXException("Email send failed. Notification Settings '{0}' not found.", new object[1]
        {
          (object) notificationCDs[index]
        });
      setupNotifications.Add(tuple);
    }
    return setupNotifications;
  }

  public virtual bool IsHtml(string text)
  {
    if (string.IsNullOrEmpty(text))
      return false;
    int num1 = text.IndexOf("<html", StringComparison.CurrentCultureIgnoreCase);
    int num2 = text.IndexOf("<body", StringComparison.CurrentCultureIgnoreCase);
    return num1 > -1 && num2 > -1 && num2 > num1;
  }

  protected virtual string GetRecipientKey(EmailExtracted entity)
  {
    List<string> values = new List<string>();
    values.Add("MailTo");
    values.AddRange((IEnumerable<string>) entity.MailTo.Select<MailAddress, string>((Func<MailAddress, string>) (address => address.Address)).OrderBy<string, string>((Func<string, string>) (x => x)));
    values.Add("MailCc");
    values.AddRange((IEnumerable<string>) entity.MailCc.Select<MailAddress, string>((Func<MailAddress, string>) (address => address.Address)).OrderBy<string, string>((Func<string, string>) (x => x)));
    values.Add("MailBcc");
    values.AddRange((IEnumerable<string>) entity.MailBcc.Select<MailAddress, string>((Func<MailAddress, string>) (address => address.Address)).OrderBy<string, string>((Func<string, string>) (x => x)));
    return string.Join(";", (IEnumerable<string>) values);
  }

  public virtual EmailExtracted MergeEmails(IList<EmailExtracted> emailsToGroup)
  {
    EmailExtracted emailExtracted = emailsToGroup.FirstOrDefault<EmailExtracted>();
    if (emailExtracted == null)
      return (EmailExtracted) null;
    IEnumerable<FileInfo> source1 = EnumerableExtensions.Distinct<FileInfo, Guid?>(emailsToGroup.SelectMany<EmailExtracted, FileInfo>((Func<EmailExtracted, IEnumerable<FileInfo>>) (email => (IEnumerable<FileInfo>) email.Attachments)), (Func<FileInfo, Guid?>) (file => file.UID));
    IEnumerable<Guid> source2 = EnumerableExtensions.Distinct<Guid, Guid>(emailsToGroup.SelectMany<EmailExtracted, Guid>((Func<EmailExtracted, IEnumerable<Guid>>) (email => (IEnumerable<Guid>) email.AttachmentLinks)), (Func<Guid, Guid>) (fileID => fileID));
    return new EmailExtracted()
    {
      Email = emailExtracted.Email,
      MailTo = emailExtracted.MailTo,
      MailCc = emailExtracted.MailCc,
      MailBcc = emailExtracted.MailBcc,
      Attachments = source1.ToList<FileInfo>(),
      AttachmentLinks = source2.ToList<Guid>()
    };
  }

  public virtual EmailExtracted MergeAttachments(EmailExtracted groupedEmail, string fileName)
  {
    if (groupedEmail == null)
      return (EmailExtracted) null;
    List<FileInfo> resultingAttachments = groupedEmail.Attachments;
    List<Guid> attachmentLinks = groupedEmail.AttachmentLinks;
    attachmentLinks.RemoveAll((Predicate<Guid>) (link => resultingAttachments.Exists((Predicate<FileInfo>) (file =>
    {
      Guid? uid = file.UID;
      Guid guid = link;
      return uid.HasValue && uid.GetValueOrDefault() == guid;
    }))));
    fileName = PXLocalizer.LocalizeFormat(fileName, new object[1]
    {
      (object) PXTimeZoneInfo.Now.ToString("yyyyMMdd")
    });
    FileInfo mergedPdf = AttachmentsHelper.CreateMergedPDF((IEnumerable<FileInfo>) resultingAttachments, fileName);
    groupedEmail.Attachments = new List<FileInfo>()
    {
      mergedPdf
    };
    groupedEmail.AttachmentLinks = new List<Guid>((IEnumerable<Guid>) attachmentLinks)
    {
      mergedPdf.UID.Value
    };
    return groupedEmail;
  }

  public virtual string GetSingleEmailCombinedFileName() => "Attachment {0}.pdf";
}
