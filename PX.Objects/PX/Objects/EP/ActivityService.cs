// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ActivityService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.CR;
using PX.SM;
using PX.Web.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Objects.EP;

public class ActivityService : IActivityService
{
  private PXView _viewShared;
  private PXView _viewCountShared;
  private EntityHelper _EntityHelper;

  private PXView _view
  {
    get => PXContext.GetSlot<PXView>(this.GetType().Name + nameof (_view));
    set => PXContext.SetSlot<PXView>(this.GetType().Name + nameof (_view), value);
  }

  private PXView _viewCount
  {
    get => PXContext.GetSlot<PXView>(this.GetType().Name + nameof (_viewCount));
    set => PXContext.SetSlot<PXView>(this.GetType().Name + nameof (_viewCount), value);
  }

  public EntityHelper EntityHelper
  {
    get
    {
      if (this._EntityHelper == null)
        this._EntityHelper = new EntityHelper(this.Graph);
      return this._EntityHelper;
    }
  }

  public int GetCount(object refNoteID)
  {
    this.ViewCount.Clear();
    int num1 = 0;
    int num2 = 0;
    List<PXFilterRow> pxFilterRowList = this.InitFilters(refNoteID, new int?());
    return ((PXResult) this.ViewCount.Select((object[]) null, new object[1]
    {
      refNoteID
    }, (object[]) null, new string[1]
    {
      typeof (CRActivity.createdDateTime).Name
    }, new bool[1]{ true }, pxFilterRowList.ToArray(), ref num1, 0, ref num2)[0]).RowCount ?? 0;
  }

  public IEnumerable Select(object refNoteID, int? filterId = null)
  {
    this.View.Clear();
    int num1 = 0;
    int num2 = 50;
    return (IEnumerable) this.View.Select((object[]) null, (object[]) null, (object[]) null, new string[1]
    {
      typeof (CRActivity.createdDateTime).Name
    }, new bool[1]{ true }, this.InitFilters(refNoteID, filterId).ToArray(), ref num1, 0, ref num2).Select<object, object>((Func<object, object>) (row => !(row is PXResult) ? row : ((PXResult) row)[0]));
  }

  private List<PXFilterRow> InitFilters(object refNoteID, int? filterId)
  {
    List<PXFilterRow> pxFilterRowList = new List<PXFilterRow>();
    if (filterId.HasValue)
    {
      PXCache cach = this.View.Graph.Caches[typeof (FilterRow)];
      foreach (PXResult<FilterRow> pxResult in PXSelectBase<FilterRow, PXSelect<FilterRow, Where<FilterRow.filterID, Equal<Required<FilterRow.filterID>>>>.Config>.Select(this.View.Graph, new object[1]
      {
        (object) filterId
      }))
      {
        FilterRow filterRow = PXResult<FilterRow>.op_Implicit(pxResult);
        PXFilterRow pxFilterRow1 = new PXFilterRow(filterRow.DataField, (PXCondition) (int) filterRow.Condition.Value, cach.GetValueExt<FilterRow.valueSt>((object) filterRow.ValueSt), cach.GetValueExt<FilterRow.valueSt2>((object) filterRow.ValueSt2));
        int? nullable = filterRow.OpenBrackets;
        pxFilterRow1.OpenBrackets = nullable.GetValueOrDefault();
        nullable = filterRow.CloseBrackets;
        pxFilterRow1.CloseBrackets = nullable.GetValueOrDefault();
        nullable = filterRow.Operator;
        pxFilterRow1.OrOperator = nullable.GetValueOrDefault() == 1;
        PXFilterRow pxFilterRow2 = pxFilterRow1;
        pxFilterRowList.Add(pxFilterRow2);
      }
      if (pxFilterRowList.Count > 0)
      {
        ++pxFilterRowList[0].OpenBrackets;
        ++pxFilterRowList[pxFilterRowList.Count - 1].CloseBrackets;
        pxFilterRowList[pxFilterRowList.Count - 1].OrOperator = false;
      }
    }
    pxFilterRowList.Add(new PXFilterRow()
    {
      DataField = typeof (CRActivity.refNoteID).Name,
      Condition = (PXCondition) 0,
      Value = refNoteID
    });
    return pxFilterRowList;
  }

  public virtual string GetKeys(object item)
  {
    PXCache cache = this.View.Cache;
    StringBuilder stringBuilder = new StringBuilder();
    bool flag = false;
    foreach (string key in (IEnumerable<string>) cache.Keys)
    {
      if (flag)
        stringBuilder.Append(',');
      stringBuilder.Append(cache.GetValue(item, key));
      flag = true;
    }
    return stringBuilder.ToString();
  }

  public virtual bool ShowTime(object item)
  {
    return Array.IndexOf((Array) this.ActivitiesWithTime, (object) ((CRActivity) item).ClassID) > -1;
  }

  public virtual DateTime? GetStartDate(object item) => ((CRActivity) item).StartDate;

  public virtual DateTime? GetEndDate(object item) => ((CRActivity) item).EndDate;

  public virtual void Cancel(string keys)
  {
    if (!(this.SearchItem(keys) is CRActivity crActivity))
      return;
    System.Type type = crActivity.With<CRActivity, System.Type>((Func<CRActivity, System.Type>) (_ => this.EntityHelper.GetPrimaryGraphType((object) _, true)));
    if (!(type != (System.Type) null) || !(Activator.CreateInstance(type) is IActivityMaint instance))
      return;
    instance.CancelRow(crActivity);
  }

  public virtual void Complete(string keys)
  {
    if (!(this.SearchItem(keys) is CRActivity crActivity))
      return;
    System.Type type = crActivity.With<CRActivity, System.Type>((Func<CRActivity, System.Type>) (_ => this.EntityHelper.GetPrimaryGraphType((object) _, true)));
    if (!(type != (System.Type) null) || !(Activator.CreateInstance(type) is IActivityMaint instance))
      return;
    instance.CompleteRow(crActivity);
  }

  public virtual void Defer(string keys, int minuts)
  {
  }

  public virtual void Dismiss(string keys)
  {
  }

  public virtual void Open(string keys)
  {
    ((TasksAndEventsReminder) this.Graph).NavigateToItem(this.SearchItem(keys) as CRActivity);
  }

  public virtual bool IsViewed(object item) => true;

  public virtual string GetImage(object item) => ((CRActivity) item).ClassIcon;

  public virtual string GetTitle(object item) => ((CRActivity) item).Subject;

  protected virtual object SearchItem(string keys)
  {
    return (object) PXResultset<CRActivity>.op_Implicit(PXSelectBase<CRActivity, PXSelect<CRActivity>.Config>.Search<CRActivity.noteID>(this.View.Graph, (object) keys, Array.Empty<object>()));
  }

  protected PXGraph Graph => this.View.Graph;

  private PXView View
  {
    get
    {
      if (this._view == null || this._viewCount == null)
        this.CreateView();
      return this._view;
    }
  }

  private PXView ViewCount
  {
    get
    {
      if (this._view == null || this._viewCount == null)
        this.CreateView();
      return this._viewCount;
    }
  }

  protected virtual void CreateView()
  {
    TasksAndEventsReminder instance = PXGraph.CreateInstance<TasksAndEventsReminder>();
    this._view = ((PXSelectBase) instance.ActivityList).View;
    this._viewCount = ((PXSelectBase) instance.ActivityCount).View;
  }

  public virtual void ShowAll(object refNoteID)
  {
    if (!(refNoteID is Guid refNoteID1))
      return;
    ((TasksAndEventsReminder) this.Graph).OpenInquiryScreen(refNoteID1);
  }

  public virtual int[] ActivitiesWithTime
  {
    get => new int[3]{ 1, 2, 4 };
  }

  public virtual IEnumerable<ActivityService.Total> GetCounts()
  {
    List<ActivityService.Total> counts = new List<ActivityService.Total>();
    foreach (PXResult<ActivityService.Total> pxResult in ((PXSelectBase<ActivityService.Total>) this.ReminderGraph.Counters).Select(Array.Empty<object>()))
    {
      ActivityService.Total total = PXResult<ActivityService.Total>.op_Implicit(pxResult);
      counts.Add(total);
    }
    if (this._viewShared == null || this._viewCountShared == null)
    {
      this._viewShared = this._view;
      this._viewCountShared = this._viewCount;
      this._view = (PXView) null;
      this._viewCount = (PXView) null;
    }
    return (IEnumerable<ActivityService.Total>) counts;
  }

  private TasksAndEventsReminder ReminderGraph
  {
    get
    {
      return this._viewShared != null && this._viewCountShared != null ? (TasksAndEventsReminder) this._viewShared.Graph : (TasksAndEventsReminder) this.View.Graph;
    }
  }

  public int GetActiveReminderCounts()
  {
    return ((PXSelectBase<CRActivity>) this.ReminderGraph.ReminderList).Select(Array.Empty<object>()).Count;
  }

  public virtual IEnumerable<ActivityService.IActivityType> GetActivityTypes()
  {
    ActivityService.ActivityTypeDeinition slot = PXDatabase.GetSlot<ActivityService.ActivityTypeDeinition>(typeof (EPActivityType).Name, new System.Type[1]
    {
      typeof (EPActivityType)
    });
    return slot == null ? (IEnumerable<ActivityService.IActivityType>) null : (IEnumerable<ActivityService.IActivityType>) slot.List;
  }

  public virtual void CreateTask(object refNoteID)
  {
    this.CreateActivity(0, (Guid?) refNoteID, (string) null, new int?());
  }

  public virtual void CreateEvent(object refNoteID)
  {
    this.CreateActivity(1, (Guid?) refNoteID, (string) null, new int?());
  }

  public virtual void CreateActivity(object refNoteID, string typeCode)
  {
    this.CreateActivity(refNoteID, typeCode, new int?());
  }

  public virtual void CreateActivity(
    object refNoteID,
    string typeCode,
    PXRedirectHelper.WindowMode windowMode = 3)
  {
    this.CreateActivity(refNoteID, typeCode, new int?(), windowMode);
  }

  [Obsolete]
  public virtual void CreateActivity(
    object refNoteID,
    string typeCode,
    Guid? obsoleteOwnerID,
    PXRedirectHelper.WindowMode windowMode = 3)
  {
    this.CreateActivity(refNoteID, typeCode, obsoleteOwnerID.HasValue ? PXAccess.GetContactID(obsoleteOwnerID) : new int?(), windowMode);
  }

  public virtual void CreateActivity(
    object refNoteID,
    string typeCode,
    int? owner,
    PXRedirectHelper.WindowMode windowMode = 3)
  {
    this.CreateActivity(2, (Guid?) refNoteID, typeCode, owner, windowMode);
  }

  public virtual void OpenMailPopup(string link)
  {
    CREmailActivityMaint instance1 = PXGraph.CreateInstance<CREmailActivityMaint>();
    PXCache cach = ((PXGraph) instance1).Caches[typeof (CRSMEmail)];
    CRSMEmail instance2 = (CRSMEmail) cach.CreateInstance();
    this.FillMailAccount(instance2);
    instance2.Type = (string) null;
    instance2.IsIncome = new bool?(false);
    string str = link;
    instance2.Body = string.IsNullOrEmpty(str) ? PXRichTextConverter.NormalizeHtml(link + instance2.Body) : str;
    cach.Insert((object) instance2);
    PXRedirectHelper.TryRedirect((PXGraph) instance1, (PXRedirectHelper.WindowMode) 3);
  }

  public virtual void CreateEmailActivity(object refNoteID, int EmailAccountID)
  {
    this.CreateEmailActivity(refNoteID, EmailAccountID, (Action<object>) null);
  }

  public virtual void CreateEmailActivity(
    object refNoteID,
    int EmailAccountID,
    Action<object> initializeHandler)
  {
    System.Type graphType = ((object) new CRActivity()
    {
      ClassID = new int?(4),
      Type = (string) null
    }).With<object, System.Type>((Func<object, System.Type>) (_ => this.EntityHelper.GetPrimaryGraphType(_, true)));
    if (!PXAccess.VerifyRights(graphType))
      throw new AccessViolationException(PX.Objects.CR.Messages.FormNoAccessRightsMessage(graphType));
    this.DoCreateEmailActivity(PXGraph.CreateInstance(graphType), refNoteID, EmailAccountID, initializeHandler);
  }

  public virtual void DoCreateEmailActivity(
    PXGraph targetGraph,
    object refNoteID,
    int EmailAccountID,
    Action<object> initializeHandler)
  {
    PXCache primaryCache = GraphHelper.GetPrimaryCache(targetGraph);
    CRSMEmail instance = (CRSMEmail) primaryCache.CreateInstance();
    instance.IsIncome = new bool?(false);
    if (EmailAccountID != 0)
      this.FillMailAccount(instance, EmailAccountID);
    else
      this.FillMailAccount(instance);
    this.FillMailCC(targetGraph, instance, (Guid?) refNoteID);
    instance.RefNoteID = (Guid?) refNoteID;
    instance.Body = this.GenerateMailBody(targetGraph);
    if (initializeHandler != null)
      initializeHandler((object) instance);
    primaryCache.Insert((object) instance);
    PXRedirectHelper.TryRedirect(targetGraph, (PXRedirectHelper.WindowMode) 3);
  }

  public virtual void CreateActivity(
    int classId,
    Guid? refNoteID,
    string typeCode,
    int? owner,
    PXRedirectHelper.WindowMode windowMode = 3)
  {
    object obj = (object) new CRActivity()
    {
      ClassID = new int?(classId),
      Type = typeCode
    };
    System.Type primaryGraphType = this.EntityHelper.GetPrimaryGraphType(ref obj, true);
    PXGraph targetGraph = PXAccess.VerifyRights(primaryGraphType) ? PXGraph.CreateInstance(primaryGraphType) : throw new AccessViolationException(PX.Objects.CR.Messages.FormNoAccessRightsMessage(primaryGraphType));
    this.DoCreateActivity(targetGraph, classId, refNoteID, typeCode, owner);
    PXRedirectHelper.TryRedirect(targetGraph, windowMode);
  }

  public virtual void DoCreateActivity(
    PXGraph targetGraph,
    int classId,
    Guid? refNoteID,
    string typeCode,
    int? owner)
  {
    CRActivity crActivity1 = (CRActivity) null;
    PXCache primaryCache = GraphHelper.GetPrimaryCache(targetGraph);
    if (!owner.HasValue)
      owner = EmployeeMaint.GetCurrentOwnerID(targetGraph);
    Action<object> action = (Action<object>) (act1 =>
    {
      if (!(act1 is CRActivity crActivity3))
        return;
      crActivity3.ClassID = new int?(classId);
      crActivity3.RefNoteID = refNoteID;
      if (!string.IsNullOrEmpty(typeCode))
        crActivity3.Type = typeCode;
      crActivity3.OwnerID = owner;
    });
    EntityHelper entityHelper = new EntityHelper(targetGraph);
    System.Type entityRowType = entityHelper.GetEntityRowType(refNoteID);
    object entityRow = entityHelper.GetEntityRow(entityRowType, refNoteID);
    System.Type type = (System.Type) null;
    if (entityRowType != (System.Type) null)
      PXPrimaryGraphAttribute.FindPrimaryGraph(targetGraph.Caches[entityRowType], ref entityRow, ref type);
    if (type != (System.Type) null)
    {
      PXGraph instance1 = PXGraph.CreateInstance(type);
      if (instance1.Caches[typeof (CRActivity)] is PXCache<CRActivity> cach)
      {
        instance1.Views[instance1.PrimaryView].Cache.Current = entityRow;
        CRActivity instance2 = (CRActivity) ((PXCache) cach).CreateInstance();
        if (action != null)
          action((object) instance2);
        crActivity1 = GraphHelper.InitNewRow<CRActivity>(cach, instance2);
      }
    }
    if (crActivity1 == null)
    {
      CRActivity instance = (CRActivity) primaryCache.CreateInstance();
      action((object) instance);
      crActivity1 = GraphHelper.InitNewRow<CRActivity>((PXCache<CRActivity>) primaryCache, instance);
    }
    CRActivity destData = primaryCache.Update((object) crActivity1) as CRActivity;
    if (!(entityRowType != (System.Type) null))
      return;
    UDFHelper.CopyAttributes(targetGraph.Caches[entityRowType], entityRow, primaryCache, (object) destData, destData?.Type);
  }

  public virtual string GenerateMailBody(PXGraph graph)
  {
    return PXRichTextConverter.NormalizeHtml(MailAccountManager.AppendSignature((string) null, graph, (MailAccountManager.SignatureOptions) 0));
  }

  public virtual void FillMailAccount(CRSMEmail message)
  {
    message.MailAccountID = MailAccountManager.DefaultMailAccountID;
  }

  public virtual void FillMailAccount(CRSMEmail message, int MailAccountID)
  {
    message.MailAccountID = new int?(MailAccountID);
  }

  public virtual void FillMailCC(PXGraph graph, CRSMEmail message, Guid? refNoteId)
  {
    if (!refNoteId.HasValue)
      return;
    message.MailCc = PXDBEmailAttribute.AppendAddresses(message.MailCc, this.GetEmailAddressesForCc(graph, new Guid?(refNoteId.Value)));
  }

  public virtual string GetEmailAddressesForCc(PXGraph graph, Guid? refNoteID)
  {
    string emailAddressesForCc = string.Empty;
    Select2<CRRelation, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<CRRelation.contactID>>, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CRRelation.entityID>>, LeftJoin<Users, On<Users.pKID, Equal<PX.Objects.CR.Contact.userID>>>>>, Where<CRRelation.refNoteID, Equal<Required<CRRelation.refNoteID>>>> select2 = new Select2<CRRelation, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<CRRelation.contactID>>, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CRRelation.entityID>>, LeftJoin<Users, On<Users.pKID, Equal<PX.Objects.CR.Contact.userID>>>>>, Where<CRRelation.refNoteID, Equal<Required<CRRelation.refNoteID>>>>();
    PXView pxView = new PXView(graph, false, (BqlCommand) select2);
    object[] objArray = new object[1]{ (object) refNoteID };
    foreach (PXResult<CRRelation, PX.Objects.CR.Contact, BAccount, Users> pxResult in pxView.SelectMulti(objArray))
    {
      CRRelation relation = ((PXResult) pxResult).GetItem<CRRelation>();
      if ((relation != null ? (!relation.AddToCC.GetValueOrDefault() ? 1 : 0) : 1) == 0)
      {
        PX.Objects.CR.Contact contact = ((PXResult) pxResult).GetItem<PX.Objects.CR.Contact>();
        BAccount businessAccount = ((PXResult) pxResult).GetItem<BAccount>();
        Users user = ((PXResult) pxResult).GetItem<Users>();
        CRRelation.FillUnboundData(relation, contact, businessAccount, user);
        if (relation.Email != null && (relation.Email = relation.Email.Trim()) != string.Empty)
          emailAddressesForCc = PXDBEmailAttribute.AppendAddresses(emailAddressesForCc, PXDBEmailAttribute.FormatAddressesWithSingleDisplayName(relation.Email, relation.ContactName ?? relation.Name));
      }
    }
    return emailAddressesForCc;
  }

  public class ActivityTypeDeinition : IPrefetchable, IPXCompanyDependent
  {
    public readonly List<EPActivityType> List = new List<EPActivityType>();

    void IPrefetchable.Prefetch()
    {
      this.List.Clear();
      this.List.AddRange(GraphHelper.RowCast<EPActivityType>((IEnumerable) PXSelectBase<EPActivityType, PXSelect<EPActivityType, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPActivityType.active, Equal<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPActivityType.application, Equal<PXActivityApplicationAttribute.backend>>>>>.And<BqlOperand<EPActivityType.isSystem, IBqlBool>.IsEqual<False>>>>>.Config>.Select(new PXGraph(), Array.Empty<object>())));
    }
  }
}
